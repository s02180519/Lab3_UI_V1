//using Castle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public abstract class V1Data : INotifyPropertyChanged
    {
        public string data;/*{ set; get; }*/
        public DateTime date;/* { set; get; }*/
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public V1Data()
        {

        }
        public V1Data(string new_data, DateTime new_date)
        {
            data = new_data;
            date = new_date;
        }

        protected void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
        }
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged("Data");
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public abstract float[] NearZero(float eps);
        public abstract string ToLongString();
        public override string ToString()
        {
            return "data is: " + data + "\ndate is: " + date;
        }
        public abstract string ToLongString(string format);
    }
}