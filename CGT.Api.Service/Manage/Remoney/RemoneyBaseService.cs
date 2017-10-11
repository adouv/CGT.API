using CGT.DDD.Caching;
using CGT.DDD.Config;
using CGT.DDD.Logger;
using CGT.Entity.CgtTravelModel;
using CGT.Event.Model.Manage;
using CGT.PayCenter.Service;
using CGT.PetaPoco.Repositories.CgtTravel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.Api.Service.Manage.Remoney {
    /// <summary>
    /// 返现操作基类
    /// </summary>
    public abstract class RemoneyBaseService {
        internal ManageRiskModel ManageRiskModelBase { get; set; }

        public TravelBatchRep travelBatchRep = new TravelBatchRep();
        public EnterpriseOrderRep enterpriseOrderRep = new EnterpriseOrderRep();
        public EnterpriseOrderRiskRep enterpriseOrderRiskRep = new EnterpriseOrderRiskRep();
        public ICache Cache = new RedisCache();
        private string strKey = "excel_ticket_noes";
        /// <summary>
        /// 重构函数
        /// </summary>
        public RemoneyBaseService(ManageRiskModel ManageRiskModel) {
            ManageRiskModelBase = ManageRiskModel;
        }
        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate() {
            if (ManageRiskModelBase.baseRiskModelList.Count == 0) {
                travelBatchRep.UpdateTravelBatchOrderListByTravelBatchId(ManageRiskModelBase.TravelBatchId, (int)TranslationState.成功, 0, 0);
                throw new AggregateException("没有任何风控数据！");
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 添加订单和订单风控数据
        /// </summary>
        public void AddEnterpriseOrderAndRiksListPay(int ReviewState) {
            var EnterpriseOrderList = new List<EnterpriseOrder>();
            var EnterpriseOrderRiskList = new List<EnterpriseOrderRisk>();
            var financingOrderlist = new List<FinancingOrder>();
            foreach (var baseRiskModel in ManageRiskModelBase.baseRiskModelList) {
                #region 差旅订单实体
                var enterpriseOrderModel = new EnterpriseOrder() {
                    OrderId = baseRiskModel.TicketNum,
                    TicketNo = baseRiskModel.TicketNum,
                    PassengerName = baseRiskModel.PersonName,
                    DepartureTime = baseRiskModel.DepartureTime,
                    DepartureCity = baseRiskModel.DepartCode,
                    ReachCity = baseRiskModel.ArriveCode,
                    Pnr = baseRiskModel.PNR,
                    TicketAmount = baseRiskModel.OrderAmount,
                    TicketTime = baseRiskModel.TicketTime,
                    Airline = baseRiskModel.FlightNo.Substring(0, 2),
                    FlightNo = baseRiskModel.FlightNo,
                    PayCenterCode = ManageRiskModelBase.PayCenterCode,
                    PayCenterName = ManageRiskModelBase.PayCenterName,
                    AdvancesId = Convert.ToInt32(ManageRiskModelBase.UserFactoringId),
                    AdvancesName = ManageRiskModelBase.FactoringName,
                    AdvancesEmail = ManageRiskModelBase.FactoringEmail,
                    AdvancesReapalNo = ManageRiskModelBase.FactoringReapalNo,
                    EnterpriseWhiteListID = (int)baseRiskModel.EnterpriseID,
                    CashBackEmial = ManageRiskModelBase.UserName,
                    CashBackReapalNo = ManageRiskModelBase.BackReapalNo,
                    BillDateTime = GetBilldateByMonth(DateTime.Now, int.Parse(ManageRiskModelBase.AccountPeriod)).Item2,
                    BillDate = GetBilldateByMonth(DateTime.Now, int.Parse(ManageRiskModelBase.AccountPeriod)).Item1,
                    FactoringInterestRate = ManageRiskModelBase.InterestRate,
                    UserInterestRate = ManageRiskModelBase.FactoringInterestRate,
                    InsuredAmount = baseRiskModel.OrderAmount * 0.001m,
                    BackTime = null,
                    //====================
                    EtermStatus = "",
                    IsBackStatus = 1,
                    NoBackReason = "",
                    SuspendedServiceUrl = "",
                    RepaymentStatus = 0,
                    //====================
                    BackStatus = 0,
                    CreateTime = DateTime.Now,
                    OrderTravelBatchId = ManageRiskModelBase.TravelBatchId,
                    OrderEnterpriseName = baseRiskModel.EnterpriseName,
                    UserInterest = baseRiskModel.OrderAmount * ManageRiskModelBase.FactoringInterestRate
                };
                EnterpriseOrderList.Add(enterpriseOrderModel);
                #endregion
                #region 差旅订单风控实体
                var enterpriseOrderRiskModel = new EnterpriseOrderRisk() {
                    TravelBatchId = ManageRiskModelBase.TravelBatchId,
                    EOrderId = baseRiskModel.TicketNum,
                    TravelRiskType = 0,
                    TravelRiskState = baseRiskModel.FailReason == "" ? 1 : 0,
                    RiskCreateTime = DateTime.Now,
                    ReviewState = ReviewState,
                    ReviewUserId = 0,
                    ReviewTime = DateTime.Now,
                    FailReason = baseRiskModel.FailReason,
                    RefuseReason = "",
                };
                EnterpriseOrderRiskList.Add(enterpriseOrderRiskModel);
                #endregion
                #region  支付体系实体
                var financingOrder = new FinancingOrder() {
                    airline = baseRiskModel.FlightNo.Substring(0, 2),
                    departureCity = baseRiskModel.DepartCode,
                    departureTime = baseRiskModel.DepartureTime.ToString(),
                    flightNo = baseRiskModel.FlightNo,
                    orderId = baseRiskModel.TicketNum,
                    orderPrice = baseRiskModel.OrderAmount.ToString(),
                    passengerName = baseRiskModel.PersonName,
                    passengerNo = "",
                    pnr = baseRiskModel.PNR,
                    reachCity = baseRiskModel.ArriveCode,
                    ticketNo = baseRiskModel.TicketNum
                };
                financingOrderlist.Add(financingOrder);
                #endregion
            }
            enterpriseOrderRep.AddEnterpriseOrders(EnterpriseOrderList);
            enterpriseOrderRiskRep.AddEnterpriseOrderRisks(EnterpriseOrderRiskList);
            //用户体系保理返现接口
            var RemoneyNotifyUrl = JsonConfig.JsonRead("RemoneyNotifyUrl", "CgtPayApi") + "/api/manage/remoney/notify";
            var travelBatchRemoney = new TravelBatchRemoneyProcessor(
                ManageRiskModelBase.UserFactoringId.ToString(),
                ManageRiskModelBase.FactoringEmail,
                ManageRiskModelBase.FactoringReapalNo,
                ManageRiskModelBase.PayCenterCode.ToString(),
                ManageRiskModelBase.UserName,
                ManageRiskModelBase.BackReapalNo,
                financingOrderlist,
                "企业批量返现订单",
                "1.1.1.1",
                GetBilldateByMonth(DateTime.Now, int.Parse(ManageRiskModelBase.AccountPeriod)).Item1.ToString("yyyy-MM-dd"),
                ManageRiskModelBase.EnterpriseID.ToString(),
                GetBilldateByMonth(DateTime.Now, int.Parse(ManageRiskModelBase.AccountPeriod)).Item2.ToString(),
                ManageRiskModelBase.FactoringName,
                ManageRiskModelBase.PayCenterName,
                ManageRiskModelBase.EnterpriseName,
                ManageRiskModelBase.TravelBatchId,
                RemoneyNotifyUrl
            );
            var travelBatchRemoneyResult = travelBatchRemoney.Execute();
            if (!travelBatchRemoneyResult.Success) {
                LoggerFactory.Instance.Logger_Info(travelBatchRemoneyResult.Message.ToString(), "travelBatchRemoneyResultError");
            }
        }
        /// 按自然月生成账期和实际账期天数
        /// </summary>
        /// <param name="date"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Tuple<DateTime, int> GetBilldateByMonth(DateTime date, int number) {
            //账期规则算出月份和天数
            var month = number / 30;
            var day = number % 30;
            DateTime d1 = new DateTime(date.Year, date.Month, 1);
            DateTime d2 = d1.AddMonths(month + 1).AddDays(-1);
            DateTime dateres = d1.AddMonths(month).AddDays(day).AddDays(-1);
            if (date.AddMonths(month + 1).Month == dateres.Month) {
                dateres = d2;
            }
            var datevaule = (int)(dateres.Date - d1.Date).TotalDays + 1;
            return new Tuple<DateTime, int>(dateres, datevaule);
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        public void ModifyTicketCache() {
            //获取缓存
            var cacheValue = Cache.Get(strKey).ToString();
            var list = new List<string>();
            try {
                list = JsonConvert.DeserializeObject<List<string>>(cacheValue);
            }
            catch (Exception ex) {
                if (null == list)
                    list = new List<string>();
            }
            //写入缓存
            var ticketNums = ManageRiskModelBase.baseRiskModelList.Select(t => t.TicketNum).ToList();
            Cache.Put(strKey, JsonConvert.SerializeObject(list.Except(ticketNums).ToList()));
        }
        /// <summary>
        /// 审核状态枚举
        /// </summary>
        public enum ReviewState {
            审核通过 = 0,
            进入审核 = 1,
            拒绝审核 = 2
        }
        /// <summary>
        /// 流程状态枚举
        /// </summary>
        public enum TranslationState {
            验证中 = 0,
            返现中 = 1,
            成功 = 2
        }
    }
}
