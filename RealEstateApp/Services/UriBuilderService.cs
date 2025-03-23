
namespace RealEstateApp.Services
{
    public class UriBuilderService
    {
        private const string _baseAddress = "http://realestateappapi.somee.com/api";
        private const string _userPath = "users";

        public string BaseAddress => _baseAddress;
        public string UserAddress => $"{_baseAddress}/{_userPath}";

        public Uri GetRegisterUserUri() => new Uri($"{UserAddress}/register");
        public Uri GetLoginUserUri() => new Uri($"{UserAddress}/login");
    }
}
