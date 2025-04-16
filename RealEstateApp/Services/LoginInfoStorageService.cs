using RealEstateApp.Models;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstateApp.Services
{
    public class LoginInfoStorageService
    {
        private LoginToken? _loginToken;
        public async Task SaveLoginInfoAsync(LoginToken loginToken)
        {
            await SecureStorage.SetAsync("AccessToken", loginToken.AccessToken);
            await SecureStorage.SetAsync("UserId", loginToken.UserId.ToString());
            await SecureStorage.SetAsync("UserName", loginToken.UserName);
        }

        public async Task<LoginToken?> GetLoginInfoAsync()
        {
            if (_loginToken != null)
                return _loginToken;

            _loginToken = await GetTokenFromSecureStorage();
            return _loginToken;
        }

        public async Task<bool> IsLoginTokenValid()
        {
            var token = await GetTokenFromSecureStorage();
            if (token == null)
                return false;

            var tokenExpiration = new JwtSecurityTokenHandler()
                .ReadJwtToken(token.AccessToken)
                .ValidTo;
            if (tokenExpiration < DateTime.UtcNow + TimeSpan.FromHours(1))
            {
                ClearLoginInfo();
                return false;
            }

            return true;
        }

        public void ClearLoginInfo()
        {
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("UserId");
            SecureStorage.Remove("UserName");
            _loginToken = null;
        }

        private async Task<LoginToken?> GetTokenFromSecureStorage()
        {
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            var userId = await SecureStorage.GetAsync("UserId");
            var userName = await SecureStorage.GetAsync("UserName");
            if (accessToken == null || userId == null || userName == null)
                return null;
            return new LoginToken(accessToken, int.Parse(userId), userName);
        }
    }
}
