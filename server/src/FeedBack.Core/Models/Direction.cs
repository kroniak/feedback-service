using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Some direction Poll to Employer. Represent many to many link.
    /// </summary>
    public class Direction : IdentityModel
    {
        /// <summary>
        /// Show opt not notification to user
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Link to Poll
        /// </summary>
        public ObjectId Poll { get; set; }

        /// <summary>
        /// Link to Employer
        /// </summary>
        public ObjectId DirectedTo { get; set; }
    }
}