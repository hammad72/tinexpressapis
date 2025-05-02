using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using System.Text;
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddDbContext<NotifcationDBContext>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DevConnectionNotification"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DevConnectionNotification"))

        )
    );

builder.Services.AddDbContext<UserMgmtDbContext>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DevConnectionUserMgt"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DevConnectionUserMgt"))
        )
    );
builder.Services.AddMemoryCache();//For OTP storage
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

builder.Services.AddScoped<IOTPControlService, OTPControlService>();

builder.Services.AddScoped<ISendOTPService, SendOTPService>();
builder.Services.AddScoped<ISendOTPRepository, SendOTPRepository>();

builder.Services.AddScoped<IUserLoginsService, UserLoginsService>();
builder.Services.AddScoped<IUserLoginsRepository, UserLoginsRepository>();

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
//builder.Services.AddSwaggerGen(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserMgmt Service API", Version = "v1" });
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification Service v1"));
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/notificationservice/swagger/v1/swagger.json", "Notification Service API v1"));
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();