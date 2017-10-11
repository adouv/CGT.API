using System;

namespace CGT.DDD.Events
{
    /// <summary>
    /// 异步事件处理
    /// </summary>
    /// <remarks></remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute : Attribute
    {

    }
}
