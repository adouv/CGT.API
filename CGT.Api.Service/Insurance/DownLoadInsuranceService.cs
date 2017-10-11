using System;
using System.Collections.Generic;
using CGT.Api.DTO;
using CGT.PetaPoco.Repositories.Insurance;
using CGT.DDD.Utils;
using CGT.Api.DTO.Insurance.InsuranceOrder.Request;
using CGT.Entity.CgtInsuranceModel;

namespace CGT.Api.Service.Insurance
{
    public class DownLoadInsuranceService : ApiBase<RequestQueryInsuranceOrder>
    {
        #region 注入服务
        public InsuranceOrderRep insuranceOrderRep { get; set; }
        public ExeclHelper Excel { get; set; }
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

        protected override void ExecuteMethod()
        {
            var list = insuranceOrderRep.QueryInsuranceOrder(new Entity.CgtInsuranceModel.InsuranceOrder() {
                UserId = this.Parameter.UserId,
                OthOrderCode =this.Parameter.OthOrderCode,
            },this.Parameter.StartDate,this.Parameter.EndDate);
            List<string> titlelist = new List<string>();
            titlelist.Add("保单号");
            titlelist.Add("第三方订单号");
            titlelist.Add("总保额");
            titlelist.Add("总保费");
            titlelist.Add("保险起期");
            titlelist.Add("保险止期");
            titlelist.Add("出发航班号");
            titlelist.Add("出发时间");
            titlelist.Add("投保人名称");
            titlelist.Add("投保人证件类型");
            titlelist.Add("投保人证件号码");
            titlelist.Add("投保人手机号");
            titlelist.Add("被保人名称");
            titlelist.Add("投保份数");
            titlelist.Add("被保人证件类型");
            titlelist.Add("被保人证件号");
            titlelist.Add("被保人手机号");

            List<string> keylist = new List<string>();
            keylist.Add("PolicyNo");
            keylist.Add("OthOrderCode");
            keylist.Add("TotalAmount");
            keylist.Add("TotalPremium");
            keylist.Add("StartDate");
            keylist.Add("EndDate");
            keylist.Add("FlightNo");
            keylist.Add("FlightDate");
            keylist.Add("AppliName");
            keylist.Add("IdentifyTypeName");
            keylist.Add("IdentifyNumber");
            keylist.Add("Mobile");
            keylist.Add("InsuredName");
            keylist.Add("ApplyNum");
            keylist.Add("InsurdIdentifyTypeName");
            keylist.Add("InsurdIdentifyNumber");
            keylist.Add("InsurdMobile");
            var filebytes = Excel.ExcelExport<InsuranceOrder>(list, titlelist, keylist);
            Result.Data = Convert.ToBase64String(filebytes);
        }
    }
}
