using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum ChangeInfo { ItemChanged, Add, Remove, Replace };

    public delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);

    public class DataChangedEventArgs : EventArgs
    {
        public ChangeInfo ChangeInfo { get; set; }
        public string Data { get; set; }
        public DataChangedEventArgs(string Data, ChangeInfo ChangeInfo)
        {
            this.ChangeInfo = ChangeInfo;
            this.Data = Data;
        }
        public override string ToString() => ChangeInfo + " " + Data;
    }

    public class V1MainCollection : IEnumerable<V1Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private/*public*/ List<V1Data> elements=new List<V1Data>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public event DataChangedEventHandler DataChanged;

        public V1MainCollection()
        {
            change_after_save = false;
            CollectionChanged += CollectionChangedHandler;
        }
        
        public void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            change_after_save = true;
        }
        public void  New_V1MainCollection()
        {

            elements.Clear();
            NotifyPropertyChanged("all_elements");
            change_after_save = true;
            NotifyPropertyChanged("max_count");
            //  NotifyPropertyChanged("all_elements");

        }

        public int count { get { return elements.Count; } }
        public bool change_after_save = true;

        public void Save(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, elements);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void Load(string filename)
        {
            FileStream fileStream = null;
            try
            {
                New_V1MainCollection();
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                var obj= binaryFormatter.Deserialize(fileStream) as List<V1Data>;
                foreach (V1Data val in obj)
                {
                    Add(val);
                } 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

            }
        }

        public int max_count
        {
            get
            {
                try
                {
                    var query_1 = from elem in (from item in elements where item is V1DataOnGrid select (V1DataOnGrid)item)
                                  where elem.grid.number_of_grid_points == (from item in elements where item is V1DataOnGrid select (V1DataOnGrid)item).Max(x => x.grid.number_of_grid_points)
                                  select elem.grid.number_of_grid_points;
                    var query_2 = from elem in (from item in elements where item is V1DataCollection select (V1DataCollection)item)
                                  where elem.value.Count() == (from item in elements where item is V1DataCollection select (V1DataCollection)item).Max(x => x.value.Count())
                                  select elem.value.Count();
                    if (query_1.Any() && query_2.Any())
                        return query_1.First() > query_2.First() ? query_1.First() : query_2.First();
                    else if (query_1.Any())
                        return query_1.First();
                    else if (query_2.Any())
                        return query_2.First();
                    else return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return -1;
                }

            }
        }

        public IEnumerable<V1Data> all_elements
        {
            get
            {
                try
                {
                    var query_1 = from item in elements select item;
                    return query_1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return (IEnumerable<V1Data>)new V1MainCollection();
                }
            }
        }

        public IEnumerable<V1Data> DataCollection
        {
            get
            {
                try
                {
                    var query_1 = from item in elements
                                  where item is V1DataCollection
                                  select item;
                                  ;
                    return query_1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return (IEnumerable<V1Data>)new V1MainCollection();
                }
            }
        }

        public IEnumerable<V1Data> DataOnGrid
        {
            get
            {
                try
                {
                    var query_1 = from item in elements
                                  where item is V1DataOnGrid
                                  select item;
                    ;
                    return query_1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return (IEnumerable<V1Data>)new V1MainCollection();
                }
            }
        }


        public void NotifyPropertyChanged(string propertyname = "")
        {
            if (PropertyChanged != null)
            {
                change_after_save = false;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
     //       Console.WriteLine("Notify");
        }


        public V1Data this[int index]
        {
            get
            {
                return elements[index];
            }
            set
            {

                if (DataChanged != null)
                    DataChanged(this, new DataChangedEventArgs(elements[index].data, ChangeInfo.Replace));
                elements[index] = value;
            }
        }



        public void PropertyChangedCollector(object sender, PropertyChangedEventArgs args)
        {
            // Console.WriteLine("PropertyChangedCollector");
            if (DataChanged != null)
                DataChanged(this, new DataChangedEventArgs((sender as V1Data).data, ChangeInfo.ItemChanged));
        }
        public IEnumerable<DataItem> V1Data_ordered_by_coordinates_length
        {
            get
            {
                try
                {
                    var query_1 = from elem in (from item in elements where item is V1DataOnGrid select (V1DataOnGrid)item) from elem_DataItem in elem select elem_DataItem;
                    var query_2 = from elem in (from item in elements where item is V1DataCollection select (V1DataCollection)item) from elem_DataItem in elem select elem_DataItem;
                    var query_3 = query_1.Union(query_2);
                    query_2 = from elem in query_3 orderby elem.coordinates.Length() descending select elem;
                    return query_2;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return (IEnumerable<DataItem>)(new DataItem(-1, new System.Numerics.Vector3(0, 0, 0)));
                }
            }
        }

        public IEnumerable<float> time_one_time
        {
            get
            {
                var query_1 = from elem in (from item in elements where item is V1DataOnGrid select (V1DataOnGrid)item) from elem_DataItem in elem select elem_DataItem;
                var query_2 = from elem in (from item in elements where item is V1DataCollection select (V1DataCollection)item) from elem_DataItem in elem select elem_DataItem;
                var res = query_1.Union(query_2);
                IEnumerable<float> times = (from elem in res
                                            where res.Count(param => param.t == elem.t) == 1
                                            select elem.t);
                return times;
            }
        }

        public void Add(V1Data item)
        {
            item.PropertyChanged += PropertyChangedCollector;
            elements.Add(item);
            if (DataChanged != null)
                DataChanged(this, new DataChangedEventArgs(item.data, ChangeInfo.Add));
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            NotifyPropertyChanged("max_count");
            NotifyPropertyChanged("all_elements");

        }

        public bool Remove(string id, DateTime dateTime)
        {
            bool flag = false; int i = 0;
            while (i < elements.Count)
            {
                if (String.Compare(elements[i].data, id) == 0 && DateTime.Compare(elements[i].date, dateTime) == 0)
                {
                    elements[i].PropertyChanged += PropertyChangedCollector;
                    elements.RemoveAt(i);
                    if (DataChanged != null)
                        DataChanged(this, new DataChangedEventArgs(elements[i].data, ChangeInfo.Remove));
                    if (CollectionChanged != null)
                        CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    
                    NotifyPropertyChanged("max_count");
                    NotifyPropertyChanged("all_elements");
                    if (!flag) { flag = true; }
                }
                else
                    i++;
            }


            return flag;
        }

        public void AddDefaultDataCollection()
        {
            V1DataCollection value2 = new V1DataCollection("ID6", new DateTime(5, 5, 5));
            value2.InitRandom(9, 1, 4, 3, 4);
            Add(value2);
            change_after_save = true;
        }

        public void AddDefaultDataOnGrid()
        {
            Random rnd = new Random();
            Grid new_grid = new Grid(rnd.Next(100), rnd.Next(5), 4);
            V1DataOnGrid value1 = new V1DataOnGrid("ID7", new DateTime(5, 5, 5), new_grid);
            value1.InitRandom(3, 7);
            Add(value1);
            change_after_save = true;
        }

        public void AddDefaults()
        {
            Random rnd = new Random();


            Grid new_grid;
            V1DataOnGrid value1;
            V1DataCollection value2;

            new_grid = new Grid(rnd.Next(100), rnd.Next(5), 4);
            value1 = new V1DataOnGrid("ID1", new DateTime(5, 5, 5), new_grid);
            value1.InitRandom(3, 7);
            Add(value1);
            new_grid = new Grid(rnd.Next(100), rnd.Next(5), 7);
            value1 = new V1DataOnGrid("ID2", new DateTime(5, 5, 5), new_grid);
            value1.InitRandom(3, 7);
            Add(value1);
            new_grid = new Grid(rnd.Next(100), rnd.Next(5), 0);
            value1 = new V1DataOnGrid("ID3", new DateTime(5, 5, 5), new_grid);
            value1.InitRandom(3, 7);
            Add(value1);
            value2 = new V1DataCollection("ID4", new DateTime(5, 5, 5));
            value2.InitRandom(5, 1, 4, 3, 4);
            Add(value2);
            value2 = new V1DataCollection("ID5", new DateTime(5, 5, 5));
            value2.InitRandom(0, 1, 4, 3, 4);
            Add(value2);
            
             change_after_save = true;

        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str = str + elements[i].ToString() + "\n";
            }
            return str;
        }

        public IEnumerator<V1Data> GetEnumerator()
        {
            return ((IEnumerable<V1Data>)elements).GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)elements).GetEnumerator();
        }

        public string ToLongString(string format)
        {
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str = str + elements[i].ToLongString(format);
            }
            return str;
        }

    }
    
}