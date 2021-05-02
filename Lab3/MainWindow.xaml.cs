using Lab3ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel mainViewModel;


        public MainWindow()
        {
            InitializeComponent();
            MyAppUIServices myAppUIServices = new MyAppUIServices();
            mainViewModel = new MainViewModel(myAppUIServices);

            DataContext = mainViewModel;
            mainViewModel.UpdateCollectionReference += UpdateReferenceHandler;
        }
        public void UpdateReferenceHandler()
        {
            ListBox_Main.ItemsSource = mainViewModel.Collection;
            ListBox_DataCollection.ItemsSource = mainViewModel.DataCollection;
            ListBox_DataOnGrid.ItemsSource = mainViewModel.DataOnGrid;
            Label_MaxCount.Content = mainViewModel.MaxCount;
        }


        public class MyAppUIServices : IUIServices
        {
            public MyAppUIServices()
            {
            }

            public string ConfirmOpen()
            {
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                if (ofd.ShowDialog() == true)
                    return ofd.FileName;
                return null;
            }

            public string ConfirmLoadFromFile()
            {
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                if (ofd.ShowDialog() == true)
                    return ofd.FileName;
                return null;
            }

            public string ConfirmSave(bool useMessageBox)
            {
                if (useMessageBox)
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Вы изменили данные. Без сохранения они будут утеряны\nСохранить?", "Сохранение", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return null;
                    }
                }

                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                if (sfd.ShowDialog() == true)
                    return sfd.FileName;

                return null;
            }

            public void ConfirmError(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

    }
}
