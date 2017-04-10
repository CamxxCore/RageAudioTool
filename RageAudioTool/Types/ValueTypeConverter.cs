using System;
using System.Reflection;

namespace RageAudioTool.Types
{
    public class ValueTypeConverter : System.ComponentModel.ExpandableObjectConverter
    {
        public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            object boxed = Activator.CreateInstance(context.PropertyDescriptor.PropertyType);
            foreach (System.Collections.DictionaryEntry entry in propertyValues)
            {
                PropertyInfo pi = context.PropertyDescriptor.PropertyType.GetProperty(entry.Key.ToString());
                if ((pi != null) && (pi.CanWrite))
                {
                    pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
                }
            }
            return boxed;
        }
    }
}
