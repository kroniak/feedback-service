using FeedBack.Core.Models;
using MongoDB.Driver;

namespace FeedBack.Core.Database.Repositories
{
    public abstract class MongoRepository<T> where T : IdentityModel
    {
        protected IMongoCollection<T> _collection;
    }
}