using Lab3ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ViewModelTests
{
    public class FakeUIServices : IUIServices
    {

        public string ConfirmOpen()
        {
            return "test.txt";
        }

        public string ConfirmLoadFromFile()
        {
            return "C:/Users/monul/OneDrive/Desktop/Lab3_UI_V1/LoadFromFile.txt";
        }

        public string ConfirmSave(bool useMessageBox)
        {
            return "test.txt";
        }

        void IUIServices.ConfirmError(Exception ex)
        {
            throw new NotImplementedException();
        }
    }


    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void TestAddCustomCommand()
        {
            MainViewModel mainViewModel = new MainViewModel(null);
            mainViewModel.DataString = "ID8";
            mainViewModel.NumberOfGridPoints = 2;
            mainViewModel.MinValue = 4.0;
            mainViewModel.MaxValue = 5.0;
            Assert.IsTrue(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.AddCustomCommand.Execute(null);
            Assert.AreEqual(2, mainViewModel.MaxCount);
            mainViewModel.DataString = "ID9";
            mainViewModel.MinValue = 5.0;
            Assert.IsFalse(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.MinValue = 4.0;
            Assert.IsTrue(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.MaxValue = 4.0;
            Assert.IsFalse(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.MaxValue = 5.0;
            Assert.IsTrue(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.NumberOfGridPoints = 1;
            Assert.IsFalse(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.NumberOfGridPoints = 2;
            Assert.IsTrue(mainViewModel.AddCustomCommand.CanExecute(null));
            mainViewModel.DataString = "ID8";
            Assert.IsFalse(mainViewModel.AddCustomCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestAddDefaultsCommand()
        {
            MainViewModel mainViewModel = new MainViewModel(null);
            mainViewModel.AddDefaultsCommand.Execute(null);
            Assert.AreEqual(8, mainViewModel.MaxCount);
        }

        [TestMethod]
        public void TestAddDefaultDataCollectionCommandCommand()
        {
            MainViewModel mainViewModel = new MainViewModel(null);
            mainViewModel.AddDefaultDataCollectionCommand.Execute(null);
            Assert.AreEqual(12, mainViewModel.MaxCount);
        }


        [TestMethod]
        public void TestAddElementFromFileCommand()
        {
            FakeUIServices fakeUIServices = new FakeUIServices();
            MainViewModel mainViewModel = new MainViewModel(fakeUIServices);
            Assert.IsTrue(mainViewModel.AddElementFromFileCommand.CanExecute(null));
            mainViewModel.AddElementFromFileCommand.Execute(null);
            Assert.AreEqual(1, mainViewModel.MaxCount);
        }


        [TestMethod]
        public void TestAddDefaultDataOnGridCommand()
        {
            MainViewModel mainViewModel = new MainViewModel(null);
            mainViewModel.AddDefaultDataOnGridCommand.Execute(null);
            Assert.AreEqual(4, mainViewModel.MaxCount);
        }


        [TestMethod]
        public void TestNewCommand()
        {
            FakeUIServices fakeUIServices = new FakeUIServices();
            MainViewModel mainViewModel = new MainViewModel(fakeUIServices);
            mainViewModel.AddDefaultsCommand.Execute(null);
            Assert.AreEqual(8, mainViewModel.MaxCount);
            mainViewModel.NewCommand.Execute(null);
            Assert.AreEqual(0, mainViewModel.MaxCount);
        }


        [TestMethod]
        public void TestSaveCommand()
        {
            FakeUIServices fakeUIServices = new FakeUIServices();
            MainViewModel mainViewModel = new MainViewModel(fakeUIServices);
            Assert.IsFalse(mainViewModel.SaveCommand.CanExecute(null));
            mainViewModel.AddDefaultsCommand.Execute(null);
            Assert.IsTrue(mainViewModel.SaveCommand.CanExecute(null));
        }


        [TestMethod]
        public void TestOpenCommand()
        {
            FakeUIServices fakeUIServices = new FakeUIServices();
            MainViewModel mainViewModel = new MainViewModel(fakeUIServices);
            mainViewModel.AddDefaultsCommand.Execute(null);
            Assert.IsTrue( mainViewModel.OpenCommand.CanExecute(null));
            int maxCount = mainViewModel.MaxCount;
            mainViewModel.SaveCommand.Execute(null);
            mainViewModel.NewCommand.Execute(null);
            mainViewModel.OpenCommand.Execute(null);
            Assert.AreEqual(maxCount, mainViewModel.MaxCount);
        }


        [TestMethod]
        public void TestRemoveCommand()
        {
            MainViewModel mainViewModel = new MainViewModel(null);
            Assert.IsFalse( mainViewModel.RemoveCommand.CanExecute(null));
        }
    }
}
