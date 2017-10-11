using CGT.Api.DTO;
using CGT.Api.DTO.Manage.Remoney;
using CGT.DDD.Caching;
using CGT.DDD.Logger;
using CGT.PetaPoco.Repositories.CgtTravel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.Api.Service.Manage.Remoney {
    /// <summary>
    /// 批量返现回掉通知通知
    /// </summary>
    public class RemoneyNotifyService {
        private object thisLock = new object();

        EnterpriseWhiteRep enterpriseWhiteRep = new EnterpriseWhiteRep();

        TravelBatchRep travelBatchRep = new TravelBatchRep();

        EnterpriseOrderRep enterpriseOrderRep = new EnterpriseOrderRep();

        public ICache Cache = new RedisCache();
        private string strKey = "excel_ticket_noes";
        /// <summary>
        ///回调业务处理
        /// </summary>
        /// <returns></returns>
        public ResponseMessageModel Execute(RequestRemoneyModel remoneyView) {
            //遍历成功订单号集合
            var orderidlist = remoneyView.list.Select(i => i.orderId).ToList();
            var ordersuccessList = remoneyView.list.Where(i => i.isSuccess == true).Select(i => i.orderId).ToList();
            if (ordersuccessList.Any()) {
                //授信余额修改
                var successmoney = remoneyView.list.Sum(i => decimal.Parse(i.amount));
                if (LimitAmount(1, successmoney, int.Parse(remoneyView.enterpriseId), remoneyView.batchNum).Result) {
                    LoggerFactory.Instance.Logger_Info(remoneyView.batchNum + "|" + successmoney, "travelBatchLimitAmountError");
                }
                //批量修改返现成功订单状态
                enterpriseOrderRep.UpdateEnterpriseOrderBatchBackState(ordersuccessList);
            }
            //修改批次返现状态为完成
            travelBatchRep.UpdateTravelBatchOrderTranslationStateByTravelBatchId(remoneyView.batchNum, 2);
            //更新缓存
            ModifyTicketCache(orderidlist);
            return new ResponseMessageModel() {
                IsSuccess = true
            };
        }
        /// <summary>
        /// 授信余额操作 0加 1减(操作余额加减逻辑)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LimitAmount(int Type, decimal Amount, int EnterpriseId, string TravelBatchId) {
            var result = false;
            await Task.Run(() => {
                lock (thisLock) {
                    try {
                        enterpriseWhiteRep.UpdateEnterpriseWhiteListAccountBalance(Amount, EnterpriseId, Type);
                        result = true;
                    }
                    catch (Exception ex) {
                        result = false;
                        DDD.Logger.LoggerFactory.Instance.Logger_Info(TravelBatchId + "|" + ex.Message, "TravelBatchIdLimitError");
                    }
                }
            });
            return result;
        }
        /// <summary>
        /// 更新Redis缓存
        /// </summary>
        public void ModifyTicketCache(List<string> alllist) {
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
            var ticketNums = alllist;
            Cache.Put(strKey, JsonConvert.SerializeObject(list.Except(ticketNums).ToList()));
        }
    }
}
