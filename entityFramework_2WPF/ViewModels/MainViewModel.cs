
using CommunityToolkit.Mvvm.Input;
using entityFramework_2WPF.Data;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace entityFramework_2WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool addValueIsChecked;
        public bool AddValueIsChecked
        {
            get { return addValueIsChecked; }
            set
            {
                addValueIsChecked = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenFileCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand AddValueCommand { get; private set; }
        public ICommand ResetDataCommand { get; private set; }
        public ICommand ClosePopupCommand { get; private set; }
        public ICommand AddValueBtnCommand { get; private set; }
        public ICommand CancelBtnCommand { get; private set; }

        private ShopContext shopContext;

        public static MainViewModel? instance;
        public MainViewModel()
        {
            instance = this;

            shopContext = new ShopContext();
            CheckDatabaseConnection();

            OpenFileCommand = new RelayCommand(() => OpenFile());
            ExportCommand = new RelayCommand(() => ExportFile());
            AddValueCommand = new RelayCommand(() => { AddValueIsChecked = true; });
            ResetDataCommand = new RelayCommand(() => ResetData());
            AddValueBtnCommand = new RelayCommand(() => AddValue());
            CancelBtnCommand = new RelayCommand(() => { AddValueIsChecked = !MainWindow.instance.PopupPopup.IsOpen; });
        }

        private async void CheckDatabaseConnection()
        {
            if (!await shopContext.Database.CanConnectAsync())
            {
                await shopContext.Database.EnsureCreatedAsync();
            }
        }
        private void OpenFile()
        {
            
        }
        private void ExportFile()
        {

        }
        private void AddValue()
        {
            
        }
        private void ResetData()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
