using System;
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
                return value?.GetType().Name;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
