using Implementation.Abstracts.Auth;
using Implementation.Abstracts.Auth.Data;
using Implementation.Concretes.Models.Auth;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Implementation.Concretes.Auth
{
    public class AuthService : IAuthService, IUserMapper
    {
        private readonly IAuthDataService _authDataService;
        public readonly List<UserModel> DummyUserCollection = new List<UserModel>();

        public AuthService()
        {

        }

        public AuthService(IAuthDataService authDataService)
        {
            _authDataService = authDataService;
        }

        public Guid? ValidateUser(string username, string password)
        {
            var dummyUserCollection = _authDataService.GetDummyUserList();
            var user = dummyUserCollection.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null) return null;

            return user.Id;
        }


        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var dummyUserCollection = _authDataService.GetDummyUserList();
            var user = dummyUserCollection.FirstOrDefault(u => u.Id == identifier);

            return user == null ? null : new CustomUserIdentity { UserName = user.Username };
        }
    }
}