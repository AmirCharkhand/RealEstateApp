using System.Text.Json.Serialization;

namespace RealEstateApp.Models
{
    public class LoginToken (string accessToken, int userId, string userName)
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = accessToken;
        [JsonPropertyName("user_id")]
        public int UserId{ get; init; } = userId;
        [JsonPropertyName("user_name")]
        public string UserName { get; init; } = userName;
    }
}
