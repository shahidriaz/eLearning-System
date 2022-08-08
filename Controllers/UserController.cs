using eLearning.Models;
using eLearning_System.DTO;
using eLearning_System.Interfaces;
using eLearning_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// private instance of User,SignIn Manager and ITokenService
        /// </summary>
        private UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ITokenService tokenService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this._signInManager = signInManager;
            this.tokenService = tokenService;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User to Create</param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser() { 
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.Email
                };
                //Create User
                IdentityResult identityResult = await userManager.CreateAsync(appUser, user.Password);
                //If user is created
                if (identityResult.Succeeded)
                {
                    //Add the selected role for the created user
                    identityResult = await userManager.AddToRoleAsync(appUser, user.SelectedRole);
                    if (identityResult.Succeeded)
                    {
                        //TODO:Shahid - Modify it
                        //if succeeded, return the created user in json
                        return Json(appUser);
                    }
                    else
                    {
                        //TODO:Shahid
                        // Role is not assigned to User, so delete the user
                    }
                    
                }
                else
                {
                    return BadRequest(identityResult.Errors);
                }
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Retrive user against the Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return Json(user);
            else
                return BadRequest(new Exception("Email was not found"));
        }
        /// <summary>
        /// Used to Login and return the JWT Token
        /// </summary>
        /// <param name="signIn"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserWithKey>> Login([FromBody] SignIn signIn)
        {
            string token = string.Empty;
            ApplicationUser appUser = new ApplicationUser() { 
                Email = signIn.Login,
                UserName = signIn.Login
            };
            // Sign in the user
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser.UserName, signIn.Password, false, false);
            //Is used provided correct information
            if (result.Succeeded)
            {
                // Get the complete User's object
                ApplicationUser currentUser = await userManager.FindByNameAsync(appUser.UserName);
                if (currentUser != null)
                {
                    // If user is retrieved
                    var assignedRole = await userManager.GetRolesAsync(currentUser);
                    // if Role is not NULL
                    if (assignedRole != null)
                    {
                        // Create Token
                        token = tokenService.CreateToken(new User()
                        {
                            Email = appUser.Email,
                            SelectedRole = assignedRole.FirstOrDefault()??"",
                        });
                        //return the data
                        return new UserWithKey()
                        {
                            Token = token,
                            UserName = appUser.UserName,
                            Roles = assignedRole.FirstOrDefault() ?? ""
                        };
                    }
                }
                else
                {
                    // Logically flow will not come here
                }   
            }
            else
            {
                return BadRequest("Unable to Login using provided credentials");
            }
            return BadRequest("Unable to Login using provided credentials");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
