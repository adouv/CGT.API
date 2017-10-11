using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.PetaPoco.Repositories.CgtTravel;
using System.Collections.Generic;
using System.Linq;


namespace CGT.Api.Service.Boss.Enterprise
{
    /// <summary>
    /// 分销企业商2级联动
    /// </summary>
    public class DistributionEnterpriseService : ApiBase<RequestDistributionEnterprise>
    {
        #region 注入仓储

        public EnterpriseWhiteRep EpRep { get; set; }

        #endregion

        /// <summary>
        /// Api赋值
        /// </summary>
        /// <param name="json"></param>
        public override void SetData(RequestModel json)
        {
            base.SetData(json);
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        protected override void Validate()
        {
            base.Validate();
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        protected override void ExecuteMethod()
        {
            var list = EpRep.EnterpriseInfoList();
            List<dynamic> keylist = (from item in list select (dynamic)new { PayCenterName = item.PayCenterName, PayCenterCode = item.PayCenterCode, UserId = item.UserId, UserName = item.UserName }).Distinct().ToList();

            List<Distruation> DistList = new List<Distruation>();
            var DefaultModel = new Distruation
            {
                PayCenterName = "全部",
                PayCenterCode = null,
                UserId = 0,
                UserName = null,
                EpConstructList = new List<EpConstruct>()
            };
            var DefaultEnterprise = new EpConstruct
            {
                EnterpriseName = "全部",
                EnterpriseWhiteListID = 0
            };
            DefaultModel.EpConstructList.Add(DefaultEnterprise);
            DistList.Add(DefaultModel);
            foreach (var key in keylist)
            {
                var Dist = new Distruation
                {
                    PayCenterName = key.PayCenterName,
                    PayCenterCode = key.PayCenterCode,
                    UserId = key.UserId,
                    UserName = key.UserName,
                    EpConstructList = new List<EpConstruct>()
                };

                var DefaultEP = new EpConstruct
                {
                    EnterpriseName = "全部",
                    EnterpriseWhiteListID = 0
                };
                Dist.EpConstructList.Add(DefaultEP);
                foreach (var item in list)
                {
                    if (item.PayCenterCode == key.PayCenterCode)
                    {
                        Dist.EpConstructList.Add(new EpConstruct { EnterpriseName = item.EnterpriseName, EnterpriseWhiteListID = item.EnterpriseWhiteListID });
                    }
                }
                DistList.Add(Dist);
            }
            this.Result.Data = new { DistruationList = DistList };
        }

        internal class Distruation
        {
            public string PayCenterName { get; set; }

            public string PayCenterCode { get; set; }

            public long UserId { get; set; }

            public string UserName { get; set; }

            public List<EpConstruct> EpConstructList { get; set; }
        }

        internal class EpConstruct
        {
            public string EnterpriseName { get; set; }

            public long EnterpriseWhiteListID { get; set; }
        }
    }

}
