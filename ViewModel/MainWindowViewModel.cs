using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AutoChronicle.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    { 
        public ObservableCollection<string> Items { get; }
        private readonly ICollectionView _itemsView;
        private string? _searchKeyword;

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<string> { "Audi", "Mazda", "BMW", "Mercedes", "Jeep", "Geely", "Honda", "Nissan", "Toyota", "KIA", "Cintroen", "Peugeot", "GAZ", "VAZ", "Subaru", "Mitsubishi", "FSO", "Volvo", "Dodge", "Chevrolet", "Ford"};
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

        private bool ItemsFilter(object obj)
        {
            if (string.IsNullOrEmpty(_searchKeyword))
            {
                return true;
            }

            return obj is string item && item.ToLower().Contains(_searchKeyword!.ToLower());
        }
    }
}
