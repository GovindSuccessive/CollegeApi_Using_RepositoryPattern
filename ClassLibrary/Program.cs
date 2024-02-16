using ClassLibrary.Context;
using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddDbContext<CollegeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/
builder.Services.AddDbContext<CollegeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
