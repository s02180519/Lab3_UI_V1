using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Grid               /*параметры равномерной сетки по времени*/
    {
        public float t { set; get; }
        public float time_step { set; get; }
        public int number_of_grid_points { set; get; }

        public Grid(float new_t, float new_time_step, int new_number_of_grid_points)
        {
            t = new_t;
            time_step = new_time_step;
            number_of_grid_points = new_number_of_grid_points;
        }
        public override string ToString()
        {
            return "time is:" + t + "\ntime step is:" + time_step + "\nnumber of grid point:" + number_of_grid_points;
        }
        public string ToString(string format)
        {
            return "time is:" + String.Format(format, t) + "\ntime step is: " + String.Format(format, time_step) +
                "\nnumber of grid point:" + String.Format(format, number_of_grid_points) + "\n";
        }
    }
}