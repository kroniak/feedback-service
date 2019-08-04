using System.Collections.Generic;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Poll template model
    /// </summary>
    public class PollTemplate : IdentityModel
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
        /// List of the question
        /// </summary>
        public List<Question> TemplateQuestions { get; set; }
    }
}