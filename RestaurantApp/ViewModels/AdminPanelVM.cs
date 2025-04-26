
//using RestaurantApp.ViewModels.Commands;
//using System;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Windows.Controls;
//using System.Windows.Input;
//using RestaurantApp.Commands;

//namespace RestaurantApp.ViewModels
//{
//    public class AdminPanelViewModel : INotifyPropertyChanged
//    {
//        private readonly IDatabaseService _databaseService;

//        #region Properties

//        // Categories
//        private ObservableCollection<Category> _categories;
//        public ObservableCollection<Category> Categories
//        {
//            get { return _categories; }
//            set
//            {
//                _categories = value;
//                OnPropertyChanged(nameof(Categories));
//            }
//        }

//        private Category _selectedCategory;
//        public Category SelectedCategory
//        {
//            get { return _selectedCategory; }
//            set
//            {
//                _selectedCategory = value;
//                if (_selectedCategory != null)
//                {
//                    CategoryName = _selectedCategory.Name;
//                    CategoryDescription = _selectedCategory.Description;
//                }
//                OnPropertyChanged(nameof(SelectedCategory));
//            }
//        }

//        private string _categoryName;
//        public string CategoryName
//        {
//            get { return _categoryName; }
//            set
//            {
//                _categoryName = value;
//                OnPropertyChanged(nameof(CategoryName));
//            }
//        }

//        private string _categoryDescription;
//        public string CategoryDescription
//        {
//            get { return _categoryDescription; }
//            set
//            {
//                _categoryDescription = value;
//                OnPropertyChanged(nameof(CategoryDescription));
//            }
//        }

//        // Products
//        private ObservableCollection<Product> _products;
//        public ObservableCollection<Product> Products
//        {
//            get { return _products; }
//            set
//            {
//                _products = value;
//                OnPropertyChanged(nameof(Products));
//            }
//        }

//        private Product _selectedProduct;
//        public Product SelectedProduct
//        {
//            get { return _selectedProduct; }
//            set
//            {
//                _selectedProduct = value;
//                if (_selectedProduct != null)
//                {
//                    ProductName = _selectedProduct.Name;
//                    ProductPrice = _selectedProduct.Price;
//                    ProductPortionSize = _selectedProduct.PortionSize;
//                    ProductTotalQuantity = _selectedProduct.TotalQuantity;
//                    SelectedProductCategory = Categories.FirstOrDefault(c => c.Id == _selectedProduct.CategoryId);

//                    // Update allergens selection
//                    foreach (var allergen in Allergens)
//                    {
//                        allergen.IsSelected = _selectedProduct.Allergens.Any(a => a.Id == allergen.Id);
//                    }
//                }
//                OnPropertyChanged(nameof(SelectedProduct));
//            }
//        }

//        private string _productName;
//        public string ProductName
//        {
//            get { return _productName; }
//            set
//            {
//                _productName = value;
//                OnPropertyChanged(nameof(ProductName));
//            }
//        }

//        private decimal _productPrice;
//        public decimal ProductPrice
//        {
//            get { return _productPrice; }
//            set
//            {
//                _productPrice = value;
//                OnPropertyChanged(nameof(ProductPrice));
//            }
//        }

//        private string _productPortionSize;
//        public string ProductPortionSize
//        {
//            get { return _productPortionSize; }
//            set
//            {
//                _productPortionSize = value;
//                OnPropertyChanged(nameof(ProductPortionSize));
//            }
//        }

//        private decimal _productTotalQuantity;
//        public decimal ProductTotalQuantity
//        {
//            get { return _productTotalQuantity; }
//            set
//            {
//                _productTotalQuantity = value;
//                OnPropertyChanged(nameof(ProductTotalQuantity));
//            }
//        }

//        private Category _selectedProductCategory;
//        public Category SelectedProductCategory
//        {
//            get { return _selectedProductCategory; }
//            set
//            {
//                _selectedProductCategory = value;
//                OnPropertyChanged(nameof(SelectedProductCategory));
//            }
//        }

//        private Category _selectedCategoryFilter;
//        public Category SelectedCategoryFilter
//        {
//            get { return _selectedCategoryFilter; }
//            set
//            {
//                _selectedCategoryFilter = value;
//                FilterProducts();
//                OnPropertyChanged(nameof(SelectedCategoryFilter));
//            }
//        }

//        // Menus
//        private ObservableCollection<Menu> _menus;
//        public ObservableCollection<Menu> Menus
//        {
//            get { return _menus; }
//            set
//            {
//                _menus = value;
//                OnPropertyChanged(nameof(Menus));
//            }
//        }

//        private Menu _selectedMenu;
//        public Menu SelectedMenu
//        {
//            get { return _selectedMenu; }
//            set
//            {
//                _selectedMenu = value;
//                if (_selectedMenu != null)
//                {
//                    MenuName = _selectedMenu.Name;
//                    SelectedMenuCategory = Categories.FirstOrDefault(c => c.Id == _selectedMenu.CategoryId);
//                    MenuProducts.Clear();
//                    foreach (var menuProduct in _selectedMenu.MenuProducts)
//                    {
//                        MenuProducts.Add(new MenuProductItem
//                        {
//                            Product = menuProduct.Product,
//                            Quantity = menuProduct.Quantity
//                        });
//                    }
//                }
//                OnPropertyChanged(nameof(SelectedMenu));
//            }
//        }

//        private string _menuName;
//        public string MenuName
//        {
//            get { return _menuName; }
//            set
//            {
//                _menuName = value;
//                OnPropertyChanged(nameof(MenuName));
//            }
//        }

//        private Category _selectedMenuCategory;
//        public Category SelectedMenuCategory
//        {
//            get { return _selectedMenuCategory; }
//            set
//            {
//                _selectedMenuCategory = value;
//                OnPropertyChanged(nameof(SelectedMenuCategory));
//            }
//        }

//        private Category _selectedMenuCategoryFilter;
//        public Category SelectedMenuCategoryFilter
//        {
//            get { return _selectedMenuCategoryFilter; }
//            set
//            {
//                _selectedMenuCategoryFilter = value;
//                FilterMenus();
//                OnPropertyChanged(nameof(SelectedMenuCategoryFilter));
//            }
//        }

//        private ObservableCollection<Product> _availableProducts;
//        public ObservableCollection<Product> AvailableProducts
//        {
//            get { return _availableProducts; }
//            set
//            {
//                _availableProducts = value;
//                OnPropertyChanged(nameof(AvailableProducts));
//            }
//        }

//        private Product _selectedProductToAdd;
//        public Product SelectedProductToAdd
//        {
//            get { return _selectedProductToAdd; }
//            set
//            {
//                _selectedProductToAdd = value;
//                OnPropertyChanged(nameof(SelectedProductToAdd));
//            }
//        }

//        private decimal _productQuantity = 1;
//        public decimal ProductQuantity
//        {
//            get { return _productQuantity; }
//            set
//            {
//                _productQuantity = value;
//                OnPropertyChanged(nameof(ProductQuantity));
//            }
//        }

//        private ObservableCollection<MenuProductItem> _menuProducts;
//        public ObservableCollection<MenuProductItem> MenuProducts
//        {
//            get { return _menuProducts; }
//            set
//            {
//                _menuProducts = value;
//                OnPropertyChanged(nameof(MenuProducts));
//            }
//        }

//        // Allergens
//        private ObservableCollection<AllergenViewModel> _allergens;
//        public ObservableCollection<AllergenViewModel> Allergens
//        {
//            get { return _allergens; }
//            set
//            {
//                _allergens = value;
//                OnPropertyChanged(nameof(Allergens));
//            }
//        }

//        private AllergenViewModel _selectedAllergen;
//        public AllergenViewModel SelectedAllergen
//        {
//            get { return _selectedAllergen; }
//            set
//            {
//                _selectedAllergen = value;
//                if (_selectedAllergen != null)
//                {
//                    AllergenName = _selectedAllergen.Name;
//                    AllergenDescription = _selectedAllergen.Description;
//                }
//                OnPropertyChanged(nameof(SelectedAllergen));
//            }
//        }

//        private string _allergenName;
//        public string AllergenName
//        {
//            get { return _allergenName; }
//            set
//            {
//                _allergenName = value;
//                OnPropertyChanged(nameof(AllergenName));
//            }
//        }

//        private string _allergenDescription;
//        public string AllergenDescription
//        {
//            get { return _allergenDescription; }
//            set
//            {
//                _allergenDescription = value;
//                OnPropertyChanged(nameof(AllergenDescription));
//            }
//        }

//        #endregion

//        #region Commands

//        // Category Commands
//        public ICommand NewCategoryCommand { get; private set; }
//        public ICommand SaveCategoryCommand { get; private set; }
//        public ICommand DeleteCategoryCommand { get; private set; }

//        // Product Commands
//        public ICommand NewProductCommand { get; private set; }
//        public ICommand SaveProductCommand { get; private set; }
//        public ICommand DeleteProductCommand { get; private set; }
//        public ICommand ClearProductFilterCommand { get; private set; }

//        // Menu Commands
//        public ICommand NewMenuCommand { get; private set; }
//        public ICommand SaveMenuCommand { get; private set; }
//        public ICommand DeleteMenuCommand { get; private set; }
//        public ICommand ClearMenuFilterCommand { get; private set; }
//        public ICommand AddProductToMenuCommand { get; private set; }
//        public ICommand RemoveProductFromMenuCommand { get; private set; }

//        // Allergen Commands
//        public ICommand NewAllergenCommand { get; private set; }
//        public ICommand SaveAllergenCommand { get; private set; }
//        public ICommand DeleteAllergenCommand { get; private set; }

//        #endregion

//        public AdminPanelViewModel()
//        {
//            _databaseService = ServiceLocator.Instance.GetService<IDatabaseService>();

//            // Initialize collections
//            Categories = new ObservableCollection<Category>();
//            Products = new ObservableCollection<Product>();
//            Menus = new ObservableCollection<Menu>();
//            Allergens = new ObservableCollection<AllergenViewModel>();
//            MenuProducts = new ObservableCollection<MenuProductItem>();
//            AvailableProducts = new ObservableCollection<Product>();

//            // Initialize commands
//            InitializeCommands();

//            // Load data
//            LoadData();
//        }

//        private void InitializeCommands()
//        {
//            // Category Commands
//            NewCategoryCommand = new RelayCommand(param => NewCategory());
//            SaveCategoryCommand = new RelayCommand(param => SaveCategory(), param => CanSaveCategory());
//            DeleteCategoryCommand = new RelayCommand(param => DeleteCategory(), param => CanDeleteCategory());

//            // Product Commands
//            NewProductCommand = new RelayCommand(param => NewProduct());
//            SaveProductCommand = new RelayCommand(param => SaveProduct(), param => CanSaveProduct());
//            DeleteProductCommand = new RelayCommand(param => DeleteProduct(), param => CanDeleteProduct());
//            ClearProductFilterCommand = new RelayCommand(param => ClearProductFilter());

//            // Menu Commands
//            NewMenuCommand = new RelayCommand(param => NewMenu());
//            SaveMenuCommand = new RelayCommand(param => SaveMenu(), param => CanSaveMenu());
//            DeleteMenuCommand = new RelayCommand(param => DeleteMenu(), param => CanDeleteMenu());
//            ClearMenuFilterCommand = new RelayCommand(param => ClearMenuFilter());
//            AddProductToMenuCommand = new RelayCommand(param => AddProductToMenu(), param => CanAddProductToMenu());
//            RemoveProductFromMenuCommand = new RelayCommand(param => RemoveProductFromMenu(param as MenuProductItem), param => true);

//            // Allergen Commands
//            NewAllergenCommand = new RelayCommand(param => NewAllergen());
//            SaveAllergenCommand = new RelayCommand(param => SaveAllergen(), param => CanSaveAllergen());
//            DeleteAllergenCommand = new RelayCommand(param => DeleteAllergen(), param => CanDeleteAllergen());
//        }

//        private async void LoadData()
//        {
//            try
//            {
//                // Load categories
//                var categories = await _databaseService.GetCategoriesAsync();
//                Categories.Clear();
//                foreach (var category in categories)
//                {
//                    Categories.Add(category);
//                }

//                // Load products
//                var products = await _databaseService.GetProductsAsync();
//                Products.Clear();
//                foreach (var product in products)
//                {
//                    Products.Add(product);
//                }

//                // Load available products for menus
//                AvailableProducts.Clear();
//                foreach (var product in products)
//                {
//                    AvailableProducts.Add(product);
//                }

//                // Load menus
//                var menus = await _databaseService.GetMenusAsync();
//                Menus.Clear();
//                foreach (var menu in menus)
//                {
//                    Menus.Add(menu);
//                }

//                // Load allergens
//                var allergens = await _databaseService.GetAllergensAsync();
//                Allergens.Clear();
//                foreach (var allergen in allergens)
//                {
//                    Allergens.Add(new AllergenViewModel
//                    {
//                        Id = allergen.Id,
//                        Name = allergen.Name,
//                        Description = allergen.Description,
//                        IsSelected = false
//                    });
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle exception (e.g., show message to user)
//                System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
//            }
//        }

//        #region Category Methods

//        private void NewCategory()
//        {
//            SelectedCategory = null;
//            CategoryName = string.Empty;
//            CategoryDescription = string.Empty;
//        }

//        private async void SaveCategory()
//        {
//            try
//            {
//                if (SelectedCategory == null)
//                {
//                    // Create new category
//                    var newCategory = new Category
//                    {
//                        Name = CategoryName,
//                        Description = CategoryDescription
//                    };

//                    var categoryId = await _databaseService.AddCategoryAsync(newCategory);
//                    newCategory.Id = categoryId;
//                    Categories.Add(newCategory);
//                }
//                else
//                {
//                    // Update existing category
//                    SelectedCategory.Name = CategoryName;
//                    SelectedCategory.Description = CategoryDescription;
//                    await _databaseService.UpdateCategoryAsync(SelectedCategory);
//                }

//                // Refresh data
//                LoadData();
//            }
//            catch (Exception ex)
//            {
//                // Handle exception
//                System.Diagnostics.Debug.WriteLine($"Error saving category: {ex.Message}");
//            }
//        }

//        private async void DeleteCategory()
//        {
//            if (SelectedCategory != null)
//            {
//                try
//                {
//                    await _databaseService.DeleteCategoryAsync(SelectedCategory.Id);
//                    Categories.Remove(SelectedCategory);
//                    NewCategory();
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception
//                    System.Diagnostics.Debug.WriteLine($"Error deleting category: {ex.Message}");
//                }
//            }
//        }

//        private bool CanSaveCategory()
//        {
//            return !string.IsNullOrWhiteSpace(CategoryName);
//        }

//        private bool CanDeleteCategory()
//        {
//            return SelectedCategory != null;
//        }

//        #endregion

//        #region Product Methods

//        private void NewProduct()
//        {
//            SelectedProduct = null;
//            ProductName = string.Empty;
//            ProductPrice = 0;
//            ProductPortionSize = string.Empty;
//            ProductTotalQuantity = 0;
//            SelectedProductCategory = null;

//            // Reset allergen selection
//            foreach (var allergen in Allergens)
//            {
//                allergen.IsSelected = false;
//            }
//        }

//        private async void SaveProduct()
//        {
//            try
//            {
//                // Collect selected allergens
//                var selectedAllergens = Allergens
//                    .Where(a => a.IsSelected)
//                    .Select(a => new Allergen { Id = a.Id, Name = a.Name, Description = a.Description })
//                    .ToList();

//                if (SelectedProduct == null)
//                {
//                    // Create new product
//                    var newProduct = new Product
//                    {
//                        Name = ProductName,
//                        Price = ProductPrice,
//                        PortionSize = ProductPortionSize,
//                        TotalQuantity = ProductTotalQuantity,
//                        CategoryId = SelectedProductCategory?.Id ?? 0,
//                        Category = SelectedProductCategory,
//                        Allergens = selectedAllergens
//                    };

//                    var productId = await _databaseService.AddProductAsync(newProduct, selectedAllergens.Select(a => a.Id).ToList());
//                    newProduct.Id = productId;
//                    Products.Add(newProduct);
//                }
//                else
//                {
//                    // Update existing product
//                    SelectedProduct.Name = ProductName;
//                    SelectedProduct.Price = ProductPrice;
//                    SelectedProduct.PortionSize = ProductPortionSize;
//                    SelectedProduct.TotalQuantity = ProductTotalQuantity;
//                    SelectedProduct.CategoryId = SelectedProductCategory?.Id ?? 0;
//                    SelectedProduct.Category = SelectedProductCategory;
//                    SelectedProduct.Allergens = selectedAllergens;

//                    await _databaseService.UpdateProductAsync(SelectedProduct, selectedAllergens.Select(a => a.Id).ToList());
//                }

//                // Refresh data
//                LoadData();
//            }
//            catch (Exception ex)
//            {
//                // Handle exception
//                System.Diagnostics.Debug.WriteLine($"Error saving product: {ex.Message}");
//            }
//        }

//        private async void DeleteProduct()
//        {
//            if (SelectedProduct != null)
//            {
//                try
//                {
//                    await _databaseService.DeleteProductAsync(SelectedProduct.Id);
//                    Products.Remove(SelectedProduct);
//                    NewProduct();
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception
//                    System.Diagnostics.Debug.WriteLine($"Error deleting product: {ex.Message}");
//                }
//            }
//        }

//        private bool CanSaveProduct()
//        {
//            return !string.IsNullOrWhiteSpace(ProductName) &&
//                   ProductPrice > 0 &&
//                   !string.IsNullOrWhiteSpace(ProductPortionSize) &&
//                   ProductTotalQuantity >= 0 &&
//                   SelectedProductCategory != null;
//        }

//        private bool CanDeleteProduct()
//        {
//            return SelectedProduct != null;
//        }

//        private void FilterProducts()
//        {
//            if (SelectedCategoryFilter != null)
//            {
//                var filteredProducts = _databaseService.GetProductsAsync().Result
//                    .Where(p => p.CategoryId == SelectedCategoryFilter.Id)
//                    .ToList();

//                Products.Clear();
//                foreach (var product in filteredProducts)
//                {
//                    Products.Add(product);
//                }
//            }
//            else
//            {
//                LoadData();
//            }
//        }

//        private void ClearProductFilter()
//        {
//            SelectedCategoryFilter = null;
//            LoadData();
//        }

//        #endregion

//        #region Menu Methods

//        private void NewMenu()
//        {
//            SelectedMenu = null;
//            MenuName = string.Empty;
//            SelectedMenuCategory = null;
//            MenuProducts.Clear();
//        }

//        private async void SaveMenu()
//        {
//            try
//            {
//                if (SelectedMenu == null)
//                {
//                    // Create new menu
//                    var newMenu = new Menu
//                    {
//                        Name = MenuName,
//                        CategoryId = SelectedMenuCategory?.Id ?? 0,
//                        Category = SelectedMenuCategory,
//                        MenuProducts = MenuProducts.Select(mp => new MenuProduct
//                        {
//                            ProductId = mp.Product.Id,
//                            Product = mp.Product,
//                            Quantity = mp.Quantity
//                        }).ToList()
//                    };

//                    // Calculate price with discount
//                    decimal totalPrice = MenuProducts.Sum(mp => mp.Product.Price * mp.Quantity);
//                    decimal discountPercentage = AppSettings.Instance.MenuDiscount; // Read from settings
//                    newMenu.Price = totalPrice * (1 - discountPercentage / 100);

//                    var menuId = await _databaseService.AddMenuAsync(newMenu,
//                        MenuProducts.Select(mp => new Tuple<int, decimal>(mp.Product.Id, mp.Quantity)).ToList());
//                    newMenu.Id = menuId;
//                    Menus.Add(newMenu);
//                }
//                else
//                {
//                    // Update existing menu
//                    SelectedMenu.Name = MenuName;
//                    SelectedMenu.CategoryId = SelectedMenuCategory?.Id ?? 0;
//                    SelectedMenu.Category = SelectedMenuCategory;

//                    var menuProducts = MenuProducts.Select(mp => new MenuProduct
//                    {
//                        MenuId = SelectedMenu.Id,
//                        ProductId = mp.Product.Id,
//                        Product = mp.Product,
//                        Quantity = mp.Quantity
//                    }).ToList();

//                    SelectedMenu.MenuProducts = menuProducts;

//                    // Calculate price with discount
//                    decimal totalPrice = MenuProducts.Sum(mp => mp.Product.Price * mp.Quantity);
//                    decimal discountPercentage = AppSettings.Instance.MenuDiscount; // Read from settings
//                    SelectedMenu.Price = totalPrice * (1 - discountPercentage / 100);

//                    await _databaseService.UpdateMenuAsync(SelectedMenu,
//                        MenuProducts.Select(mp => new Tuple<int, decimal>(mp.Product.Id, mp.Quantity)).ToList());
//                }

//                // Refresh data
//                LoadData();
//            }
//            catch (Exception ex)
//            {
//                // Handle exception
//                System.Diagnostics.Debug.WriteLine($"Error saving menu: {ex.Message}");
//            }
//        }

//        private async void DeleteMenu()
//        {
//            if (SelectedMenu != null)
//            {
//                try
//                {
//                    await _databaseService.DeleteMenuAsync(SelectedMenu.Id);
//                    Menus.Remove(SelectedMenu);
//                    NewMenu();
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception
//                    System.Diagnostics.Debug.WriteLine($"Error deleting menu: {ex.Message}");
//                }
//            }
//        }

//        private bool CanSaveMenu()
//        {
//            return !string.IsNullOrWhiteSpace(MenuName) &&
//                   SelectedMenuCategory != null &&
//                   MenuProducts.Count > 0;
//        }

//        private bool CanDeleteMenu()
//        {
//            return SelectedMenu != null;
//        }

//        private void FilterMenus()
//        {
//            if (SelectedMenuCategoryFilter != null)
//            {
//                var filteredMenus = _databaseService.GetMenusAsync().Result
//                    .Where(m => m.CategoryId == SelectedMenuCategoryFilter.Id)
//                    .ToList();

//                Menus.Clear();
//                foreach (var menu in filteredMenus)
//                {
//                    Menus.Add(menu);
//                }
//            }
//            else
//            {
//                LoadData();
//            }
//        }

//        private void ClearMenuFilter()
//        {
//            SelectedMenuCategoryFilter = null;
//            LoadData();
//        }

//        private void AddProductToMenu()
//        {
//            if (SelectedProductToAdd != null && ProductQuantity > 0)
//            {
//                var existingItem = MenuProducts.FirstOrDefault(mp => mp.Product.Id == SelectedProductToAdd.Id);

//                if (existingItem != null)
//                {
//                    // Update quantity if product already exists in menu
//                    existingItem.Quantity += ProductQuantity;
//                }
//                else
//                {
//                    // Add new product to menu
//                    MenuProducts.Add(new MenuProductItem
//                    {
//                        Product = SelectedProductToAdd,
//                        Quantity = ProductQuantity
//                    });
//                }

//                // Reset selection and quantity
//                SelectedProductToAdd = null;
//                ProductQuantity = 1;
//            }
//        }

//        private void RemoveProductFromMenu(MenuProductItem menuProduct)
//        {
//            if (menuProduct != null)
//            {
//                MenuProducts.Remove(menuProduct);
//            }
//        }

//        private bool CanAddProductToMenu()
//        {
//            return SelectedProductToAdd != null && ProductQuantity > 0;
//        }

//        #endregion

//        #region Allergen Methods

//        private void NewAllergen()
//        {
//            SelectedAllergen = null;
//            AllergenName = string.Empty;
//            AllergenDescription = string.Empty;
//        }

//        private async void SaveAllergen()
//        {
//            try
//            {
//                if (SelectedAllergen == null)
//                {
//                    // Create new allergen
//                    var newAllergen = new Allergen
//                    {
//                        Name = AllergenName,
//                        Description = AllergenDescription
//                    };

//                    var allergenId = await _databaseService.AddAllergenAsync(newAllergen);

//                    // Add to observable collection
//                    Allergens.Add(new AllergenViewModel
//                    {
//                        Id = allergenId,
//                        Name = AllergenName,
//                        Description = AllergenDescription,
//                        IsSelected = false
//                    });
//                }
//                else
//                {
//                    // Update existing allergen
//                    var allergen = new Allergen
//                    {
//                        Id = SelectedAllergen.Id,
//                        Name = AllergenName,
//                        Description = AllergenDescription
//                    };

//                    await _databaseService.UpdateAllergenAsync(allergen);

//                    // Update in observable collection
//                    SelectedAllergen.Name = AllergenName;
//                    SelectedAllergen.Description = AllergenDescription;
//                }

//                // Refresh data
//                LoadData();
//            }
//            catch (Exception ex)
//            {
//                // Handle exception
//                System.Diagnostics.Debug.WriteLine($"Error saving allergen: {ex.Message}");
//            }
//        }

//        private async void DeleteAllergen()
//        {
//            if (SelectedAllergen != null)
//            {
//                try
//                {
//                    await _databaseService.DeleteAllergenAsync(SelectedAllergen.Id);
//                    Allergens.Remove(SelectedAllergen);
//                    NewAllergen();
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception
//                    System.Diagnostics.Debug.WriteLine($"Error deleting allergen: {ex.Message}");
//                }
//            }
//        }

//        private bool CanSaveAllergen()
//        {
//            return !string.IsNullOrWhiteSpace(AllergenName);
//        }

//        private bool CanDeleteAllergen()
//        {
//            return SelectedAllergen != null;
//        }

//        #endregion

//        #region INotifyPropertyChanged Implementation

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        #endregion
//    }

//    // Helper classes
//    public class AllergenViewModel : INotifyPropertyChanged
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public string Description { get; set; }

//        private bool _isSelected;
//        public bool IsSelected
//        {
//            get { return _isSelected; }
//            set
//            {
//                _isSelected = value;
//                OnPropertyChanged(nameof(IsSelected));
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    public class MenuProductItem : INotifyPropertyChanged
//    {
//        public Product Product { get; set; }

//        private decimal _quantity;
//        public decimal Quantity
//        {
//            get { return _quantity; }
//            set
//            {
//                _quantity = value;
//                OnPropertyChanged(nameof(Quantity));
//            }
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}