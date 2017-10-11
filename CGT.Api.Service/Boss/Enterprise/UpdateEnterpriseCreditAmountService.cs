using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.DDD.Logger;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.Enterprise
{
    public class UpdateEnterpriseCreditAmountService : ApiBase<RequestUpdateEnterpriseCreditAmount>
    {
        #region 注入

        /// <summary>
        /// 企业白名单仓储类
        /// </summary>
        public EnterpriseWhiteRep EnterpriseWhiteRep { get; set; }


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
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod()
        {

            int num= EnterpriseWhiteRep.UpdateEnterpriseCreditAmount(Parameter.EnterpriseId,Parameter.CreditAmount*10000);
            if (num>0)
            {
                LoggerFactory.Instance.Logger_Info("企业：" +Parameter.EnterpriseId.ToString()+"增加授信额度："+(Parameter.CreditAmount*10000).ToString(), "UpdateEnterpriseCreditAmountService");

                Result.Data = num;
                
            }
            else
            {
                
                Result.Message = "企业增额失败";
                Result.IsSuccess = false;
            }
        }
    }
}
