using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CGT.DDD
{
   public class ServerCommon
    {
        private static int seed;
        public static string CreateOrderId()
        {
            return "cgt"+Get62String(Interlocked.Increment(ref seed)) + Get62String(DateTime.Now.Ticks);
        }
        private static string Get62String(long val)
        {
            var buffer = new StringBuilder();
            while (val != 0)
            {
                var rem = val % 62;
                var chr = (rem < 10 ? 48 : (rem < 36 ? 55 : 61)) + rem;
                buffer.Append((char)chr);
                val /= 62;
            }
            return buffer.ToString();
        }
    }
}
