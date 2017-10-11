using System;

namespace CGT.SuspendedService {
    /// <summary>
    /// 数字压缩类
    /// </summary>
    class NumberPackger {
        /// <summary>
        /// 位数
        /// </summary>
        public const int Bits = 6;

        private static readonly char[] Mapping = {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="value">需要进行压缩的值</param>
        public static string Package(int value) {
            var result = string.Empty;
            var lenth = Mapping.Length;
            var num = value;
            while (num > 0) {
                result = Mapping[num % lenth] + result;
                num = num / lenth;
            }
            return result.PadLeft(Bits, '0');
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="value">需要解压的内容</param>
        public static int Unpackage(string value) {
            var result = 0;
            var length = Mapping.Length;
            for (var i = value.Length - 1; i >= 0; i--) {
                var times = (int)Math.Pow(length, value.Length - i - 1);

                var exists = false;
                for (int j = 0; j < length; j++) {
                    if (value[i] == Mapping[j]) {
                        result += j * times;
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    throw new InvalidOperationException("值[" + value + "]无效");
                }
            }
            return result;
        }
    }
}
