using System;

namespace CDWM_MR.Common
{
    /// <summary>
    /// 数据库事务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class UseTranAttribute:Attribute
    {
    }
}
