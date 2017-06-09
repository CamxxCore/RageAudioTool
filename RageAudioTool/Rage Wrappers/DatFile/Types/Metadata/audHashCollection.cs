using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    [TypeConverter(typeof(NamedObjectConverter))]
    public class audHashCollection : CollectionBase, ICustomTypeDescriptor
    {
        private readonly audSoundBase parent;

        public List<audHashDesc> BaseList => List.Cast<audHashDesc>().ToList();
    
        public audHashCollection(audSoundBase parent)
        {
            this.parent = parent;
        }

        public audHashDesc this[int index] => (audHashDesc)List[index];

        public void Add(uint hashKey, int subOffset)
        {
            List.Add(new audHashDesc(new audHashString(parent.Parent, hashKey), subOffset));
        }

        public void Add(audHashDesc track)
        {
            List.Add(track);
        }

        public void Remove(audHashDesc track)
        {
            List.Remove(track);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            var pds = new PropertyDescriptorCollection(null);

            for (int i = 0; i < this.List.Count; i++)
            {
                audHashCollectionPropertyDescriptor pd = new
                    audHashCollectionPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            return pds;
        }
    }
}
