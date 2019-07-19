using BMS.PublicTransport.Domain.Contract.Types;
using Newtonsoft.Json;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikTokenResponse : IVasttrafikResponse
    {
        public ResponseType ResponseType => ResponseType.Token;

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
