using RestaurantApp.Models;
using RestaurantApp.ViewModels;

namespace RestaurantApp.Services
{
    public class ViewModelNavigationService : INavigationService
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public ViewModelNavigationService(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void NavigateTo<T>() where T : class
        {
            // Map the type to a view name
            string viewName = typeof(T).Name.Replace("View", "");
            _mainWindowViewModel.NavigateTo(viewName);
        }

        public void UserLoggedIn(User user)
        {
            _mainWindowViewModel.UserLoggedIn(user);
        }
    }
}