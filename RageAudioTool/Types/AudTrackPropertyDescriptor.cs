using System;
using System.ComponentModel;
using System.Text;
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool.Types
{
    public class audHashCollectionPropertyDescriptor : PropertyDescriptor
    {
        private readonly audHashCollection collection;
        private int index = -1;

        public audHashCollectionPropertyDescriptor(audHashCollection coll,
            int idx) : base( "#"+idx, null )
        {
            collection = coll;
            index = idx;
        }

        public override AttributeCollection Attributes => new AttributeCollection(null);

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType => collection.GetType();

        public override string DisplayName
        {
            get
            {
                audHashDesc t = collection[index];
                return t.TrackName;
            }
        }

        public override string Description
        {
            get
            {
                audHashDesc t = collection[index];
                StringBuilder sb = new StringBuilder();
                sb.Append(t.TrackName.ToString());
                return sb.ToString();
            }
        }

        public override object GetValue(object component)
        {
            return collection[index];
        }

        public override bool IsReadOnly => true;

        public override string Name => "#" + index.ToString();

        public override Type PropertyType => collection[index].GetType();

        public override void ResetValue(object component) { }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
          //   collection[index].TrackName = (audHashString) value;
        }
    }
}
