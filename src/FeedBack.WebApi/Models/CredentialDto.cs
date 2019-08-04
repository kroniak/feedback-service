using System.ComponentModel.DataAnnotations;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace FeedBack.WebApi.Models
{
    /// <summary>
    /// User dto for Login procedure
    /// </summary>
    public class CredentialDto
    {
        /// <summary>
        /// Email UserName for login
        /// </summary>
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        /// <summary>
        /// Not hashed password for login
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}