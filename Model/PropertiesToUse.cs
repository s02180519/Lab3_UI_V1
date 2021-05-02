using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public partial class PropertiesToUse : IDataErrorInfo, System.ComponentModel.INotifyPropertyChanged
    {
        public double min=4.0, max=5.0;
        public V1MainCollection element_collection;
        public string string_data = "";
        public string data
        {
            get { return string_data; }
            set
            { string_data = value;
            }
        }
        public int number_of_grid_points { get; set; }
        public double minValue { get { return min; } set {
                min = value;
                NotifyPropertyChanged("maxValue"); } }
        public double maxValue { get { return max; } set
            {
                max = value;
                NotifyPropertyChanged("minValue");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public PropertiesToUse(ref V1MainCollection collection, string new_data = "UI8", int new_number_of_grid_points = 2, double new_minValue = 4.0, double new_maxValue = 5.0)
        {
            element_collection = collection;
            data = new_data;
            number_of_grid_points = new_number_of_grid_points;
            minValue = new_minValue;
            maxValue=new_maxValue;
        }

        public string Error { get { return "Error Invalid Data"; } }

        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "data":
                        {
                            if (data.Length == 0)
                            {
                                msg = "Data is empty";
                                break;
                            }
                            foreach (V1Data element in element_collection)
                                if (string.Compare(element.data, data) == 0)
                                    msg = "the value of the string property of the base class V1 Data must not match the value of this property for any element in the V1MainCollection collection";
                            break;
                        }
                    case "number_of_grid_points":
                        if (number_of_grid_points < 2) msg = "the number of grid nodes in time must be greater than 2";
                        break;
                    case "minValue":
                        {
                            if (minValue >= maxValue) msg = "the MinValue value must be less than the MaxValue";
                            break;
                        }
                    case "maxValue":
                        {
                            if (minValue >= maxValue) 
                            { 
                                msg = "the MinValue value must be less than the MaxValue";
                            }
                            break;
                        }
                    default:
                        break;
                }
                return msg;
            }
        }

        public void NotifyPropertyChanged(string propertyname = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        public static void Add(PropertiesToUse element)
        {
            Grid new_grid = new Model.Grid(0, 1, element.number_of_grid_points);
            V1DataOnGrid value1 = new V1DataOnGrid(element.data, DateTime.Now , new_grid);
            value1.InitRandom((float)element.min, (float)element.max);
            element.element_collection.Add(value1);
        }
        
    }
}
