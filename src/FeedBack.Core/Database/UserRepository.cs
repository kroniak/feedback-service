using System;
using System.Diagnostics.CodeAnalysis;
using FeedBack.Core.Database.Interfaces;
using FeedBack.Core.Models;
using MongoDB.Driver;

namespace FeedBack.Core.Database
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _credentials;

        /// <inheritdoc />
        public UserRepository(IMongoDatabase database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            _credentials = database.GetCollection<User>(DbConstants.UserCollectionName);
        }

        /// <inheritdoc />
        public User GetSecureUser(string userName)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserName, userName);
            return _credentials.Find(filter).FirstOrDefault();
        }
    }
}