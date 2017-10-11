using System;
using System.Collections.Generic;
using System.Diagnostics;
using CGT.Api.DTO.Boss.TravelOrder.Request;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CGT.DDD.Extension;
using CGT.DDD.Utils;
using CGT.Entity.CgtModel;
using CGT.Event.Model.Manage;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using CGT.DDD.MQ;
using Newtonsoft.Json;
using CGT.Entity.CgtTravelModel;
using AutoMapper;
using CGT.DDD.Caching;
using Microsoft.Extensions.Caching.Memory;
using CGT.DDD.Logger;

namespace CGT.Api.Service.Manage.CheckTicket
{

    public class TravelOrderImportService : ApiBase<RequestTravelOrderImportModel>
    {
        #region 服务

        /// <summary>
        /// 批次机票信息
        /// </summary>
        public TravelBatchOrderRep TravelBatchOrderRep { get; set; }

        /// <summary>
        /// 批次信息
        /// </summary>
        public TravelBatchRep TravelBatchRep { get; set; }

        /// <summary>
        /// 企业白名单，月限额，余额
        /// </summary>
        public EnterpriseWhiteRep EnterpriseWhiteRep { get; set; }

        /// <summary>
        /// 企业订单数据
        /// </summary>
        public EnterpriseOrderRep EnterpriseOrderRep { get; set; }

        /// <summary>
        /// 分销商信息
        /// </summary>
        public UserAccountRep UserAccountRep { get; set; }
        /// <summary>
        /// 风控信息
        /// </summary>
        public TravelRiskRep TravelRiskRep { get; set; }

        public ExeclHelper ExcelHelper { get; set; }

        public UserFactoringRep UserFactoringRep { get; set; }
 
        public ICache Cache = new RedisCache();
        #endregion

        /// <summary>
        /// 上传文件流
        /// </summary>
        private Stream _fileStream;

        public Stream ExcelStream { set { _fileStream = value; } }

        /// <summary>
        /// 当前上传数据
        /// </summary>
        private Dictionary<long, List<BaseRiskModel>> _manageRiskModels = new Dictionary<long, List<BaseRiskModel>>();

        /// <summary>
        /// 本次验证结果
        /// </summary>
        private readonly List<Tuple<string, string>> _verifyResults = new List<Tuple<string, string>>();

        //账号信息
        private UserAccount _userAccount = new UserAccount();

        //企业分控信息
        private Dictionary<int, TravelRisk> _travelRisks = new Dictionary<int, TravelRisk>();

        //分销商保理信息
        private UserFactoring _userFactoring = new UserFactoring();

        private List<EnterpriseWhiteList> _enterpriseWhiteLists = new List<EnterpriseWhiteList>();

        private string   strKey = "excel_ticket_noes";

        /// <summary>
        /// 用于非Excel文件导入
        /// </summary>
        public  List<BaseRiskModel> BaseRiskModels { get; set; }
        /// <summary>
        /// TODO TODO TODO ....
        /// </summary>
        protected override void ExecuteMethod()
        {
            try
            {
                if (null == _fileStream && !BaseRiskModels.Any()) throw  new Exception("选择要上传的文件");

                UserAccount ua = new UserAccount() { PayCenterCode = Parameter.PayCenterCode };
                _userAccount = UserAccountRep.GetUserAccount(ua);

                if (!ValidUserFactoring())
                {
                    throw new Exception("未配置保理企业；请联系相关人员处理！");
                   
                }
                //生成批次号
                var batchNo = DateTime.Now.Ticks;

                //文件上传
                if (null != _fileStream)
                {
                    ExeclTalbe et = ExcelHelper.ExcelImport(_fileStream);

                    //去除标题
                    et.rows.RemoveAt(0);

                    Debug.WriteLine("Excel 总条数:{0}", et.rows.Count);

                    //数据格式验证
                    if (!ExcelTable2EntityList(et, "" + batchNo))
                    {
                        this.Result.Data = this._verifyResults.Select(t => t.Item2);
                        this.Result.Message = "Excel数据验证失败!";
                        this.Result.ErrorCode = "200";
                        return;
                    }
                }
                else
                {
                    _manageRiskModels = BaseRiskModels.GroupBy(t => t.EnterpriseID)
                        .ToDictionary(t => t.Key, t => t.ToList());
                }

                Debug.WriteLine("格式转换后条数:{0},错误数据:{1}", _manageRiskModels.Count, _verifyResults.Select(t => t.Item2));

                //去重,Excel中数据重复
                if (!RemovalTicket())
                {
                    this.Result.Message = string.Join(",",this._verifyResults.Select(t => t.Item2));
                    this.Result.ErrorCode = "200";
                    return;
                }

                Debug.WriteLine("去重后条数:{0},错误数据:{1}", _manageRiskModels.Count, _verifyResults.Select(t => t.Item2));

                //验证企业状态
                if (!VerifyEnterpriseStatus())
                {
                    this.Result.Data = "企业信息验证失败!";
                    this.Result.Message = JsonConvert.SerializeObject(this._verifyResults.Select(t => t.Item2)); 
                    return;
                }

                Debug.WriteLine("企业信息验证结果:{0},错误数据:{1}", _manageRiskModels.Count, _verifyResults.Select(t => t.Item2));

                //验证风控
                VerifyRisk();

                Debug.WriteLine("风控验证结果:{0},错误数据:{1}", _manageRiskModels.Count, _verifyResults.Select(t => t.Item2));

                //验证分销商余额
                if (!VerifyDistrbutorBalance())
                {
                    this.Result.Message = "分销商余额不足!";
                    this.Result.ErrorCode = "200";
                    return;
                }

                Debug.WriteLine("分销商月限额验证结果");

                //异常信息返回
                if (0 < _verifyResults.Count)
                {
                    Result.Message = string.Join("\r\n", _verifyResults.Select(t => t.Item2));
                    this.Result.ErrorCode = "200";
                }

                if (0 < _manageRiskModels.Count)
                {
                    //入库&写入队列
                    SaveOrderData();

                    Result.Message += "\r\n 成功上传条数：" + (_manageRiskModels.Values.SelectMany(x => x).ToList().Count);
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "TravelOrderImportService");
                this.Result.ErrorCode = "200";
                this.Result.Message = "服务端程序错误，请联系相关人员！";
                

            }

        }

        /// <summary>
        /// 验证分销商是否有保理
        /// </summary>
        /// <returns></returns>
        private bool ValidUserFactoring()
        {
            _userFactoring = UserFactoringRep.GetUserFactoring(this._userAccount.UserFactoringCode);

            return null != _userFactoring;
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <param name="et"></param>
        /// <param name="strTravelBatchId">当前批次Id</param>
        /// <returns></returns>
        private bool ExcelTable2EntityList(ExeclTalbe et, string strTravelBatchId)
        {
            var modeles = new List<BaseRiskModel>();

            var result = true;

            Parallel.ForEach(et.rows, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, row =>
               {
                   try
                   {
                       //TODO 调整格式验证
                       var model = new BaseRiskModel();

                       model.PayCenterCode = Parameter.PayCenterCode;

                       model.PayCenterName = Parameter.PayCenterName;

                       model.TravelBatchId = strTravelBatchId;

                       //票价
                       var price = 0.0m;
                       if (Decimal.TryParse(row.columns[0].ColumnValue.Trim(), out price) && 0.0m < price)
                       {
                           model.TicketPrice = price;
                       }
                       //订单价
                       if (Decimal.TryParse(row.columns[1].ColumnValue.Trim(), out price) && 0.0m < price)
                       {
                           model.OrderAmount = price;
                       }

                       DateTime cellDate;

                       if (DateTime.TryParse(row.columns[2].ColumnValue.Trim(), out cellDate) && 1990 < cellDate.Year)
                       {
                           //起飞时间
                           model.DepartureTime = cellDate;
                       }
                       else
                       {
                           throw new Exception("起飞时间格式不正确");
                       }

                       if (DateTime.TryParse(row.columns[9].ColumnValue.Trim(), out cellDate) && 1990 < cellDate.Year)
                       {
                           //出票时间
                           model.TicketTime = cellDate;
                       }
                       else
                       {
                           throw new Exception("出票时间格式不正确");
                       }

                       //出发机场3字码
                       model.DepartCode = row.columns[3].ColumnValue.Trim();

                       if (!Regex.IsMatch(model.DepartCode, "^[A-Za-z]{3}$"))
                       {
                           throw new Exception("出发机场三字码格式不正确（例：PEK）！");
                       }

                       //到达机场
                       model.ArriveCode = row.columns[4].ColumnValue.Trim();

                       if (!Regex.IsMatch(model.DepartCode, "^[A-Za-z]{3}$"))
                       {
                           throw new Exception("到达机场三字码格式不正确（例：PEK）！");
                       }

                       //航班号
                       model.FlightNo = row.columns[5].ColumnValue.Trim();

                       if (!Regex.IsMatch(model.FlightNo, "^[A-Za-z0-9]{5,6}$"))
                       {
                           throw new Exception("航班号格式不正确（例：CA5698或CA234）！");
                       }

                       //仓位
                       model.Cabin = row.columns[6].ColumnValue.Trim();

                       if (!Regex.IsMatch(model.Cabin, "^[A-Za-z0-9]{1,2}$"))
                       {
                           throw new Exception("舱位格式不正确（例：Y或Z1）！");
                       }

                       //乘客姓名-不验证
                       model.PersonName = row.columns[7].ColumnValue.Trim();
                       if (string.IsNullOrEmpty(model.PersonName))
                       {
                           throw new Exception("乘客姓名不能为空！");
                       }

                       //票号13位数字
                       model.TicketNum = row.columns[8].ColumnValue.Replace("-", "").Trim();

                       if (!Regex.IsMatch(model.TicketNum, "^((?=.*[0-9].*))[0-9]{13}$"))
                       {
                           throw new Exception("票号格式不正确！");
                       }

                       model.PNR = row.columns[10].ColumnValue.Trim();

                       if (!Regex.IsMatch(model.PNR, "^[A-Za-z0-9]{6}$"))
                       {
                           throw new Exception("PNR格式不正确！");
                       }

                       if (1 > row.columns[11].ColumnValue.Split('|').Length)
                       {
                           throw new Exception("保理企业格式错误!");
                       }
                       var enterpriseId = 0;

                       Int32.TryParse(row.columns[11].ColumnValue.Split('|')[1].Trim(), out enterpriseId);
                       model.EnterpriseID = enterpriseId;

                       model.EnterpriseName = row.columns[11].ColumnValue.Split('|')[0];

                       //数据格式验证
                       modeles.Add(model);
                   }
                   catch (Exception e)
                   {
                       this._verifyResults.Add(new Tuple<string, string>("", row.RIndex+"行"+e.Message));
                       result = false;
                   }

               });

            _manageRiskModels = modeles.GroupBy(t => t.EnterpriseID).ToDictionary(t => t.Key, t => t.ToList());

            return result;
        }

        //数据重复验证(票号)
        private bool RemovalTicket()
        {
            var result = true;

            var ticketNums = _manageRiskModels.Values.SelectMany(x => x).ToList(); ;

            //
            var ticketNoDic = ticketNums.GroupBy(t => t.TicketNum).ToDictionary(t => t.Key, t => t.Count());


            //var hasRepeatTicket = ticketNoDic.Values.Count(t => t > 1) > 0;
            //Excel原始数据验证是否重复
            ticketNoDic.Foreach(item =>
            {
                if (1 < item.Value)
                {
                    _verifyResults.Add(new Tuple<string, string>("", item.Key + "票号重复。"));

                    result = false;
                }
            });


            var ticketNoes = ticketNoDic.Keys.ToList();

            var cacheTicket = GetCacheTickets();

            var unionList = cacheTicket?.Where(t => ticketNoes.Contains(t)) ?? new List<string>();
            unionList.Foreach(item =>
            {
                _verifyResults.Add(new Tuple<string, string>("", item + "票号已存在。"));
                result = false;
            });


            //数据库验证重复
            var exsitOrders = EnterpriseOrderRep.GetTravelOrdersByTickets(ticketNoes);

           //剔除重复数据
            exsitOrders.ForEach(item =>
            {
                if (item.EnterpriseWhiteListID == null || !_manageRiskModels.ContainsKey((long)item.EnterpriseWhiteListID.Value)) return;

                var removeModel = _manageRiskModels[(long)item.EnterpriseWhiteListID].FirstOrDefault(t => t.TicketNum == item.TicketNo);
                _verifyResults.Add(new Tuple<string, string>("", item.TicketNo + "票号已存在。"));

                _manageRiskModels[(long)item.EnterpriseWhiteListID].Remove(removeModel);


                if (1 > _manageRiskModels[(long)item.EnterpriseWhiteListID].Count)
                {
                    _manageRiskModels.Remove((long)item.EnterpriseWhiteListID);
                }

                result = false;
            });

            return result;

        }

        /// <summary>
        /// 验证企业状态,及余额
        /// </summary>
        private bool VerifyEnterpriseStatus()
        {
            var result = true;

            //验证企业状态
            var enterprseDic = _manageRiskModels;

            var enterprseIds = enterprseDic.Keys.ToList();

            //获取有效企业及余额
            _enterpriseWhiteLists = EnterpriseWhiteRep.GetEnterpriseWhiteLists(enterprseIds, Parameter.PayCenterCode).ToList();

            //无效企业
            var unUsefullEnterprises = enterprseDic.Keys.ToList().Except(_enterpriseWhiteLists.Select(t => t.EnterpriseWhiteListID).ToList()).ToList();

            if (unUsefullEnterprises.Any())
            {
                unUsefullEnterprises.ForEach(item =>
                {
                    //记录无效企业信息
                    //删除无效企业
                    enterprseDic.Remove(item);

                    _verifyResults.Add(new Tuple<string, string>("", item + ",企业不在白名单"));

                    result = false;
                });
            }

            var hasMonthLimitEnterprise = _enterpriseWhiteLists.Where(t => 1 == t.MonthStatue).ToList();

            //配置月限额企业的已导入未还款订单
            var enterpriseOrdersDic = hasMonthLimitEnterprise.Count() > 0 ?
                EnterpriseOrderRep.GetEnterpriseOrderSum(hasMonthLimitEnterprise.Select(t => t.EnterpriseWhiteListID),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)).GroupBy(t => t.Item1).ToDictionary(t => t.Key, t => t.Sum(e => e.Item2)) : new Dictionary<long, decimal>();

            //验证企业余额
            _enterpriseWhiteLists.Foreach(item =>
            {
                //当前企业导入的订单金额
                var enterpriseSum = enterprseDic.ContainsKey(item.EnterpriseWhiteListID)
                    ? enterprseDic[item.EnterpriseWhiteListID].Sum(t => t.OrderAmount)
                    : 0.0m;

                //是否有月限额限制
                var hasMonthLimit = enterpriseOrdersDic.ContainsKey(item.EnterpriseWhiteListID);

                if (hasMonthLimit)
                {
                    //已导入返现，未还款的订单金额
                    var ordersSum = enterpriseOrdersDic[item.EnterpriseWhiteListID] + enterpriseSum;

                    if (ordersSum > item.CreditMonthAmount)
                    {

                        enterprseDic.Remove(item.EnterpriseWhiteListID);
                        _verifyResults.Add(new Tuple<string, string>("", item.EnterpriseWhiteListID + ",企业达到月限额"));
                        result = false;
                    }
                }
                else
                {
                    if (enterpriseSum > item.AccountBalance)
                    {
                        //剔除余额不足
                        enterprseDic.Remove(item.EnterpriseWhiteListID);
                        _verifyResults.Add(new Tuple<string, string>("", item.EnterpriseWhiteListID + ",企业余额不足"));
                        result = false;

                    }
                }


            });

            return result;

        }

        /// <summary>
        /// 验证企业风控
        /// </summary>
        /// <returns></returns>
        private bool VerifyRisk()
        {
            var result = true;

            if (1 > _manageRiskModels.Keys.Count)
            {
                return false;
            }
            _travelRisks = TravelRiskRep.GetTravelRiskByEnterpriseIDs(_manageRiskModels.Keys.ToList()).GroupBy(t => t.EnterpriseID).ToDictionary(t => t.Key.Value, t => t.FirstOrDefault());

            _manageRiskModels.Foreach(item =>
            {

                var riskModel = _travelRisks.ContainsKey((int)item.Key) ? _travelRisks[(int)item.Key] : null;

                if (null == riskModel)
                {

                    //TODO
                    this._verifyResults.Add(new Tuple<string, string>("", "" + item.Key));
                    return;
                }
                //企业最低条数
                var riskCount = riskModel.UploadLowCount ?? 0;


                //风控类型
                item.Value.ForEach(entity =>
                {
                    entity.TravelRiskType = riskModel.TravelRiskType ?? 0;
                    entity.EtermType = riskModel.EtermType ?? 0;
                });

                //验证上传条数与风控条数
                if (item.Value.Any() && item.Value.Count < riskCount)
                {
                    _verifyResults.Add(new Tuple<string, string>("", string.Format("{0},上传票数不能低于{1}张", item.Value.FirstOrDefault().EnterpriseName, riskCount)));

                }
            });

            return result;
        }

        /// <summary>
        /// 分销商月限额
        /// </summary>
        private bool VerifyDistrbutorBalance()
        {

            //分销商配置月限额
            if (null != _userAccount && null != _userAccount.CreditAmount && 0.001m < _userAccount.CreditAmount)
            {
                var distributeAmount = EnterpriseOrderRep.GetDistributSum(Parameter.PayCenterCode);
                if (distributeAmount + _manageRiskModels.Values.SelectMany(t => t).Sum(t => t.OrderAmount) >
                    _userAccount.CreditAmount)
                {
                    _verifyResults.Add(new Tuple<string, string>("", "此分销商到达授信限额"));
                    return false;
                }
            }


            return true;
        }

        //保存数据并写入队列
        private bool SaveOrderData()
        {


            var travelBatchList = new List<TravelBatch>();

            _manageRiskModels.Foreach(item =>
            {

                if (0 < item.Value.Count)
                {

                    var enterprise = _enterpriseWhiteLists.FirstOrDefault(t => t.EnterpriseWhiteListID == item.Key);

                    var defaultModel = item.Value.FirstOrDefault();

                    var travelRisk = _travelRisks.ContainsKey((int)item.Key) ? _travelRisks[(int)item.Key] : null;

                    var travelBatchModel = new TravelBatch();
                    travelBatchModel.TravelBatchId = defaultModel.TravelBatchId;

                    travelBatchModel.PayCenterCode = Parameter.PayCenterCode;
                    travelBatchModel.PayCenterName = Parameter.PayCenterName;
                    travelBatchModel.EnterpriseId = (int)item.Key;
                    travelBatchModel.EnterpriseName = defaultModel.EnterpriseName;
                    travelBatchModel.TravelRiskType = defaultModel.TravelRiskType;
                    travelBatchModel.TravelRiskState = 0;

                    //黑屏比例
                    travelBatchModel.EtermSuccessRate = defaultModel.EtermSuccessRate;
                    travelBatchModel.EtermFailRate = defaultModel.EtermFailRate;

                    //白屏比例
                    travelBatchModel.WhithSuccessRate = null != travelRisk ? travelRisk.WhiteSuccessRate : 0;
                    travelBatchModel.WhithFailRate = null != travelRisk ? travelRisk.WhiteFailRate : 0;

                    travelBatchModel.CreateTime = DateTime.Now;
                    travelBatchModel.TotalCount = item.Value.Count();
                    travelBatchModel.TranslationState = 0;
                    travelBatchModel.TotalAmount = item.Value.Sum(t => t.OrderAmount);


                    travelBatchModel.PayCenterCode = Parameter.PayCenterCode;
                    travelBatchModel.PayCenterName = Parameter.PayCenterName;
                    travelBatchModel.UserFactoringId = (int)_userFactoring.UserFactoringId;
                    travelBatchModel.FactoringName = _userFactoring.FactoringName;
                    travelBatchModel.FactoringEmail = _userFactoring.FactoringEmail;
                    travelBatchModel.FactoringReapalNo = _userFactoring.FactoringReapalNo;
                    travelBatchModel.InterestRate = _userFactoring.InterestRate ?? 0;
                    travelBatchModel.FactoringInterestRate = _userAccount.FactoringInterestRate ?? 0;
                    travelBatchModel.AccountPeriod = null != enterprise ? Int32.Parse(enterprise.AccountPeriod) : 0;
                    travelBatchModel.EnterpriseId = (int)item.Key;
                    travelBatchModel.EnterpriseName = null != enterprise ? enterprise.EnterpriseName : "";
                    travelBatchModel.UserName = _userAccount.UserName;
                    travelBatchModel.BackReapalNo = _userAccount.ReapalMerchantId;
                    travelBatchModel.TravelBatchId = item.Value.FirstOrDefault().TravelBatchId;


                    travelBatchList.Add(travelBatchModel);
                }
            });


            //批次信息保存
            var result = TravelBatchRep.Insert(travelBatchList);

            if (result < 1)
            {
                this.Result.Data = "批次信息保存失败，请联系相关人员!";
                return false;
            }
            //订单信息
            var models = _manageRiskModels.Values.SelectMany(x => x).ToList();

            var travelBatchOrder = Mapper.Map<List<BaseRiskModel>, List<TravelBatchOrder>>(models);
            travelBatchOrder.ForEach(i => i.TicketTime = Convert.ToDateTime(Convert.ToDateTime(i.TicketTime).ToString("yyyy-MM-dd HH:mm:ss")));
            travelBatchOrder.ForEach(i => i.CreateTime = Convert.ToDateTime(Convert.ToDateTime(i.CreateTime).ToString("yyyy-MM-dd HH:mm:ss")));
            travelBatchOrder.ForEach(i => i.DepartureTime = Convert.ToDateTime(Convert.ToDateTime(i.DepartureTime).ToString("yyyy-MM-dd HH:mm:ss")));
            travelBatchOrder.ForEach(i => i.UUId = "");
            travelBatchOrder.ForEach(i => i.MatchResult = "");
            LoggerFactory.Instance.Logger_Info("插入实体：" + JsonConvert.SerializeObject(travelBatchOrder), "TravelOrderImportService");
            //保存机票信息
            result = TravelBatchOrderRep.Insert(travelBatchOrder);
            if (result < 1)
            {
                this.Result.Data = "机票信息保存失败，请联系相关人员!";
                return false;
            }

            //写入队列
            _manageRiskModels.Foreach(item =>
            {

                if (0 < item.Value.Count)
                {
                    var enterprise = _enterpriseWhiteLists.FirstOrDefault(t => t.EnterpriseWhiteListID == item.Key);

                    //写入实体
                    var manageRiskModel = new ManageRiskModel()
                    {
                        baseRiskModelList = item.Value,
                        PayCenterCode = Parameter.PayCenterCode,
                        PayCenterName = Parameter.PayCenterName,
                        UserFactoringId = (int)_userFactoring.UserFactoringId,
                        FactoringName = _userFactoring.FactoringName,
                        FactoringEmail = _userFactoring.FactoringEmail,
                        FactoringReapalNo = _userFactoring.FactoringReapalNo,
                        InterestRate = _userFactoring.InterestRate ?? 0,
                        FactoringInterestRate = _userAccount.FactoringInterestRate ?? 0,
                        AccountPeriod = null != enterprise ? enterprise.AccountPeriod : "",
                        EnterpriseID = item.Key,
                        EnterpriseName = null != enterprise ? enterprise.EnterpriseName : "",
                        UserName = _userAccount.UserName,
                        BackReapalNo = _userAccount.ReapalMerchantId,
                        TravelBatchId = item.Value.FirstOrDefault().TravelBatchId,

                    };

                    manageRiskModel.baseRiskModelList = item.Value;

                    new RabbitMQClient().SendMessage(JsonConvert.SerializeObject(manageRiskModel));
                }
            });

            //写入缓存
            var ticketNums =  travelBatchOrder.Select(t => t.TicketNo).ToList();

            var list = GetCacheTickets();

            list.AddRange(ticketNums);

            Cache.Put(strKey, JsonConvert.SerializeObject(list));


            return true;
        }

        /// <summary>
        /// 缓存中获取批号
        /// </summary>
        /// <returns></returns>
        private List<string> GetCacheTickets()
        {

            var cacheValue = Cache.Get(strKey).ToString();
            var list = new List<string>();
            try
            {
                  list = JsonConvert.DeserializeObject<List<string>>(cacheValue);

            }
            catch (Exception e)
            {

            }
            finally
            {
                if (null == list)
                    list = new List<string>();
            }
           

            return list;
        }
    }
}
