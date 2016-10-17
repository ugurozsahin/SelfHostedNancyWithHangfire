using Nancy.Security;
using System.Collections.Generic;

namespace Implementation.Concretes.Models.Auth
{
    public class CustomUserIdentity : IUserIdentity
    {
        public IEnumerable<string> Claims { get; set; }

        public string UserName { get; set; }
    }
}
