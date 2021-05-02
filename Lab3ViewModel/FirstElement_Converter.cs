using Model;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Windows.Data;

namespace Lab3ViewModel
{
    [ValueConversion(typeof(V1DataOnGrid), typeof(string))]
    public class FirstElement_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //   System.Windows.Controls.ListBox res =(System.Windows.Controls.ListBox) value;
            //   if (res.SelectedItems.Count >0)
            //      return res.SelectedItem.ToString();
            Vector3[] points_value = (Vector3[])value;
            if (points_value.Length > 0)
                return points_value[points_value.Length-1];
            else return null;
            //  DataItem res = (DataItem)value;
            //  return res.coordinates;
          //  else return null;
        }

        public object ConvertBack(object value, Type targetType,
                                                       object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
