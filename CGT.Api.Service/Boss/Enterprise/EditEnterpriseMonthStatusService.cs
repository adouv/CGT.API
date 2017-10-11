using CGT.Api.DTO;
using CGT.Api.DTO.Boss.Enterprise;
using CGT.PetaPoco.Repositories.CgtTravel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT.Api.Service.Boss.Enterprise {
    /// <summary>
    /// 编辑企业月限额状态
    /// </summary>
    public class EditEnterpriseMonthStatusService : ApiBase<RequestEnterpriseMonthStatue> {
        public EnterpriseWhiteRep enterpriseWhiteRep { get; set; }
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
        protected override void ExecuteMethod() {
            if (this.Parameter.MonthStatue.HasValue) {
                var result = enterpriseWhiteRep.EditEnterpriseWhiteListMonthStatus(new Entity.CgtTravelModel.EnterpriseWhiteList {
                    EnterpriseWhiteListID = this.Parameter.EnterpriseWhiteListID,
                    MonthStatue = this.Parameter.MonthStatue
                });
                if (result < 1) {
                    throw new System.Exception("插入数据库失败");
                }
                this.Result.Data = result;
            }
            else if (this.Parameter.CreditMonthAmount.HasValue) {
                var result = enterpriseWhiteRep.EditEnterpriseWhiteListMonthAmount(new Entity.CgtTravelModel.EnterpriseWhiteList {
                    CreditMonthAmount = this.Parameter.CreditMonthAmount,
                    EnterpriseWhiteListID = this.Parameter.EnterpriseWhiteListID,
                });
                if (result < 1) {
                    throw new System.Exception("插入数据库失败");
                }
                this.Result.Data = result;
            }
            else if (this.Parameter.EnterpriseStatue.HasValue) {
                var result = enterpriseWhiteRep.EditEnterpriseWhiteListEnterpriseStatue(new Entity.CgtTravelModel.EnterpriseWhiteList {
                    EnterpriseStatue = (int)this.Parameter.EnterpriseStatue,
                    EnterpriseWhiteListID = this.Parameter.EnterpriseWhiteListID,
                     ModifiedName = Parameter.ModifyName
                });
                if (result < 1) {
                    throw new System.Exception("插入数据库失败");
                }
                this.Result.Data = result;
            }
            else if (this.Parameter.FreezeWay.HasValue) {
                var result = enterpriseWhiteRep.EditEnterpriseWhiteListFreezeWay(new Entity.CgtTravelModel.EnterpriseWhiteList {
                    FreezeWay = (int)this.Parameter.FreezeWay,
                    EnterpriseWhiteListID = this.Parameter.EnterpriseWhiteListID,
                    ModifiedName = Parameter.ModifyName
                });
                if (result < 1) {
                    throw new System.Exception("插入数据库失败");
                }
                this.Result.Data = result;
            }
            
        }
    }
}
