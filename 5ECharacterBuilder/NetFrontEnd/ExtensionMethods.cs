using System;
using System.ComponentModel;

namespace NetFrontEnd
{
    public static class ExtensionMethods
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) 
                return null;

            var field = type.GetField(name);

            if (field == null) 
                return null;

            var attr = (DescriptionAttribute) Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : name;
        }
    }
}