using System;

namespace Implementation.Abstracts.Auth
{
    public interface IAuthService
    {
        Guid? ValidateUser(string username, string password);
    }
}