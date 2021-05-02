using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace Lab3ViewModel
{
    [ValueConversion(typeof(V1DataCollection), typeof(string))]
    public class DataCollection_coordinates_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
                DataItem res = (DataItem)value;
            // return res.ToLongString();
            return "Coordinates: "+res.coordinates.ToString()+" module: "+res.coordinates.Length();
              //  DataItem res = (DataItem)value;
              //  return res.coordinates;
        }

        public object ConvertBack(object value, Type targetType,
                                                       object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
