using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RestaurantApp.Commands;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private object _currentViewModel;
        private bool _isLoggedIn;
        private bool _isClientLoggedIn;
        private bool _isEmployeeLoggedIn;
        private string _currentUserName;

        public MainWindowViewModel(INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;

            NavigateCommand = new RelayCommand<string>(ExecuteNavigate);
            LogoutCommand = new RelayCommand(ExecuteLogout);

            // Set initial view to Menu or whatever your default should be
            // For now, let's set it to login
            NavigateTo("Login");
        }

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsClientLoggedIn
        {
            get => _isClientLoggedIn;
            set
            {
                if (_isClientLoggedIn != value)
                {
                    _isClientLoggedIn = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEmployeeLoggedIn
        {
            get => _isEmployeeLoggedIn;
            set
            {
                if (_isEmployeeLoggedIn != value)
                {
                    _isEmployeeLoggedIn = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CurrentUserName
        {
            get => _currentUserName;
            set
            {
                if (_currentUserName != value)
                {
                    _currentUserName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        private void ExecuteNavigate(string destination)
        {
            NavigateTo(destination);
        }

        private void ExecuteLogout()
        {
            // Clear current user
            App.CurrentUser = null;

            // Reset logged in state
            IsLoggedIn = false;
            IsClientLoggedIn = false;
            IsEmployeeLoggedIn = false;
            CurrentUserName = null;

            // Navigate to login
            NavigateTo("Login");
        }

        public void NavigateTo(string viewName)
        {
            switch (viewName)
            {
                case "Login":
                    CurrentViewModel = new LoginViewModel(_userService, new ViewModelNavigationService(this));
                    break;
                case "Register":
                    // CurrentViewModel = new RegisterViewModel(_userService, new ViewModelNavigationService(this));
                    break;
                case "Menu":
                    // CurrentViewModel = new MenuViewModel();
                    break;
                case "Search":
                    // CurrentViewModel = new SearchViewModel();
                    break;
                case "Order":
                    // CurrentViewModel = new OrderViewModel();
                    break;
                case "OrderHistory":
                    // CurrentViewModel = new OrderHistoryViewModel();
                    break;
                case "Admin":
                    // CurrentViewModel = new AdminViewModel();
                    break;
            }
        }

        public void UserLoggedIn(User user)
        {
            if (user != null)
            {
                IsLoggedIn = true;
                CurrentUserName = user.FullName;

                if (user.IsEmployee)
                {
                    IsEmployeeLoggedIn = true;
                    NavigateTo("Admin"); // Navigate employees to admin panel
                }
                else
                {
                    IsClientLoggedIn = true;
                    NavigateTo("Menu"); // Navigate clients to menu
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}