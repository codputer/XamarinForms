using Newtonsoft.Json;

namespace App2
{
    public class LoggedInUser
    {
        [JsonProperty(".expires")]
        public string Expires { get; set; }
        [JsonProperty(".issued")]
        public string Issued { get; set; }
        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("roleId")]
        public string RoleId { get; set; }
        [JsonProperty("roleName")]
        public string RoleName { get; set; }
        [JsonProperty("shopId")]
        public string ShopId  { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}