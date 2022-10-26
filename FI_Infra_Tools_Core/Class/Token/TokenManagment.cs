using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FI_Infra_Tools_Core
{
    public class LoginRequest
    {
       
        [Required]
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class LoginResult
    {
        
        public string UserName { get; set; }
        public string JwtToken { get; set; }
    }

    public class TokenManagement
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonPropertyName("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
