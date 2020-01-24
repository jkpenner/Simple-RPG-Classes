using System;
using System.Reflection;

namespace RPG.Utility
{
    public static class EnumUtility
    {
        public static T GetAttr<T>(this Enum value) where T : System.Attribute
        {
            var attrs = value.GetAttrs<T>();
            return (attrs.Length > 0) ? attrs[0] : null;
        }

        public static bool HasAttr<T>(this Enum value) where T : System.Attribute
        {
            return value.GetAttrs<T>().Length > 0;
        }

        public static T[] GetAttrs<T>(this Enum value) where T : System.Attribute
        {
            return (T[])value.GetMember()[0].GetCustomAttributes(typeof(T), false);
        }

        public static MemberInfo[] GetMember(this Enum value)
        {
            return value.GetType().GetMember(value.ToString());
        }
    }
}