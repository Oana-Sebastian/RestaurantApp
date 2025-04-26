using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RestaurantApp.Views;
using RestaurantApp.Commands;
using RestaurantApp.Services;

namespace RestaurantApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private string _email;
        private string _errorMessage;

        public LoginViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand<PasswordBox>(ExecuteLoginCommand, CanExecuteLoginCommand);
            NavigateToRegisterCommand = new RelayCommand(ExecuteNavigateToRegister);
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    // Clear error message when user starts typing
                    if (!string.IsNullOrEmpty(ErrorMessage))
                    {
                        ErrorMessage = string.Empty;
                    }
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private bool CanExecuteLoginCommand(PasswordBox passwordBox)
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   passwordBox != null &&
                   !string.IsNullOrWhiteSpace(passwordBox.Password);
        }

        // In LoginViewModel class
        private void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            try
            {
                var user = _userService.Authenticate(Email, passwordBox.Password);

                if (user != null)
                {
                    // Store user info in a global session
                    App.CurrentUser = user;

                    // Notify the navigation service that user logged in
                    ((ViewModelNavigationService)_navigationService).UserLoggedIn(user);
                }
                else
                {
                    ErrorMessage = "Invalid email or password. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }

        private void ExecuteNavigateToRegister()
        {
            _navigationService.NavigateTo<RegisterView>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}