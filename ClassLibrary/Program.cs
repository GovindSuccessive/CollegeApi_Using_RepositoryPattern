using ClassLibrary.Context;
using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddDbContext<CollegeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/
builder.Services.AddDbContext<CollegeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEntityBaseRepository<Student>, EntityBaseRepository<Student>>();
builder.Services.AddScoped<IEntityBaseRepository<Course>, EntityBaseRepository<Course>>();
var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.Run();
