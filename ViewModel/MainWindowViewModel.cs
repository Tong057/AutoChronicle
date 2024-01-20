using AutoChronicle.Model.Utils;
using AutoChronicle.Resources.Languages;
using AutoChronicle.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AutoChronicle.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    { 
        public ObservableCollection<string> Items { get; }
        private readonly ICollectionView _itemsView;
        private string? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;
        private double _verticalOffset;
        private object _currentPage;

        private int _toggleButtonState;
        private string _selectedLanguage;

        public ICommand OpenInfoCommand { get; set; }
        public ICommand OpenGalleryPageCommand { get; set; }
        public ICommand OpenHomeCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }
        public ICommand ChangeLanguageCommand { get; set; }


        public MainWindowViewModel()
        {
            OpenInfoCommand = new RelayCommand((object obj) => CurrentPage = new InfoPage());
            ChangeThemeCommand = new RelayCommand((object obj) => ToggleButtonState = 1 - ToggleButtonState);
            ChangeLanguageCommand = new RelayCommand((object obj) => SelectedLanguage = (string)obj);

            ToggleButtonState = 1;
            _selectedLanguage = "ENG";

            Items = new ObservableCollection<string>(DataReader.ReadCarBrands()); // { "Audi", "Mazda", "BMW", "Mercedes", "Jeep", "Geely", "Honda", "Nissan", "Toyota", "KIA", "Cintroen", "Peugeot", "GAZ", "VAZ", "Subaru", "Mitsubishi", "FSO", "Volvo", "Dodge", "Chevrolet", "Ford"};
            Items = new ObservableCollection<string>(Items.OrderBy(i => i));

            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = ItemsFilter;
        }

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (SetProperty(ref _currentPage, value))
                {
                    UpdateScrollViewer();
                }
            }
        }

        public string? SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                if (SetProperty(ref _searchKeyword, value))
                {
                    _itemsView.Refresh();
                }
            }
        }

        public string? SelectedItem
        {
            get => _selectedItem;
            set 
            {
                if (SetProperty(ref _selectedItem, value) && !string.IsNullOrEmpty(_selectedItem))
                {
                    CurrentPage = new CarBrandPage(_selectedItem, _selectedLanguage);
                }
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        private bool ItemsFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchKeyword))
            {
                return true;
            }

            return obj is string item && item.ToLower().Contains(_searchKeyword!.ToLower());
        }

        public double VerticalOffset
        {
            get => _verticalOffset;
            set => SetProperty(ref _verticalOffset, value);
        }

        private void UpdateScrollViewer()
        {
            OnVerticalOffsetChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnVerticalOffsetChanged;

        public int ToggleButtonState
        {
            get { return _toggleButtonState; }
            set
            {
                if (SetProperty(ref _toggleButtonState, value))
                {
                    if (ToggleButtonState == 0)
                    {
                        AppTheme.ChangeTheme(Theme.Light);    
                    }
                    else if (ToggleButtonState == 1)
                    {
                        AppTheme.ChangeTheme(Theme.Dark);
                    }
                }
            }
        }

        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (SetProperty(ref _selectedLanguage, value))
                {
                    AppLanguage.ChangeLanguage(Enum.Parse<Language>(_selectedLanguage));
                }
            }
        }





    }
}
