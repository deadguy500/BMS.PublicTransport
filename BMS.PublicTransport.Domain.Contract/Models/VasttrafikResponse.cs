namespace BMS.PublicTransport.Domain.Contract.Models
{
    public class VasttrafikResponse
    {
        public IVasttrafikResponse Data { get; set; }

        public string AccessToken { get; set; }

        public string Message { get; set; }
        
        public bool Success { get; set; }

        public int Code { get; set; }
    }
}
