using CGT.DDD.EntityValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CGT.Api.DTO {
    /// <summary>
    /// 统一提交基类
    /// </summary>
    public abstract class RequestBaseModel {
        /// <summary>
        /// 时间戳
        /// </summary>
        [TimesTampAttribute(MessageType.TimesTampField, null, ErrorMessage = "不是有效的时间戳!")]
        public string TimesTamp { get; set; }

        /// <summary>
        /// mac
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }


        /// <summary>
        /// 当前请求的客户端Token
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// 数据验证(是否成功)
        /// 虚属性，子类可以根据自己的逻辑去复写
        /// </summary>
        public virtual bool IsValid {
            get {
                return this.GetRuleViolations() == null ||
                       this.GetRuleViolations().Count() == 0;
            }
        }


        #region Methods
        /// <summary>
        /// 获取验证失败的信息枚举,默认提供了非空验证，派生类可以根据自己的需要去复写这个方法
        /// 个性化验证同样使用yield return返回到IEnumberable列表中
        /// 它使用了简单的迭代器,如果GetRuleViolations有错误则返回迭代列表
        /// </summary> 
        /// <returns></returns>
        public IEnumerable<RuleViolation> GetRuleViolations() {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToArray();

            foreach (var i in properties) {
                var attr = i.GetCustomAttributes();
                foreach (var a in attr) {
                    var val = (a as ValidationAttribute);
                    if (val != null)
                        if (!val.IsValid(i.GetValue(this))) {
                            yield return new RuleViolation(val.ErrorMessage, i.Name);
                        }
                }
            }

        }

        /// <summary>
        /// 得到由GetRuleViolations()方法产生的验证消息集合
        /// 实际项目中，可以自己去规定，本方法为虚方法，派生类可以重写
        /// </summary>
        /// <returns></returns>
        public virtual string GetRuleViolationMessages() {

            if (GetRuleViolations() != null && GetRuleViolations().Count() > 0) {
                return string.Join(",", GetRuleViolations().Select(i => i.ToString()));
            }
            return string.Empty;

        }
        #endregion
    }
}
