using CGT.Api.DTO;
using CGT.Api.DTO.Boss.TravelBill;
using CGT.Entity.CgtTravelModel;
using CGT.PetaPoco.Repositories.Cgt;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.Api.Service.Boss.TravelBill {
    /// <summary>
    /// 金主还款确认操作
    /// </summary>
    public class RepayTravelBillByGoldService : ApiBase<RequestRepayTravelBillByGold> {
        private object thisLock = new object();
        #region 注入服务
        public UserAccountRep userAccountRep { get; set; }

        public BillRep billRep { get; set; }

        public BillEveryDayRep billEveryDayRep { get; set; }
                                     
        public EnterpriseOrderRep enterpriseOrderRep { get; set; }

        #endregion
        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json) {
            base.SetData(json);
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate() {
            base.Validate();
            if (Parameter.billId <= 0) {
                throw new AggregateException("差旅订单不存在！");
            }
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            //查询所有日账单
            var list = enterpriseOrderRep.GetBillEveryDaysListByEnterpriseIdsBill(Parameter.billId, null);
            if (!list.Any()) {
                throw new AggregateException("日账单不存在！");
            }
            //修改月账单状态和已还款金额
            var res = billRep.UpdateBillByBillid(Parameter.billId, 1);
            //修改日账单状态
            var billordersId = "Gl" + DateTime.Now.ToString("yyyyMMddhhmmss");
            var resday = billEveryDayRep.UpdateBillEveryDaysByBillid(Parameter.billId, null, billordersId);
            //修改差旅订单还款状态
            var resorder = new EnterpriseOrderRep().UpdateEnterpriseOrderlistByBillEveryDayId(list, 4);
            //更新授信余额
            var limitresult = LimitAmount(list, billordersId);
            //判断更新是否成功
            if (res > 0 && resday > 0) {
                if (!limitresult.Result) {
                    throw new AggregateException("提前还款成功,恢复授信金额失败！");
                }
                Result.Data = resday;
            }
            else {
                throw new AggregateException("账单数据更新失败！");
            }
        }
        /// <summary>
        /// 授信余额恢复
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LimitAmount(List<BillEveryDay> list, string batchordersId) {
            var result = true;
            await Task.Run(() => {
                lock (thisLock) {
                    try {
                        var res = enterpriseOrderRep.UpdateEnterpriseBalance(list);
                        if (res == 0) {
                            result = false;
                            DDD.Logger.LoggerFactory.Instance.Logger_Info(batchordersId + "|" + res, "LimitError");
                        }
                    }
                    catch (Exception ex) {
                        result = false;
                        DDD.Logger.LoggerFactory.Instance.Logger_Info(batchordersId + "|" + ex.Message, "LimitError");
                    }
                }
            });
            return result;
        }
    }
}
