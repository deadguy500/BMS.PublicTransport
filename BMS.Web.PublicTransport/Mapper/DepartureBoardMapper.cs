using BMS.Web.PublicTransport.Models;
using BMS.PublicTransport.Domain.Contract.Models;
using System.Collections.Generic;
using System.Linq;

namespace BMS.Web.PublicTransport.Mapper
{
    public static class DepartureBoardMapper
    {
        public static DepartureBoardApiModel ToApiModel(this VasttrafikDepartureBoardResponse model, string accessToken, int? limit)
        {
            return new DepartureBoardApiModel()
            {
                AccessToken = accessToken,
                ServerDate = model.ServerDate,
                ServerTime = model.ServerTime,
                Departures = limit.HasValue
                    ? model.DepartureList.ToApiModel().Take(limit.Value).ToList()
                    : model.DepartureList.ToApiModel()
            };
        }

        public static List<DepartureItemApiModel> ToApiModel(this List<VasttrafikDepartureResponse> list)
        {
            return list.ConvertAll(x => new DepartureItemApiModel()
            {
                Name = x.Name,
                Number = x.Number,
                Stop = x.Stop,
                Direction = x.Direction,
                Track = x.Track,
                PlanedTime = x.PlanedTime,
                Realtime = x.Realtime,
                Date = x.Date
            });
        }
    }
}
