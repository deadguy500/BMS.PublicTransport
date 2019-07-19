using Newtonsoft.Json;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikDepartureResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sname")]
        public string Number { get; set; }

        [JsonProperty("stop")]
        public string Stop { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("track")]
        public string Track { get; set; }

        [JsonProperty("time")]
        public string PlanedTime { get; set; }

        [JsonProperty("rtTime")]
        public string Realtime { get; set; }

        [JsonProperty("rtDate")]
        public string Date { get; set; }
    }
}
