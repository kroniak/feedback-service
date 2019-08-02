using System;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Candidate model
    /// </summary>
    public class Candidate : IdentityModel
    {
        /// <summary>
        /// Firstname + LastName + MiddleName 
        /// </summary>
        public string Fio { get; set; }

        /// <summary>
        /// Candidate position
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// Interview Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Url link to CV 
        /// </summary>
        public string CvLink { get; set; }
    }
}