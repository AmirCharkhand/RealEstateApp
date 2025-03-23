
namespace RealEstateApp.Models
{
    public class UserLogin (string email, string password)
    {
        public string Email { get; init; } = email;
        public string Password { get; init; } = password;
    }
}