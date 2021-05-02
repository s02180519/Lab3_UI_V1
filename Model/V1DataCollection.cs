using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class V1DataCollection : V1Data, IEnumerable<DataItem>            /*значения поля на неравномерной сетке, которые хранятся в коллекции List<DataItem>*/
    {
        public List<DataItem> value { get; set; }

        public V1DataCollection(string filename)
        {
            value = new List<DataItem>();
            FileStream fs = null;                               //new_data;new_date;time1;coordinate1.X;coordinate1.Y;time2;coordinate1.Z;coordinate2.X;coordinate2.Y;coordinate2.Z;...e.t.c
            try
            {
                DataItem tmp;
                string file_string;
                fs = new FileStream(filename, FileMode.Open);
                // BinaryReader reader = new BinaryReader(fs);
                StreamReader streamReader = new StreamReader(fs);
                // file_string =reader.ReadString();
                file_string = streamReader.ReadLine();
                string[] file_data = file_string.Split(new char[] { ';' });
                base.data = file_data[0];
                base.date = Convert.ToDateTime(file_data[1]);
                for (int i = 2; i < file_data.Length; i += 4)
                {
                    //  Console.WriteLine(float.Parse(file_data[i]));
                    tmp = new DataItem(Convert.ToSingle(file_data[i]), new Vector3(Convert.ToSingle(file_data[i + 1]), Convert.ToSingle(file_data[i + 2]), Convert.ToSingle(file_data[i + 3])));
                    value.Add(tmp);
                }
                streamReader.Close(); // вызывает binaryReader.Dispose(true);
                                      // освобождает все управляемые и неуправляемые ресурсы

            }
            catch (Exception ex)
            {
                //  value.RemoveAt(0);
                throw new Exception("файл содержит некорректные данные");
              //  Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close(); // закрывает поток и освобождает все ресурсы
            }
        }

      
        public V1DataCollection(string new_data, DateTime new_date) : base(new_data, new_date)
        {
            Random rnd = new Random();
            DataItem tmp;
            value = new List<DataItem>();
            for (int i = 0; i < 3; i++)
            {
                tmp = new DataItem(rnd.Next(100), new Vector3(rnd.Next(Convert.ToInt32(0), Convert.ToInt32(30)), rnd.Next(Convert.ToInt32(0), Convert.ToInt32(30)), rnd.Next(Convert.ToInt32(0), Convert.ToInt32(30))));
                value.Add(tmp);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
            info.AddValue("data", data);
            info.AddValue("date", date);
            
        }
        public V1DataCollection(SerializationInfo info, StreamingContext streamingContext) :
          base(info.GetValue("data", typeof(string)) as string, info.GetDateTime("date"))
        {

        }
       
        public override float[] NearZero(float eps)
        {
            List<float> time = new List<float>();
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].coordinates.Length() < eps)
                {
                    time.Add(value[i].t);
                }
            }
            return time.ToArray();
        }

        public void InitRandom(int nItems, float tmin, float tmax, float minValue, float maxValue)
        {
            Random rnd = new Random();
            for (int i = 0; i < nItems; i++)
            {
                value.Add(new DataItem(rnd.Next(Convert.ToInt32(tmin), Convert.ToInt32(tmax)), new Vector3(rnd.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue)), rnd.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue)), rnd.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue)))));
            }
        }

        public override string ToString()
        {
            return "type is: V1DataCollection\n" + base.ToString() + "\nCount is:" + value.Count ;
        }

        public override string ToLongString()
        {
            string str = ToString();
            for (int i = 0; i < value.Count; i++)
            {
                str = str + "\n" + value[i].ToString();
            }
            return str;
        }
        public IEnumerator<DataItem> GetEnumerator()
        {
            //Console.WriteLine("IEnumerator<Book> GetEnumerator()");
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Console.WriteLine("IEnumerable.GetEnumerator()");
            return value.GetEnumerator();
        }

        /* IEnumerator IEnumerable.GetEnumerator()
         {
             return ((IEnumerable<DataItem>)value).GetEnumerator();
         }*/

        /*public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < value.Count() ; i++)
            {
                //points.Add(new DataItem(grid.t + i * grid.time_step, points_value[i]));
                yield return value[i];
            }
        }*/
        public override string ToLongString(string format)
        {
            string str = "";
            str += "type is: V1DataCollection " + "\ndata is:" + base.data + "\ndate is: " + base.date + "\n";
            for (int i = 0; i < value.Count; i++)
            {
                // str+=String.Format("format",value[i].t,value[i].coordinates.X,value[i].coordinates.Y,)
                str += "time is:" + String.Format(format, value[i].t) + " coordinates: <" + String.Format(format, value[i].coordinates.X) + ";" +
                    String.Format(format, value[i].coordinates.Y) + ";" + String.Format(format, value[i].coordinates.Z) + String.Format(format, value[i].coordinates.Length()) + ">\n";

            }
            return str;
        }
    }
}