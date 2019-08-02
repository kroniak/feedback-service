using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Poll in action model
    /// </summary>
    public class Poll : PollTemplate
    {
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
        public new List<Question> Questions { get; set; }
        
        /// <summary>
        /// Link to Employer model
        /// </summary>
        public ObjectId Owner { get; set; }
    }
}