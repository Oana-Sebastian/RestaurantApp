namespace RestaurantApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsEmployee { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}