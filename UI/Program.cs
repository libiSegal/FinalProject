using Bl.DataApi;
using Microsoft.Extensions.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfiguration config = configurationBuilder.Build();

builder.Services.AddTestBl(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
string? m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
builder.Logging.AddFile("C:\\Users\\צוקרמן אסתר\\source\\repos\\FinalProject\\Logger.txt");

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddCors(option =>
   {
       var frontend_url = configuration.GetValue<string>("frontend_url");
       option.AddDefaultPolicy(builder =>
       {
           builder.WithOrigins(frontend_url)
           .AllowAnyMethod().AllowAnyHeader();
       });

   });
   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
