using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Models;


namespace RestaurantApp.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}