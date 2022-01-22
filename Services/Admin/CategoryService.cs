using eLearning_System.Interfaces;
using eLearning_System.Models.Admin;
using MongoDB.Driver;

namespace eLearning_System.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _category;
        public CategoryService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Name);
            _category = database.GetCollection<Category>("Categories");
        }
        public Category InsertDocument(Category category)
        {
            _category.InsertOne(category);
            return category;
        }
    }
    public interface ICategoryService
    {
        public Category InsertDocument(Category category);
    }
}
