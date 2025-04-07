using RealEstateApp.Models;
using System.IdentityModel.Tokens.Jwt;

namespace RealEstateApp.Services
{
    public class LoginInfoStorageService
    {
        public async Task SaveLoginInfoAsync(LoginToken loginToken)
        {
            await SecureStorage.SetAsync("AccessToken", loginToken.AccessToken);
            await SecureStorage.SetAsync("UserId", loginToken.UserId.ToString());
            await SecureStorage.SetAsync("UserName", loginToken.UserName);
        }

        public async Task<LoginToken?> GetLoginInfoAsync()
        {
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            var userId = await SecureStorage.GetAsync("UserId");
            var userName = await SecureStorage.GetAsync("UserName");

            if (accessToken == null || userId == null || userName == null) 
                return null;

            return new LoginToken(accessToken, int.Parse(userId), userName);
        }

        public async Task<bool> IsLoginTokenValid()
        {
            var accessToken = await SecureStorage.GetAsync("AccessToken");
            var userId = await SecureStorage.GetAsync("UserId");
            var userName = await SecureStorage.GetAsync("UserName");
            if (accessToken == null || userId == null || userName == null)
                return false;

            var tokenExpiration = new JwtSecurityTokenHandler()
                .ReadJwtToken(accessToken)
                .ValidTo;
            if (tokenExpiration < DateTime.UtcNow + TimeSpan.FromHours(1))
            {
                ClearLoginInfoAsync();
                return false;
            }

            return true;
        }

        public void ClearLoginInfoAsync()
        {
            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("UserId");
            SecureStorage.Remove("UserName");
        }
    }
}
