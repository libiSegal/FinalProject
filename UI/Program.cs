using BL;
using Dal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTestBl();
builder.Services.Configure<LaundrySystemDatabaseSettings>(builder.Configuration.GetSection("LaundrySystemDatabase"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 


// Add services to the container.
/*builder.Services.Configure<LaundrySystemDatabaseSettings>(
builder.Configuration.GetSection("LaundrySystemDatabase"));
builder.Services.AddSingleton<UserService>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
