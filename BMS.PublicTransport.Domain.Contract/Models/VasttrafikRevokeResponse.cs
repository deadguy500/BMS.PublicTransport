using BMS.PublicTransport.Domain.Contract.Types;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikRevokeResponse : IVasttrafikResponse
    {
        public ResponseType ResponseType => ResponseType.Revoke;
    }
}
