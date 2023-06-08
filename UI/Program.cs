
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfiguration config = configurationBuilder.Build();
builder.Services.AddTestBl(config);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile($"{Directory.GetCurrentDirectory()}\\Loggers\\Logger.txt");

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

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
