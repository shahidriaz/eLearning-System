using eLearning_System.Interfaces;
using eLearning_System.Models.Admin;
using eLearning_System.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eLearning_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categorySvc;
        public AdminController(ICategoryService category)
        {
            _categorySvc = category;
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public Category Create([FromBody] Category category)
        {
            
            if (ModelState.IsValid)
            {
                _categorySvc.InsertDocument(category);
            }
            return category;
        }
    }
}
