using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Main user model, has link to Employer model
    /// </summary>
    public class User : IdentityModel
    {
        /// <summary>
        ///  User email
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Users hashed password 
        /// </summary>
        public string HashedPassword { get; set; }

        /// <summary>
        /// Array of the user roles
        /// </summary>
        public Role[] Roles { get; set; }

        /// <summary>
        /// Link to Employer model
        /// </summary>
        public ObjectId? Employer { get; set; }
    }
}