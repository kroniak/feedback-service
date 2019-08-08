using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Base class with identity
    /// </summary>
    public abstract class IdentityModel
    {
        /// <summary>
        /// Public ObjectId to storing in MongoDB
        /// </summary>
        public ObjectId Id { get; set; }
    }
}