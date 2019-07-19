using System.Collections.Generic;
using BMS.PublicTransport.Domain.Contract.Types;
using Newtonsoft.Json;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikDepartureBoardResponse : IVasttrafikResponse
    {
        public ResponseType ResponseType => ResponseType.DepartureBoard;

        [JsonProperty("noNamespaceSchemaLocation")]
        public string NoNamespaceSchemaLocation { get; set; }

        [JsonProperty("servertime")]
        public string ServerTime { get; set; }

        [JsonProperty("serverdate")]
        public string ServerDate { get; set; }

        [JsonProperty("Departure")]
        public List<VasttrafikDepartureResponse> DepartureList { get; set; }
    }
}
