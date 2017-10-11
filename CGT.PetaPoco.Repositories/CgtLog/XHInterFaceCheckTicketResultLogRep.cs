using CGT.Entity.CgtLogModel;
using System;
using System.Collections.Generic;
using System.Text;
using PetaPoco.NetCore;


namespace CGT.PetaPoco.Repositories.CgtLog
{
    /// <summary>
    /// 小何接口日志仓储类
    /// </summary>
    public class XHInterFaceCheckTicketResultLogRep
    {
        /// <summary>
        /// 添加小何接口日志
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public long AddXHInterFaceCheckTicketResultLog(XHInterFaceCheckTicketResultLog model)
        {
            CgtLogDB.GetInstance().Insert(model);
            return model.Id;
        }

        /// <summary>
        /// 修改小何接口验证结果日志数据
        /// </summary>
        /// <param name="BatchNumber">批次号</param>
        /// <param name="CheckStatus">验证结果：0 超时 1成功</param>
        /// <returns></returns>
        public int UpdateXHInterFaceCheckTicketResultLog(string BatchNumber, int CheckStatus)
        {
            #region sql

            string sql = string.Format(@"
                SET  CheckStatus={0},CheckTime=GETDATE() 
                WHERE BatchNumber='{1}'
            ", CheckStatus, BatchNumber);
            #endregion

            var data = CgtLogDB.GetInstance().Update<XHInterFaceCheckTicketResultLog>(sql);
            return data;
        }

        /// <summary>
        /// 获取差旅风控规则列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        public Page<XHInterFaceCheckTicketResultLog> GetXHInterFaceCheckTicketResultLogList(string StratDate, string EndDate, int pageindex, int pagesize)
        {
            #region sql
            string wherestr = string.Empty;
            if (!string.IsNullOrWhiteSpace(StratDate))
            {
                wherestr += " AND AddTime >= @0";
            }
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                wherestr += " AND AddTime  < @1";
            }

            string sql = string.Format(@"
                                      SELECT [Id]
                                      ,[BatchNumber]
                                      ,[RegisterStatus]
                                      ,[CheckStatus]
                                      ,[TicketNum]
                                      ,[AddTime]
                                      ,[CheckTime]
                                      FROM [dbo].[XHInterFaceCheckTicketResultLog]
                                      WHERE   1 = 1
                                      {0}", wherestr);
            #endregion
            return CgtLogDB.GetInstance().Page<XHInterFaceCheckTicketResultLog>(pageindex, pagesize, sql, StratDate, EndDate);
        }

    }


}
