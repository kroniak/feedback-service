using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FeedBack.Core.Database.Interfaces;
using FeedBack.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace FeedBack.Core.Database.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        private readonly IMemoryCache _cache;

        /// <inheritdoc />
        public UserRepository(
            IMongoDatabase database,
            IMemoryCache memoryCache)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            Collection = database.GetCollection<User>(DbConstants.UserCollectionName);
        }

        /// <inheritdoc />
        public async Task<User> GetSecureUserAsync(string userName) =>
            await _cache.GetOrCreateAsync(
                new {key = "User", value = userName},
                async cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(60);
                    var filter = Builders<User>.Filter.Eq(u => u.UserName, userName);
                    var users = await Collection.Find(filter).ToListAsync();
                    return users.FirstOrDefault();
                }
            );
    }
}