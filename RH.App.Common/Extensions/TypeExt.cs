using System;

namespace RH.App.Common.Extensions
{
    public static class TypeExt
    {
        public static bool IsImplementedFrom(this Type type, Type c)
        {
            if (type == null) return false;
            if (c == null) return false;

            return c.IsAssignableFrom(type) && c != type;
        }

        public static bool IsImplementedGenericFrom(this Type type, Type c)
        {
            if (type == null) return false;
            if (c == null) return false;

            if (!type.IsGenericType && !c.IsGenericType && type.IsImplementedFrom(c)) return true;

            var baseType = type.BaseType;

            while (baseType != null)
            {
                if (baseType.IsImplementedFrom(c)) return true;
                if (baseType.IsGenericType && c.IsGenericType && baseType.GetGenericTypeDefinition() == c.GetGenericTypeDefinition()) return true;

                baseType = baseType.BaseType;
            }

            foreach (var inter in type.GetInterfaces())
            {
                if (inter.IsGenericType && inter.GetGenericTypeDefinition() == c.GetGenericTypeDefinition()) return true;
            }

            return false;
        }
    }
}
