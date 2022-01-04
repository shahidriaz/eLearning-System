using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Models
{
    /// <summary>
    /// This class will be used by the SignIn Manager to Login
    /// </summary>
    public class SignIn
    {
        /// <summary>
        /// Login i.e.UserName which in this case is Email
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
