
using CGT.Entity.CgtInsuranceModel;
using CGT.Entity.CgtModel;
using PetaPoco.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT.PetaPoco.Repositories.Insurance
{
    /// <summary>
    /// 保险订单仓储
    /// </summary>
    public class InsuranceOrderRep
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Insert(InsuranceOrder model, List<InsurancedPerson> list,out string msg)
        {
            using (var db = CgtInsuranceDB.GetInstance())
            {
                int index = 0;
                int count = 0;
                db.BeginTransaction();
                try
                {
                    model.CreateTime = DateTime.Now;
                    var id= db.Insert(model);
                    foreach (var item in list)
                    {
                        item.InsuredOrderId =Convert.ToInt64(id);
                        count += item.ApplyNum;
                        db.Insert(item);
                        index++;
                    }
                    index++;
                   var user= db.SingleOrDefault<InsuranceUser>("select * from InsuranceUser where UserId =@0", model.UserId);
                   var remiancount= user.RemainingCount - count;
                    if (remiancount<0)
                    {
                        db.AbortTransaction();
                        msg = "超过月限额数量";
                        return -1;

                    }
                   db.Execute("update InsuranceUser set RemainingCount =@1 where UserId =@0", model.UserId, remiancount);
                   db.CompleteTransaction();
                }
                catch (Exception ex)
                {
                    db.AbortTransaction();
                    index = -1;
                    throw ex;
                }
                msg = "";
                return index;
            }
        }
        /// <summary>
        /// 查询保险订单（分页）
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Page<dynamic> PageInsuranceOrder(int pageindex, int pagesize, InsuranceOrder model, DateTime? BeginBillDate, DateTime? EndBillDate)
        {
            string sql = GetsqlData(model, BeginBillDate, EndBillDate);
            return CgtInsuranceDB.GetInstance().Page<dynamic>(pageindex, pagesize, sql, model.OthOrderCode, model.UserId,
                  Convert.ToDateTime(BeginBillDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(EndBillDate).ToString("yyyy-MM-dd") + " 23:59:59"
                );
        }
        /// <summary>
        /// 查询保险订单（不分页）
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<InsuranceOrder> QueryInsuranceOrder(InsuranceOrder model, DateTime? BeginBillDate, DateTime? EndBillDate)
        {
            string sql = GetsqlData(model,BeginBillDate,EndBillDate);
            return CgtInsuranceDB.GetInstance().Query<InsuranceOrder>(sql, model.OthOrderCode, model.UserId,
                Convert.ToDateTime(BeginBillDate).ToString("yyyy-MM-dd HH:mm:ss"),
                    Convert.ToDateTime(EndBillDate).ToString("yyyy-MM-dd") + " 23:59:59"
                ).ToList();
        }

        private string GetsqlData(InsuranceOrder model, DateTime? BeginBillDate, DateTime? EndBillDate)
        {
            string wherestr = "";
            if (!string.IsNullOrEmpty(model.OthOrderCode))
            {
                wherestr += " AND a.OthOrderCode = @0";
            }
            if (model.UserId.HasValue)
            {
                wherestr += " AND a.UserId=@1";
            }
            if (BeginBillDate.HasValue)
            {
                wherestr += " AND a.CreateTime >= @2";
            }
            if (EndBillDate.HasValue)
            {
                wherestr += " AND a.CreateTime <= @3";
            }
            string sql = string.Format(@"select a.*,b.InsuredName,b.ApplyNum,b.IdentifyNumber as InsurdIdentifyNumber, b.Mobile as InsurdMobile,
     ( CASE WHEN a.IdentifyType = 01 THEN '居民身份证'
       WHEN a.IdentifyType =02 THEN '居民户口簿'
       WHEN a.IdentifyType =03 THEN '中国因公护照'
       WHEN a.IdentifyType =04 THEN '军官证/警官证'
       WHEN a.IdentifyType =05 THEN '驾驶证'
       WHEN a.IdentifyType =06 THEN '台湾居民来往大陆通行证'
       WHEN a.IdentifyType =07 THEN '组织机构代码证'
       WHEN a.IdentifyType =08 THEN '士兵证'
       WHEN a.IdentifyType =10 THEN '外国人永久居留证'
       WHEN a.IdentifyType =12 THEN '香港身份证'
	   WHEN a.IdentifyType =14 THEN '中国因私护照'
       WHEN a.IdentifyType =15 THEN '往来港澳通行证'
       WHEN a.IdentifyType =16 THEN '大陆居民往来台湾通行证'
       WHEN a.IdentifyType =17 THEN '军官离退休证'
       WHEN a.IdentifyType =18 THEN '港澳居民来往内地通行证'
       WHEN a.IdentifyType =19 THEN '澳门身份证'
       WHEN a.IdentifyType =20 THEN '台湾身份证'
       WHEN a.IdentifyType =21 THEN '外国护照'
               ELSE NULL
          END ) AS IdentifyTypeName,
     ( CASE WHEN b.IdentifyType = 01 THEN '居民身份证'
       WHEN b.IdentifyType =02 THEN '居民户口簿'
       WHEN b.IdentifyType =03 THEN '中国因公护照'
       WHEN b.IdentifyType =04 THEN '军官证/警官证'
       WHEN b.IdentifyType =05 THEN '驾驶证'
       WHEN b.IdentifyType =06 THEN '台湾居民来往大陆通行证'
       WHEN b.IdentifyType =07 THEN '组织机构代码证'
       WHEN b.IdentifyType =08 THEN '士兵证'
       WHEN b.IdentifyType =10 THEN '外国人永久居留证'
       WHEN b.IdentifyType =12 THEN '香港身份证'
	   WHEN b.IdentifyType =14 THEN '中国因私护照'
       WHEN b.IdentifyType =15 THEN '往来港澳通行证'
       WHEN b.IdentifyType =16 THEN '大陆居民往来台湾通行证'
       WHEN b.IdentifyType =17 THEN '军官离退休证'
       WHEN b.IdentifyType =18 THEN '港澳居民来往内地通行证'
       WHEN b.IdentifyType =19 THEN '澳门身份证'
       WHEN b.IdentifyType =20 THEN '台湾身份证'
       WHEN b.IdentifyType =21 THEN '外国护照'
               ELSE NULL
          END ) AS InsurdIdentifyTypeName,
   ( CASE WHEN b.Relation = 01 THEN '本人'
       WHEN b.Relation =10 THEN '配偶'
       WHEN b.Relation =11 THEN '丈夫'
       WHEN b.Relation =12 THEN '妻子'
       WHEN b.Relation =20 THEN '儿子'
       WHEN b.Relation =30 THEN '女儿'
       WHEN b.Relation =40 THEN '儿女'
       WHEN b.Relation =50 THEN '父母'
       WHEN b.Relation =51 THEN '父亲'
       WHEN b.Relation =52 THEN '母亲'
	   WHEN b.Relation =10 THEN '配偶'
       WHEN b.Relation =11 THEN '丈夫'
       WHEN b.Relation =12 THEN '妻子'
       WHEN b.Relation =20 THEN '儿子'
       WHEN b.Relation =30 THEN '女儿'
       WHEN b.Relation =40 THEN '儿女'
       WHEN b.Relation =50 THEN '父母'
       WHEN b.Relation =51 THEN '父亲'
       WHEN b.Relation =52 THEN '母亲'
       WHEN b.Relation =61 THEN '孙子、孙女与祖父、母'
       WHEN b.Relation =62 THEN '外孙、外孙女与外祖父、母'
       WHEN b.Relation =63 THEN '弟、妹与兄、姐'
       WHEN b.Relation =64 THEN '和被保监护人关系密切的其他亲属、朋友'
       WHEN b.Relation =66 THEN '被监护人住所地的居民委员会、村民委员会或者民政部门'
       WHEN b.Relation =67 THEN '精神病院等医疗机构'
       WHEN b.Relation =68 THEN '社会福利机构'
       WHEN b.Relation =80 THEN '单位'
       WHEN b.Relation =81 THEN '学校'
               ELSE NULL
          END ) AS RelationName
from InsuranceOrder a WITH (NOLOCK) left join [dbo].[InsurancedPerson] b WITH (NOLOCK) 
                   on a.ID = b.InsuredOrderId where 1=1
      {0}", wherestr);
            return sql;
        }
    }
}

    

