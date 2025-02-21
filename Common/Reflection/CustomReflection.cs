using System;
using System.Reflection;

namespace Common.Reflection
{
    public static class CustomReflection
    {
        public static Type GetFieldType(object targetObject, string fieldName)
        {
            PropertyInfo property = targetObject.GetType().GetProperty(fieldName);
            if (property != null)
            {
                return property.PropertyType;
            }

            FieldInfo field = targetObject.GetType().GetField(fieldName);
            if (field != null)
            {
                return field.FieldType;
            }

            return null;
        }
        
        public static Type GetFieldType(Type targetType, string fieldName)
        {
            PropertyInfo property = targetType.GetProperty(fieldName);
            if (property != null)
            {
                return property.PropertyType;
            }

            FieldInfo field = targetType.GetField(fieldName);
            if (field != null)
            {
                return field.FieldType;
            }

            return null;
        }
    }
}