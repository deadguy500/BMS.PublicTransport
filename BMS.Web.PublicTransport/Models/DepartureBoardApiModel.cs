using System.Collections.Generic;

namespace BMS.Web.PublicTransport.Models
{
    public class DepartureBoardApiModel
    {
        public string AccessToken { get; set; }

        public string ServerTime { get; set; }

        public string ServerDate { get; set; }

        public List<DepartureItemApiModel> Departures { get; set; }
    }
}
