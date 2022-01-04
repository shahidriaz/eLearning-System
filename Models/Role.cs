using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Models
{
    /// <summary>
    /// Role DTO
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Name of the Role
        /// </summary>
        [Required]
        public string RoleName { get; set; }
    }
}
