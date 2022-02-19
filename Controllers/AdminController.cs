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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost] 
        public Category Create([FromBody] Category category)
        {
            
            if (ModelState.IsValid)
            {
                _categorySvc.InsertDocument(category);
            }
            return category;
        }
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public List<Category> LoadCategories()
        {
            return _categorySvc.LoadCategories();
        }
        [HttpDelete]
        public bool DeleteCategory(string id)
        {
            return _categorySvc.DeleteCategory(id);
        }
        [HttpDelete]
        public bool UpdateCategory([FromBody] Category category,string id)
        {
            return _categorySvc.DeleteCategory(id);
        }
    }
}
