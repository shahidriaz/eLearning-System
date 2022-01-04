using System.ComponentModel.DataAnnotations;

namespace eLearning_System.Models
{
    /// <summary>
    /// This is a DTO for User which will be bing in the html template
    /// </summary>
    public class User
    {
        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Middle Name
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Selected Role
        /// </summary>
        [Required]
        public string SelectedRole { get; set; }
    }
}
