using AutoChronicle.Model.Utils;
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

        public ICommand OpenInfoCommand { get; set; }
        public ICommand OpenGalleryPageCommand { get; set; }
        public ICommand OpenHomeCommand { get; set; }
        public ICommand OpenCarBrandCommand { get; set; }

        public MainWindowViewModel()
        {
            OpenInfoCommand = new RelayCommand((object obj) => CurrentPage = new InfoPage());

            Items = new ObservableCollection<string>(DataReader.ReadCarBrands()); // { "Audi", "Mazda", "BMW", "Mercedes", "Jeep", "Geely", "Honda", "Nissan", "Toyota", "KIA", "Cintroen", "Peugeot", "GAZ", "VAZ", "Subaru", "Mitsubishi", "FSO", "Volvo", "Dodge", "Chevrolet", "Ford"};
            Items = new ObservableCollection<string>(Items.OrderBy(i => i));

            _itemsView = CollectionViewSource.GetDefaultView(Items);
            _itemsView.Filter = ItemsFilter;
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
                    CurrentPage = new CarBrandPage(_selectedItem);
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
    }
}
