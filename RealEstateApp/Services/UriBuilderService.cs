
namespace RealEstateApp.Services
{
    public class UriBuilderService
    {
        private const string _baseAddress = "http://realestateappapi.somee.com/api";
        private const string _userPath = "users";
        private const string _categoryPath = "categories";
        private const string _propertyPath = "Properties";

        public string BaseAddress => _baseAddress;
        public string UserAddress => $"{_baseAddress}/{_userPath}";
        public string CategoriesAddress => $"{_baseAddress}/{_categoryPath}";
        public string PropertiesAddress => $"{_baseAddress}/{_propertyPath}";

        public Uri GetRegisterUserUri() => new Uri($"{UserAddress}/register");
        public Uri GetLoginUserUri() => new Uri($"{UserAddress}/login");
        public Uri GetPropertiesListUri(int categoryId) => new Uri($"{PropertiesAddress}/PropertyList?categoryId={categoryId}");
        public Uri GetPropertyDetailsUri(int propertyId) => new Uri($"{PropertiesAddress}/PropertyDetail?id={propertyId}");
        public Uri GetTrendingPropertiesUri() => new Uri($"{PropertiesAddress}/TrendingProperties");
        public Uri GetSearchPropertiesUri(string address) => new Uri($"{PropertiesAddress}/SearchProperties?address={address}");
    }
}
