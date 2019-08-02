using System.Collections.Generic;
using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Answer to Poll model 
    /// </summary>
    public class PollAnswer: IdentityModel
    {
        /// <summary>
        /// Mark of the visible of the poll
        /// </summary>
        public bool IsVisibleToPollSubject { get; set; }
        
        /// <summary>
        /// Link to Employer
        /// </summary>
        public ObjectId Answerer { get; set; }
        
        /// <summary>
        /// Link to Employer
        /// </summary>
        public ObjectId PollSubject { get; set; }
        
        /// <summary>
        /// Link to Poll
        /// </summary>
        public ObjectId Poll { get; set; }
        
        /// <summary>
        /// List of the QuestionAnswers
        /// </summary>
        public List<QuestionAnswer> Answers { get; set; }
    }
}