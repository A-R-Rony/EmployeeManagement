using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

//add services
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
/*
 sqlserver connection
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("EmployeeDBConnection")));
*/ 

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("EmployeeDBConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)) // Specify the MySQL version (adjust it accordingly)
    ));
 


var app = builder.Build();

//app.MapGet("/", () => "Hello world");
app.UseStaticFiles();

app.UseRouting(); // Enables routing system
app.UseAuthorization(); // Enables authorization middleware (if needed)
//app.UseStatusCodePages();
// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();