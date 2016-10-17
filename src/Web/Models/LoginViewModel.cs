using Nancy;

namespace Web.Models
{
    public class LoginViewModel
    {
        public bool Error { get; set; }
        public Url ReturnUrl { get; set; }
    }
}