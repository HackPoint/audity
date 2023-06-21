using System.Text.Json;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddEndpointsApiExplorer();

builder.Services.AddLogging(cfg => {
    cfg.AddJsonConsole(opts => {
        opts.IncludeScopes = true;
        opts.JsonWriterOptions = new JsonWriterOptions {
            Indented = true
        };
    });
});

builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();