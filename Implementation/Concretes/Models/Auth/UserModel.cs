using System;

namespace Implementation.Concretes.Models.Auth
{
    public class UserModel
    {
        public UserModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}