namespace RealEstateApp.Models
{
    public class NewUser (string name, string email, string password, string phone)
    {
        public string Name { get; init; } = name;
        public string Password { get; init; } = password;
        public string Email { get; init; } = email;
        public string Phone { get; init; } = phone;
    }
}
