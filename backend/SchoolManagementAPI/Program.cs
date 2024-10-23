
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Repository.Interface;
using SchoolManagement.Repository;
using SchoolManagement.BusinessRules.Interface;
using SchoolManagement.BusinessRules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()  
                   .AllowAnyMethod() 
                   .AllowAnyHeader(); 
        });
});

builder.Services.AddScoped<IStudentsService, StudentService>();

builder.Services.AddScoped<IStudentRules, StudentRules>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
