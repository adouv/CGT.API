using CGT.Entity.CgtModel;
using CGT.Entity.CgtTravelModel;
using PetaPoco.NetCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CGT.PetaPoco.Repositories.CgtTravel
{
    public class EnterpriseTempoaryRep
    {

        static Object locker = new Object();

        /// <summary>
        /// 获取用户上传企业信息
        /// </summary>
        /// <param name="pageindex">当前页面</param>
        /// <param name="pagesize">页面尺寸</param>
        /// <param name="userid">BOSS用户ID</param>
        /// <returns></returns>
        public Page<EnterpriseTempoary> GetPageList(int pageindex, int pagesize, long userid)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                return db.Page<EnterpriseTempoary>(pageindex, pagesize, "select * from EnterpriseTempoary where UserId=@0 ORDER BY ID desc", userid);
            }
        }

        ///<summary>
        ///批量插入临时企业信息 
        ///</summary>
        ///<param name = "list" > Excel List</param>
        ///<param name = "UserId" > BOSS 用户登录ID</param>
        ///<returns></returns>
        public dynamic QuestSaveList(List<EnterpriseTempoary> list, long UserId)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                try
                {
                    db.BeginTransaction();
                    foreach (EnterpriseTempoary item in list)
                    {
                        item.UserId = UserId;
                        var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM EnterpriseTempoary  with (nolock) where EnterpriseName like @0", item.EnterpriseName + "%");
                        if (count == 0)
                        {
                            db.Insert(item);
                        }
                        else
                        {
                            item.EnterpriseName = item.EnterpriseName + count;
                            db.Insert(item);
                        }
                    }
                    db.CompleteTransaction();
                    return new { errorcode = 0, msg = "文件保存成功" };
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    throw new Exception(ex.Message);
                }
            }
        }


        /// <summary>
        /// 更改状态为白名单
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public dynamic UpdateEnterpriseTempoaryStatus(List<EnterpriseTempoary> list)
        {
            lock (locker)
            {
                using (var db = CgtTravelDB.GetInstance())
                {
                    int index = 0;
                    try
                    {
                        db.BeginTransaction();
                        foreach (var item in list)
                        {
                            var db_item = db.Single<EnterpriseTempoary>(item.ID);
                            if (item.AuditStatus == null)
                            {
                                if (item.CreditAmount > 0)
                                {
                                    var DistUser = db.FirstOrDefault<UserAccount>("select * from cgt.dbo.UserAccount where UserName=@0", item.DistributionAccount.Trim());
                                    if (DistUser != null)
                                    {
                                        var model = new EnterpriseWhiteList
                                        {
                                            EnterpriseName = item.EnterpriseName,
                                            PayCenterCode = DistUser.PayCenterCode,
                                            PayCenterName = DistUser.UserCompanyName,
                                            AccountNumber = item.AccountNumber,
                                            AccountPeriod = item.AccountPeriod,
                                            TravelServiceAgreementURL = item.TravelServiceAgreementURL,
                                            CreditAmount = item.CreditAmount != null ? (decimal)item.CreditAmount : 0,
                                            EnterpriseStatue = 1,
                                            CreateTime = DateTime.Now,
                                            ModifiedTime = DateTime.Now,
                                            TableId = Guid.NewGuid(),
                                            AccountBalance = item.CreditAmount,

                                            CreditMonthAmount = item.CreditAmount,
                                            MonthStatue = 0,
                                            FreezeWay = 1

                                        };
                                        db.Insert(model);
                                        if (model.EnterpriseWhiteListID > 0)
                                        {
                                            db.Update<EnterpriseTempoary>("set CreditAmount=@0 ,AuditStatus=1 where ID=@1", item.CreditAmount, item.ID);
                                        }
                                        index = index + 1;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    db.Update<EnterpriseTempoary>("set CreditAmount=@0 ,AuditStatus=0 where ID=@1", item.CreditAmount, item.ID);
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }
                        db.CompleteTransaction();
                        if (index > 0)
                        {
                            return new { errorcode = 0, msg = "信息更新成功" };
                        }
                        else
                        {
                            return new { errorcode = -1, msg = "信息更新失败" };
                        }
                    }
                    catch (Exception ex)
                    {
                        db.AbortTransaction();
                        return new { errorcode = -2, msg = ex.Message };
                    }
                }
            }
        }

        /// <summary>
        /// 保存用户协议
        /// </summary>
        /// <param name="userId">BOSS用户ID</param>
        /// <param name="enterpriseID">企业ID</param>
        /// <param name="httpfile">相对Excel保存地址</param>
        /// <returns></returns>
        public int SaveEnterpriseProtocol(long userId, long enterpriseID, string httpfile)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                return db.Update<EnterpriseTempoary>("set TravelServiceAgreementURL=@0 where ID=@1 and UserId=@2", httpfile, enterpriseID, userId);
            }
        }

        /// <summary>
        /// 获取所有的企业名称列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<EnterpriseTempoary> GetDistEnterpriseList(long userid)
        {
            using (var db = CgtTravelDB.GetInstance())
            {
                return db.Query<EnterpriseTempoary>("select * from EnterpriseTempoary where UserId=@0 and AuditStatus is null and TravelServiceAgreementURL is not null", userid).ToList();
            }
        }
    }
}
