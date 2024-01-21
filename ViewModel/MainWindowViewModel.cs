using AutoChronicle.Model.Utils;
using AutoChronicle.Resources.Languages;
using AutoChronicle.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace AutoChronicle.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    { 
        public ObservableCollection<string> CarBrands { get; }
        private readonly ICollectionView _itemsView;
        private string? _selectedItem;
        private int _selectedIndex;
        private string? _searchKeyword;
        private double _verticalOffset;
        private object _currentPage;

        private int _toggleButtonState = 1;
        private string _selectedLanguage = "ENG";

        public ICommand OpenInfoCommand { get; set; }
        public ICommand OpenGalleryPageCommand { get; set; }
        public ICommand OpenHomeCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }
        public ICommand ChangeLanguageCommand { get; set; }


        public MainWindowViewModel()
        {
            OpenInfoCommand = new RelayCommand((object obj) => CurrentPage = new InfoPage());
            OpenHomeCommand = new RelayCommand((object obj) => CurrentPage = new HomePage());
            ChangeThemeCommand = new RelayCommand((object obj) => ToggleButtonState = 1 - ToggleButtonState);
            ChangeLanguageCommand = new RelayCommand((object obj) => SelectedLanguage = (string)obj);

            CarBrands = new ObservableCollection<string>(DataReader.ReadCarBrands()); // { "Audi", "Mazda", "BMW", "Mercedes", "Jeep", "Geely", "Honda", "Nissan", "Toyota", "KIA", "Cintroen", "Peugeot", "GAZ", "VAZ", "Subaru", "Mitsubishi", "FSO", "Volvo", "Dodge", "Chevrolet", "Ford"};
            CarBrands = new ObservableCollection<string>(CarBrands.OrderBy(i => i));
            _itemsView = CollectionViewSource.GetDefaultView(CarBrands);
            _itemsView.Filter = ItemsFilter;

            CurrentPage = new HomePage();
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
                    if (CurrentPage is CarBrandPage)
                    {
                        CurrentPage = new CarBrandPage(_selectedItem, _selectedLanguage);
                    }
                }
            }
        }

    }
}
