
//using RestaurantApp.Commands;
//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Input;

//namespace RestaurantApp.ViewModels
//{
//    public class LowStockItemsViewModel : INotifyPropertyChanged
//    {
//        private readonly IDatabaseService _databaseService;

//        private ObservableCollection<Product> _lowStockItems;
//        public ObservableCollection<Product> LowStockItems
//        {
//            get { return _lowStockItems; }
//            set
//            {
//                _lowStockItems = value;
//                OnPropertyChanged(nameof(LowStockItems));
//            }
//        }

//        private decimal _stockThreshold;
//        public decimal StockThreshold
//        {
//            get { return _stockThreshold; }
//            set
//            {
//                _stockThreshold = value;
//                OnPropertyChanged(nameof(StockThreshold));
//                LoadLowStockItems();
//            }
//        }

//        private string _statusMessage;
//        public string StatusMessage
//        {
//            get { return _statusMessage; }
//            set
//            {
//                _statusMessage = value;
//                OnPropertyChanged(nameof(StatusMessage));
//            }
//        }

//        public ICommand RefreshCommand { get; private set; }

//        public LowStockItemsViewModel()
//        {
//            _databaseService = ServiceLocator.Instance.GetService<IDatabaseService>();
//            LowStockItems = new ObservableCollection<Product>();

//            // Initialize with threshold from settings
//            StockThreshold = AppSettings.Instance.LowStockThreshold;

//            // Initialize commands
//            RefreshCommand = new RelayCommand(param => LoadLowStockItems());

//            // Load initial data
//            LoadLowStockItems();
//        }

//        private async void LoadLowStockItems()
//        {
//            try
//            {
//                StatusMessage = "Loading low stock items...";

//                // Get all products
//                var allProducts = await _databaseService.GetProductsAsync();

//                // Filter products with stock below threshold
//                var lowStockProducts = allProducts
//                    .Where(p => p.TotalQuantity <= StockThreshold)
//                    .OrderBy(p => p.TotalQuantity)
//                    .ToList();

//                // Update the collection
//                LowStockItems.Clear();
//                foreach (var product in lowStockProducts)
//                {
//                    LowStockItems.Add(product);
//                }

//                StatusMessage = $"Found {LowStockItems.Count} items with stock below threshold ({StockThreshold})";
//            }
//            catch (Exception ex)
//            {
//                StatusMessage = $"Error: {ex.Message}";
//                System.Diagnostics.Debug.WriteLine($"Error loading low stock items: {ex.Message}");
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    // Converter to determine stock level status
//    public class StockLevelConverter : System.Windows.Data.IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            if (value is decimal quantity)
//            {
//                if (quantity <= AppSettings.Instance.CriticalStockThreshold)
//                    return "Critical";
//                else if (quantity <= AppSettings.Instance.LowStockThreshold)
//                    return "Low";
//            }
//            return "Normal";
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}