using Implementation.Abstracts.Lifestyles;
using Implementation.Concretes.Models.Auth;
using System.Collections.Generic;

namespace Implementation.Abstracts.Auth.Data
{
    public interface IAuthDataService : ISingletonService
    {
        void InsertDummyUserList(List<UserModel> userList);
        List<UserModel> GetDummyUserList();
    }
}