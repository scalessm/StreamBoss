namespace StreamBoss.Shared.Models
{
    public class RequestError
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
    }
}
