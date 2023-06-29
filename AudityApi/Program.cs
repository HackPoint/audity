using System.Text.Json;
using Application;
using Infrastructure;

const string corsApplication = "corsApplication";
var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();


// Add services to the container.
builder.Services
    .AddInfrastructureServices(configuration)
    .AddApplicationServices()
    .AddEndpointsApiExplorer();

builder.Services.AddLogging(cfg =>
{
    cfg.AddJsonConsole(opts =>
    {
        opts.IncludeScopes = true;
        opts.JsonWriterOptions = new JsonWriterOptions {
            Indented = true
        };
    });
});
builder.Services.AddCors(p =>
    p.AddPolicy(corsApplication, bldr => { bldr.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
else {
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseCors(corsApplication);
app.MapControllers();

app.Run();