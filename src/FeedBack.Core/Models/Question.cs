using System.Collections.Generic;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Question model
    /// </summary>
    public class Question : IdentityModel
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Question type
        /// </summary>
        public QType Type { get; set; }

        /// <summary>
        /// Dictionary of the question possible answers
        /// </summary>
        public Dictionary<int, string> AnswerVariant { get; set; }

        /// <summary>
        /// Flag is required question
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Flag is has string addition field
        /// </summary>
        public bool HasStringAddition { get; set; }
    }
}