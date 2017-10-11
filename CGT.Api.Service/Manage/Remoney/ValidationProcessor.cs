using CGT.Event.Model.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGT.Api.Service.Manage.Remoney {
    /// <summary>
    /// 验证基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValidationProcessor<T> {
        /// <summary>
        /// 比率结果  0成功  1审核 2失败
        /// </summary>
        public int BateResult { get; set; }
        /// <summary>
        /// 白屏比率
        /// </summary>
        public double WhiteBate { get; set; }
        /// <summary>
        /// 黑屏比率
        /// </summary>
        public double BlackBate { get; set; }
        /// <summary>
        /// 获取验证比率
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public abstract ValidationView GetValidationBate(List<T> list);
    }
    /// <summary>
    /// 白屏验证结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WhiteValidationProcessor<T> : ValidationProcessor<T> where T : BaseRiskModel {
        public override ValidationView GetValidationBate(List<T> list) {
            var task = Task.Run(() => {
                WhiteBate = list.Where(i => i.WhiteResultState == 1).Count() / list.Count;
                if (WhiteBate >= (double)list[0].WhiteSuccessRate) {
                    BateResult = 0;
                }
                else if (WhiteBate < (double)list[0].WhiteFailRate) {
                    BateResult = 2;
                }
                else {
                    BateResult = 1;
                }
            });
            task.Wait();
            return new ValidationView() {
                BateResult = BateResult,
                WhiteBate = WhiteBate,
                BlackBate = 0
            };
        }
    }
    /// <summary>
    /// 黑屏验证
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlackValidationProcessor<T> : ValidationProcessor<T> where T : BaseRiskModel {
        public override ValidationView GetValidationBate(List<T> list) {
            var task = Task.Run(() => {
                BlackBate = list.Where(i => i.BlackResultState == 1).Count() / list.Count;
                if (BlackBate >= (double)list[0].EtermSuccessRate) {
                    BateResult = 0;
                }
                else if (BlackBate < (double)list[0].EtermFailRate) {
                    BateResult = 2;
                }
                else {
                    BateResult = 1;
                }
            });
            task.Wait();
            return new ValidationView() {
                BateResult = BateResult,
                WhiteBate = WhiteBate,
                BlackBate = BlackBate
            };
        }
    }
    /// <summary>
    /// 黑白屏混合验证
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MergeValidationProcessor<T> : ValidationProcessor<T> where T : BaseRiskModel {
        public override ValidationView GetValidationBate(List<T> list) {
            var task = Task.WhenAll(
                           Task.Run(() => WhiteBate = list.Where(i => i.WhiteResultState == 1).Count() / list.Count),
                           Task.Run(() => BlackBate = list.Where(i => i.BlackResultState == 1).Count() / list.Count));
            task.ContinueWith(w => {
                if (WhiteBate >= (double)list[0].WhiteSuccessRate && BlackBate >= (double)list[0].EtermSuccessRate) {
                    BateResult = 0;
                }
                else if (WhiteBate < (double)list[0].WhiteFailRate || BlackBate < (double)list[0].EtermFailRate) {
                    BateResult = 2;
                }
                else {
                    BateResult = 1;
                }
            });
            task.Wait();
            return new ValidationView() {
                BateResult = BateResult,
                WhiteBate = WhiteBate,
                BlackBate = 0
            };
        }
    }
    /// <summary>
    /// 验证抽象工厂类
    /// </summary>
    public static class ValidationProcessorFactory<T> where T : BaseRiskModel {
        public static ValidationProcessor<T> CreateValidationProcessor(int Type) {
            switch (Type) {
                case 0:
                    return new BlackValidationProcessor<T>();
                case 1:
                    return new WhiteValidationProcessor<T>();
                case 2:
                    return new MergeValidationProcessor<T>();
                default:
                    throw new NotSupportedException("未知风控类型");
            }
        }
    }
    /// <summary>
    /// 返回基类
    /// </summary>
    public class ValidationView {
        /// <summary>
        /// 比率结果  0成功  1审核 2失败
        /// </summary>
        public int BateResult { get; set; }
        /// <summary>
        /// 白屏比率
        /// </summary>
        public double WhiteBate { get; set; }
        /// <summary>
        /// 黑屏比率
        /// </summary>
        public double BlackBate { get; set; }
    }
}
