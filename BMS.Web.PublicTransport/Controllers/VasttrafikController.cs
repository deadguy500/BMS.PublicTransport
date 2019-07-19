using BMS.PublicTransport.Domain.Contract;
using BMS.PublicTransport.Domain.Contract.Models;
using BMS.Web.PublicTransport.Mapper;
using BMS.Web.PublicTransport.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMS.Web.PublicTransport.Controllers
{
    [Route("api/vasttrafik")]
    [ApiController]
    public class VasttrafikController : ControllerBase
    {
        private readonly IVasttrafikService _vasttrafikService;

        public VasttrafikController(IVasttrafikService vasttrafikService)
        {
            _vasttrafikService = vasttrafikService;
        }

        [HttpGet("{accesstoken}/{busstopname}/{limit}")]

        public async Task<DepartureBoardResponse<DepartureBoardApiModel>> GetDepartures(string accessToken, string busStopName, int? limit)
        {
            var busStopTable = GetBusStopTable();

            if (!busStopTable.TryGetValue(busStopName, out string busStopId))
            {
                return new DepartureBoardResponse<DepartureBoardApiModel>()
                {
                    Success = false,
                    Message = $"Bus stop name ({busStopName}) not found"
                };
            }

            try
            {
                var departureBoardResponse = await _vasttrafikService.DepartureBoard(accessToken, busStopId, DateTime.Now.AddMinutes(-2));

                if (!departureBoardResponse.Success)
                {
                    var response = await _vasttrafikService.Token();

                    if (response.Success)
                    {
                        var tokenResponse = (VasttrafikTokenResponse)response.Data;
                        accessToken = tokenResponse.AccessToken;

                        departureBoardResponse = await _vasttrafikService.DepartureBoard(accessToken, busStopId, DateTime.Now.AddMinutes(-2));
                    }
                    else
                    {
                        return new DepartureBoardResponse<DepartureBoardApiModel>()
                        {
                            Success = false,
                            Message = response.Message
                        };
                    }
                }

                var data = (VasttrafikDepartureBoardResponse)departureBoardResponse.Data;

                var departures = data.ToApiModel(accessToken, limit);

                return new DepartureBoardResponse<DepartureBoardApiModel>()
                {
                    Data = departures,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new DepartureBoardResponse<DepartureBoardApiModel>()
                {
                    Success = false,
                    Message = $"Something wrong happened: {ex.Message}"
                };
            }
        }

        [HttpGet]
        [Route("busstops")]
        public DepartureBoardResponse<List<BusStopApiModel>> GetBusStops()
        {
            var table = GetBusStopTable();
            var list = new List<BusStopApiModel>();

            foreach (var key in table.Keys)
            {
                if (table.TryGetValue(key, out string name))
                {
                    list.Add(new BusStopApiModel()
                    {
                        Id = key,
                        Name = name
                    });
                }
            }

            return new DepartureBoardResponse<List<BusStopApiModel>>()
            {
                Data = list,
                Success = true
            };
        }

        private Dictionary<string, string> GetBusStopTable()
        {
            return new Dictionary<string, string>()
            {
                {"Eriksbergstorget, Göteborg", "9021014002240000"},
                {"Eriksbergs Färjeläge, Göteborg", "9021014002239000"},
                {"Lindholmspiren, Göteborg", "9021014004493000"},
            };
        }
    }
}