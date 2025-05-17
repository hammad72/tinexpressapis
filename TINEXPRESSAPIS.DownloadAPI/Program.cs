using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

//using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
try
{
    builder.Services.AddDbContext<OrderDbContext>

    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DevConnectionOrder"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DevConnectionOrder"))
        )
    );

    builder.Services.AddScoped<IDownloadLabelService, DownloadLabelService>();
    builder.Services.AddScoped<IDownloadLabelRepository, DownloadLabelRepository>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddControllers();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              //policy.WithOrigins("http://example.com", "http://www.contoso.com");
                              policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Download Service API", Version = "v1" });
    });

    builder.Services.AddAuthorization();

    QuestPDF.Settings.License = LicenseType.Community;

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Download Service v1"));
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/downloadservice/swagger/v1/swagger.json", "Download Service API v1"));
    }


    app.UseHttpsRedirection();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "public")),
        RequestPath = "/public"
    });

    app.Run();
}
catch (MySqlException ex)
{
    // Handle MySQL-specific exceptions
    Console.WriteLine($"MySQL error: {ex.Message}");
}
catch (AggregateException ex)
{
    foreach (var innerException in ex.InnerExceptions)
    {
        Console.WriteLine($"Inner exception: {innerException.Message}");
    }
}
catch (Exception ex)
{
    // Handle general exceptions
    Console.WriteLine($"Error: {ex.Message}");
}
