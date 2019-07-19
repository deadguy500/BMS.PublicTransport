using BMS.PublicTransport.Domain.Contract.Types;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikErrorResponse : IVasttrafikResponse
    {
        public ResponseType ResponseType => ResponseType.Error;
    }
}
