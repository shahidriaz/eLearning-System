using eLearning.Models;
using eLearning_System.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Controllers
{
    /// <summary>
    /// Role Controller for the Management of the Roles
    /// </summary>
    public class RoleController : Controller
    {
        /// <summary>
        /// private instance of the RoleManager
        /// </summary>
        private RoleManager<ApplicationRole> roleManager;
        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            // setting the role manager from the dependancy
            this.roleManager= roleManager;
        }
        /// <summary>
        /// Used to get all available roles from the Application
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRoles()
        {
            IEnumerable<ApplicationRole> appRoles = roleManager.Roles.AsEnumerable();
            if (appRoles != null)
                return Json(appRoles);
            else
                return BadRequest("Role was not found");
        }
        /// <summary>
        /// Get A Role by RoleName
        /// </summary>
        /// <param name="roleName">role name</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoleByRoleName(string roleName)
        {
            ApplicationRole appRole = await roleManager.FindByNameAsync(roleName);
            if (appRole != null)
                return Json(appRole);
            else
                return BadRequest("Role was not found");
        }
        /// <summary>
        /// POST: RoleController/Create
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole appRole = new ApplicationRole()
                {
                    Name = role.RoleName
                };
                IdentityResult identityResult = await roleManager.CreateAsync(appRole);
                if (identityResult.Succeeded)
                    return Json(appRole);
                else
                    return BadRequest(identityResult.Errors);
            }
            return BadRequest(ModelState);
        }
    }
}
