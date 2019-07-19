using BMS.PublicTransport.Domain.Contract.Types;

namespace BMS.PublicTransport.Domain.Contract.Models
{
    public interface IVasttrafikResponse
    {
        ResponseType ResponseType { get; }
    }
}
