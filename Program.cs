using eLearning_System.DTO;
using eLearning_System.Interfaces;
using eLearning_System.Services;
using eLearning_System.Settings;
var builder = WebApplication.CreateBuilder(args);
var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
(
    mongoDbSettings.ConnectionString, mongoDbSettings.Name
);
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddCors();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(policy => {
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:65121");
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
