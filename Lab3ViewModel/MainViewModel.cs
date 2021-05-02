using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab3ViewModel
{

    public interface IUIServices
    {
        string ConfirmSave(bool useMessageBox);

        string ConfirmOpen();

        void ConfirmError(Exception ex);

        string ConfirmLoadFromFile();
    }

    public delegate void SimpleFunc();

    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        private IUIServices uIServices;
        
        private string dataString = "UI8";

        public string DataString
        {
            get { return dataString; }
            set
            {
                dataString = value;
                RaisePropertyChanged();
            }
        }

        private int numberOfGridPoints = 2;

        public int NumberOfGridPoints
        { 
            get => numberOfGridPoints; 
            set
            {
                numberOfGridPoints = value;
                RaisePropertyChanged();
            }
        }

        private double minValue = 4.0;
        public double MinValue
        {
            get => minValue; 
            set
            {
                minValue = value;
                RaisePropertyChanged("maxValue");
            }
        }

        private double maxValue = 5.0;

        public double MaxValue
        {
            get => maxValue; 
            set
            {
                maxValue = value;
                RaisePropertyChanged("minValue");
            }
        }

        public event SimpleFunc UpdateCollectionReference;

        public V1MainCollection collection = new V1MainCollection();

        public IEnumerable Collection{ get => collection;}
        public IEnumerable DataCollection { get => collection.DataCollection; }

        public IEnumerable DataOnGrid { get => collection.DataOnGrid; }

        public int MaxCount 
        { 
            get=>collection.max_count;
        }

        private readonly ICommand openCommand;

        private readonly ICommand saveCommand;

        private readonly ICommand addDefaultsCommand;

        private readonly ICommand addDefaultDataCollectionCommand;

        private readonly ICommand addDefaultDataOnGridCommand;


        private readonly ICommand newCommand;

        private readonly ICommand removeCommand;

        private readonly ICommand addElementFromFileCommand;

        private readonly ICommand addCustomCommand;


        public ICommand OpenCommand { get => openCommand; }

        public ICommand SaveCommand { get => saveCommand; }

        public ICommand AddDefaultsCommand { get => addDefaultsCommand; }

        public ICommand AddElementFromFileCommand { get => addElementFromFileCommand; }

        public ICommand AddDefaultDataCollectionCommand { get => addDefaultDataCollectionCommand; }

        public ICommand AddDefaultDataOnGridCommand { get => addDefaultDataOnGridCommand; }

        public ICommand NewCommand { get => newCommand; }

        public ICommand RemoveCommand { get => removeCommand; }

        public ICommand AddCustomCommand { get => addCustomCommand; }

        public string Error { get { return "Error ViewModel.Validation"; } }

        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "data":
                        {
                            if (dataString.Length == 0)
                            {
                                msg = "Data is empty";
                                break;
                            }
                            foreach (V1Data element in collection)
                                if (string.Compare(element.data, dataString) == 0)
                                    msg = "common data!";
                            break;
                        }
                    case "number_of_grid_points":
                        if (numberOfGridPoints < 2) msg = "the number of grid nodes in time must be greater than 2";
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

        public MainViewModel(IUIServices uIServices)
        {
            this.uIServices = uIServices;

            addCustomCommand = new RelayCommand(_ => this["data"]!= "Data is empty" && this["data"] != "common data!" && 
            this["number_of_grid_points"] != "the number of grid nodes in time must be greater than 2" && 
            this["minValue"] != "the MinValue value must be less than the MaxValue" &&
            this["maxValue"] != "the MinValue value must be less than the MaxValue",
            _ =>
            {
                PropertiesToUse properties = new PropertiesToUse(ref collection, dataString, numberOfGridPoints, minValue, maxValue);
                PropertiesToUse.Add(properties);
                DataString = "";
                collection.CollectionChanged += collection.CollectionChangedHandler;
                UpdateCollectionReference?.Invoke();
            });

            addDefaultsCommand = new RelayCommand(_ => true, _ =>
            {
                collection.AddDefaults();
                collection.CollectionChanged += collection.CollectionChangedHandler;
                UpdateCollectionReference?.Invoke();

            });

            addDefaultDataCollectionCommand = new RelayCommand(_ => true, _ =>
            {
                collection.AddDefaultDataCollection();
                collection.CollectionChanged += collection.CollectionChangedHandler;
                UpdateCollectionReference?.Invoke();

            });

            addElementFromFileCommand = new RelayCommand(_ => true, _ =>
            {
                try
                {
                    string filename = uIServices.ConfirmLoadFromFile();
                    if (filename != null)
                    {
                        collection.Add(new V1DataCollection(filename));
                        collection.CollectionChanged += collection.CollectionChangedHandler;
                        UpdateCollectionReference?.Invoke();
                    }
                }
                catch (Exception ex)
                {
                    uIServices.ConfirmError(ex);
                }

            });

            addDefaultDataOnGridCommand = new RelayCommand(_ => true, _ =>
            {
                collection.AddDefaultDataOnGrid();
                collection.CollectionChanged += collection.CollectionChangedHandler;
                UpdateCollectionReference?.Invoke();

            });

            newCommand = new RelayCommand(_ => true, _ => {
                if (collection.change_after_save)
                {
                    string filename = uIServices.ConfirmSave(true);
                    if (filename != null)
                        collection.Save(filename);
                }
                collection =new V1MainCollection();
                collection.CollectionChanged += collection.CollectionChangedHandler;
                UpdateCollectionReference?.Invoke();
            });

            saveCommand = new RelayCommand(_ => collection.change_after_save, _ => {
                try
                {
                    string filename = uIServices.ConfirmSave(false);
                    if (filename != null)
                    {
                        collection.Save(filename);
                        collection.change_after_save = false;
                    }
                }
                catch(Exception ex)
                {
                    uIServices.ConfirmError(ex);
                }
            });

            openCommand = new RelayCommand(_ => true, _ =>
            {
                try
                {
                    string filename;
                    if (collection.change_after_save) 
                    {
                        filename = uIServices.ConfirmSave(true);
                        if(filename!=null)
                            collection.Save(filename); 
                    }
                    
                    filename = uIServices.ConfirmOpen();
                    if (filename!=null)
                    {
                        collection = new V1MainCollection();
                        collection.Load(filename);
                        collection.CollectionChanged += collection.CollectionChangedHandler;
                        UpdateCollectionReference?.Invoke();
                    }
                }
                catch(Exception ex)
                {
                    uIServices.ConfirmError(ex);
                }
            });

            removeCommand = new RelayCommand(param => (param != null) && ((param as IList<object>).Count() > 0),
                param =>
                {
                    collection.Remove(((param as IList<object>)[0] as V1Data).data, ((param as IList<object>)[0] as V1Data).date);
                    collection.CollectionChanged += collection.CollectionChangedHandler;
                    UpdateCollectionReference?.Invoke();
                });
        }
    }
}
