namespace FeedBack.Core.Models
{
    /// <summary>
    /// Question answer
    /// </summary>
    public class QuestionAnswer: Question
    {
        /// <summary>
        /// String representation of the answer
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// String representation of the addition question
        /// </summary>
        public string AdditionalAnswer { get; set; }
    }
}