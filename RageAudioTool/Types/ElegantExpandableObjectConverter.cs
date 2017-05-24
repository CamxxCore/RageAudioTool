using System;
using System.Text;
using System.ComponentModel;

namespace RageAudioTool.Types
{
    class NamedObjectConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                            System.Globalization.CultureInfo culture,
                            object value, Type destType)
        {
            if (destType == typeof(string))
            {
                return value.GetType().Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    class PropertyListObjectConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
                            System.Globalization.CultureInfo culture,
                            object value, Type destType)
        {
            if (destType == typeof(string))
            {
                StringBuilder sb = new StringBuilder();

                var type = value.GetType();

                var properties = type.GetProperties();

                for (int i = 0; i < Math.Min(4, properties.Length); i++)
                {
                    sb.AppendFormat(type == typeof(string) ? 
                        "{0}: \"{1}\"" : "{0}: {1} ", 
                        properties[i].Name, properties[i].GetValue(value));
                }

                return sb.ToString();
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
