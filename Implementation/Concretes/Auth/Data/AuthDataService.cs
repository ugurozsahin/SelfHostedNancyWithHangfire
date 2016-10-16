using Implementation.Abstracts.Auth.Data;
using Implementation.Abstracts.Common;
using Implementation.Abstracts.Configuration;
using Implementation.Concretes.Models.Auth;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Implementation.Concretes.Auth.Data
{
    public class AuthDataService : IAuthDataService
    {
        private readonly IConfigWrapper _configWrapper;
        private readonly ISerializer _serializer;
        private readonly IRedisClient _redisClient;

        public AuthDataService(
            IConfigWrapper configWrapper,
            ISerializer serializer,
            IRedisClient redisClient)
        {
            _configWrapper = configWrapper;
            _serializer = serializer;
            _redisClient = redisClient;
            Initialize();
        }

        private void Initialize()
        {
            var dummyUserCollection = GetDummyUserList();
            if (dummyUserCollection == null || !dummyUserCollection.Any())
            {
                dummyUserCollection = new List<UserModel>
                {
                    new UserModel()
                    {
                        Username = "admin",
                        Password = "password"
                    },
                    new UserModel()
                    {
                        Username = "user",
                        Password = "password"
                    }
                };
                InsertDummyUserList(dummyUserCollection);
            }
        }

        public void InsertDummyUserList(List<UserModel> userList)
        {
            using (var redisConnection = _redisClient.OpenRedisConnection())
            {
                var db = redisConnection.GetDatabase(_configWrapper.AuthRedisDb);
                db.StringSet(_configWrapper.RedisKeyUserList, _serializer.Serialize(userList), TimeSpan.FromDays(1), When.NotExists, CommandFlags.HighPriority);
            }
        }

        public List<UserModel> GetDummyUserList()
        {
            using (var redisConnection = _redisClient.OpenRedisConnection())
            {
                var db = redisConnection.GetDatabase(_configWrapper.AuthRedisDb);
                var serializedUserList = db.StringGet(_configWrapper.RedisKeyUserList, CommandFlags.HighPriority);
                if (!string.IsNullOrWhiteSpace(serializedUserList))
                {
                    var userList = _serializer.Deserialize<List<UserModel>>(serializedUserList);
                    return userList;
                }
            }
            return null;
        }

    }
}
