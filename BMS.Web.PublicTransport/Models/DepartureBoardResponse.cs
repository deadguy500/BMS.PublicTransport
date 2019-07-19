namespace BMS.Web.PublicTransport.Models
{
    public class DepartureBoardResponse<T> : DepartureBoardResponse
    {
        public T Data { get; set; }
    }

    public class DepartureBoardResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
