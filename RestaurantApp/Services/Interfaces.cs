using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Models;
namespace RestaurantApp.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        bool RegisterUser(User user, string password);

    }

    public interface INavigationService
    {
        void NavigateTo<T>() where T : class;

    }
}
