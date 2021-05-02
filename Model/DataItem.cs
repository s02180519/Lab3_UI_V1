using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    [Serializable]
    public class DataItem : ISerializable   /* значения поля в момент времени t*/
    {
        public float t { get; set; }
        public System.Numerics.Vector3 coordinates { get; set; }

        public DataItem(float new_t, System.Numerics.Vector3 new_coordinates)
        {
            t = new_t;
            coordinates = new_coordinates;
        }

        public float T
        {
            get { return t; }
            set { t = value; }
        }

        public override string ToString()
        {
            string str = "time is:" + t + "\ncoordinates are: ";
            //  str += ToString();
            str += "<" + coordinates.X + ";" + coordinates.Y + ";" + coordinates.Z + ">";
            return str;
        }
        public string ToString(string format)
        {
            string str = "";
            str += "current time:" + String.Format(format, t) + " current coordinates:<" + String.Format(format, coordinates.X) + ";" +
                String.Format(format, coordinates.Y) + ";" + String.Format(format, coordinates.Z) + "> vector`s length:" + String.Format(format, coordinates.Length()) + "\n";
            return str;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("t", t);
            info.AddValue("coordinates_x", coordinates.X);
            info.AddValue("coordinates_y", coordinates.Y);
            info.AddValue("coordinates_z", coordinates.Z);
        }

        public DataItem (SerializationInfo info, StreamingContext streamingContext)
        {
            t = info.GetSingle("t");
            coordinates = new System.Numerics.Vector3(info.GetSingle("coordinates_x"), info.GetSingle("coordinates_y"), info.GetSingle("coordinates_z"));

        }

        public static explicit operator DataItem(V1DataCollection v)
        {
            throw new NotImplementedException();
        }
    }
}
