using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Poll in action model
    /// </summary>
    public class Poll : IdentityModel
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Flag os a Poll is private
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Flag is a Poll is candidate poll
        /// </summary>
        public bool IsCandidatePoll { get; set; }

        /// <summary>
        /// For future. Type of the poll
        /// </summary>
        public string PollType { get; set; }
        
        /// <summary>
        /// Start Poll Date
        /// </summary>
        public DateTime StartDate { get; set; } = DateTime.Now;

        /// <summary>
        /// End Poll Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// List of the Question
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Link to Employer model
        /// </summary>
        public ObjectId Owner { get; set; }
    }
}