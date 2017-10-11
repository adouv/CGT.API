using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using CGT.DDD.Logger;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    /// <summary>
    /// 批次订单详情仓储类
    /// </summary>
    public class TravelBatchOrderRep : BaseRep
    {
        /// <summary>
        /// 修改批次详情表
        /// </summary>
        /// <param name="TravelBatchId">系统批次号（条件）</param>
        /// <param name="EnterpriseId">企业编号（条件）</param>
        /// <param name="strTicketNo">订单集合（条件）</param>
        /// <param name="strNotTicketNo">除此订单集合（条件）</param>
        /// <param name="WhiteResultState">白名单验证状态 0失败 1成功</param>
        /// <param name="BlackResultState">黑屏验证状态 0失败 1成功</param>
        /// <param name="UUid">小何批次号</param>
        /// <param name="RegisterStatus">小何接口注册状态 0失败 1成功 </param>
        /// <param name="CheckStatus">小何接口验证状态 0失败 1成功</param>
        /// <param name="EtermStatus">黑屏接口验证状态 0失败 1成功</param>
        /// <param name="strMatchResult">匹配失败原因</param>
        /// <returns></returns>
        public int UpdateTravelBatchOrder(string TravelBatchId, long EnterpriseId, string strTicketNo, string strNotTicketNo, int WhiteResultState, int BlackResultState, string UUId, int RegisterStatus, int CheckStatus, int EtermStatus, string strMatchResult)
        {
            #region sql
            string wherestr = string.Empty;
            string setstr = string.Empty;
            #region 查询条件

            if (!string.IsNullOrWhiteSpace(TravelBatchId))
            {
                wherestr += " AND TravelBatchId ='" + TravelBatchId + "'";
            }
            if (EnterpriseId > 0)
            {
                wherestr += " AND EnterpriseId =" + EnterpriseId;
            }
            if (!string.IsNullOrWhiteSpace(strTicketNo))
            {
                wherestr += " AND TicketNo IN (" + strTicketNo + ")";
            }
            if (!string.IsNullOrWhiteSpace(strNotTicketNo))
            {
                wherestr += " AND TicketNo NOT IN (" + strNotTicketNo + ")";
            }

            #endregion

            #region 修改内容

            if (WhiteResultState > 0)
            {
                setstr += "WhiteResultState= " + WhiteResultState + ",";
            }
            if (BlackResultState > 0)
            {
                setstr += "BlackResultState=" + BlackResultState + ",";
            }
            if (!string.IsNullOrWhiteSpace(UUId))
            {
                setstr += "UUId='" + UUId + "',";
            }
            if (RegisterStatus > 0)
            {
                setstr += "RegisterStatus=" + RegisterStatus + ",";
            }
            if (CheckStatus > 0)
            {
                setstr += "CheckStatus=" + CheckStatus + ",";
            }
            if (EtermStatus > 0)
            {
                setstr += "EtermStatus=" + EtermStatus + ",";
            }
            if (!string.IsNullOrWhiteSpace(strMatchResult))
            {
                setstr += "MatchResult='" + strMatchResult + "',";
            }
            if (!string.IsNullOrWhiteSpace(setstr))
            {
                setstr = setstr.Substring(0, setstr.Length - 1);
            }

            #endregion

            string sql = string.Format(@"
                 SET {0}
WHERE   1 = 1 {1}
            ", setstr, wherestr);
            #endregion
            var data = CgtTravelDB.GetInstance().Update<TravelBatchOrder>(sql);
            return data;
        }


        

        /// <summary>
        /// 批量录入订单 todo 1. 事务，2. values ()()()()
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public int InsertTravelOrders(List<TravelBatchOrder> orders)
        {

            try
            {
               //return  CgtTravelDB.BulkInsert<TravelBatchOrder>("TravelBatchOrder", orders);
                return this.BulkInsert(orders, CgtTravelDB.GetInstance());

            }
            catch (Exception ex)
            {

                return 0;
            }

        }
        /// <summary>
        /// 保存批次数据
        /// </summary>
        /// <param name="modeles"></param>
        /// <returns></returns>
        public int Insert(List<TravelBatchOrder> modeles)
        {
            try
            {
                // todo  1. transction，2 values ()()()
                using (var db = CgtTravelDB.GetInstance())
                {

                    Parallel.ForEach(modeles, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, item => {

                        db.Insert(item);
                    });

                }

            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex, "TravelOrderImportService");
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 获取批次详情信息集合
        /// </summary>
        /// <param name="UUId">小何批次号</param>
        /// <returns></returns>
        public List<TravelBatchOrder> getManageRiskModelByUUId(string UUId)
        {
            string sql = string.Format(@"
               select * from TravelBatchOrder WITH (NOLOCK)
                WHERE UUId='{0}'
            ", UUId);
            var db = CgtTravelDB.GetInstance();
            return db.Fetch<TravelBatchOrder>(sql);
        }


    }
}
