using System;
using BMS.PublicTransport.Domain.Contract.Models;
using System.Threading.Tasks;

namespace BMS.PublicTransport.Domain.Contract
{
    public interface IVasttrafikService
    {
        Task<VasttrafikResponse> Token();

        Task<VasttrafikResponse> DepartureBoard(string accessToken, string busStopId, DateTime departure);
    }
}
