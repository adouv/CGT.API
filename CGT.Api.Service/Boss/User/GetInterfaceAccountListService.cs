using CGT.Api.DTO;
using CGT.Api.DTO.Boss.User;
using CGT.Api.DTO.Boss.User.Request;
using CGT.Entity.CgtModel;
using CGT.PetaPoco.Repositories.Cgt;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.User {
    /// <summary>
    /// 代理列表
    /// </summary>
    public class GetInterfaceAccountListService : ApiBase<RequestGetInterfaceAccountList> {
        #region 注入服务
        ///已在ApiBase注入

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
        }
        /// <summary>
        /// 业务逻辑
        /// </summary>
        protected override void ExecuteMethod() {
            var data = interfaceAccountRep.GetInterfaceAccountList(Parameter.PageIndex,Parameter.PageSize);
        }
    }
}
