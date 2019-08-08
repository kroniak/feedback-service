using System.Collections.Generic;
using MongoDB.Bson;

namespace FeedBack.Core.Models
{
    /// <summary>
    /// Main Employer Model
    /// </summary>
    public class Employer : IdentityModel
    {
        /// <summary>
        /// FirstName + MiddleName + Surname
        /// </summary>
        public string Fio { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// TeamRoles list
        /// </summary>
        public TeamRole[] TeamRoles { get; set; }

        /// <summary>
        /// First priority list of the teams
        /// </summary>
        public List<string> TeamsPriorityFirst { get; set; }

        /// <summary>
        /// Second priority list of the teams
        /// </summary>
        public List<string> TeamsPrioritySecond { get; set; }

        /// <summary>
        /// Link to User model
        /// </summary>
        public ObjectId? User { get; set; }

        /// <summary>
        /// Link to Candidate model
        /// </summary>
        public ObjectId? Candidate { get; set; }
    }
}