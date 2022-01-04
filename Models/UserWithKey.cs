using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Models
{
    /// <summary>
    /// This is a simple class holding the User Name and JWT Token 
    /// </summary>
    public class UserWithKey
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// JWT Token
        /// </summary>
        public string Token { get; set; }
    }
}
