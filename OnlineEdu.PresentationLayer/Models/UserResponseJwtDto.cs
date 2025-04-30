namespace OnlineEdu.PresentationLayer.Models
{
    public class UserResponseJwtDto
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public DateTime expireDate { get; set; }
    }
}
