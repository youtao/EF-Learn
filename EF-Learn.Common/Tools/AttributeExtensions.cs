using System;
using System.Linq;

namespace EF_Learn.Common.Tools
{
    public static class AttributeExtensions
    {
        /// <summary>
        /// 获取标签属性
        /// </summary>
        /// <typeparam name="TAttribute">要查询的特性标签类型</typeparam>
        /// <typeparam name="TValue">要返回该特性标签值得类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="lambda">查询表达式</param>
        /// <returns></returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> lambda) where TAttribute : Attribute
        {
            var attribute = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (attribute != null)
            {
                return lambda(attribute);
            }
            return default(TValue);
        }
    }
}