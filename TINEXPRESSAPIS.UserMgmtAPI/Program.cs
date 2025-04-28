using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddDbContext<UserMgmtDbContext>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DevConnectionUserMgt"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DevConnectionUserMgt"))
        )
    );

builder.Services.AddScoped<IUserLoginsService, UserLoginsService>();
builder.Services.AddScoped<IUserLoginsRepository, UserLoginsRepository>();

builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

builder.Services.AddScoped<ICustomerProfileService, CustomerProfileService>();
builder.Services.AddScoped<ICustomerProfileRepository, CustomerProfileRepository>();

builder.Services.AddScoped<ICustomerUserProfileService, CustomerUserProfileService>();
builder.Services.AddScoped<ICustomerUserProfileRepository, CustomerUserProfileRepository>();

builder.Services.AddScoped<ICouriersService, CouriersService>();
builder.Services.AddScoped<ICouriersRepository, CouriersRepository>();

builder.Services.AddScoped<ICourierUserProfileService, CourierUserProfileService>();
builder.Services.AddScoped<ICourierUserProfileRepository, CourierUserProfileRepository>();

builder.Services.AddScoped<IUserTypesService, UserTypesService>();
builder.Services.AddScoped<IUserTypesRepository, UserTypesRepository>();

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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserMgmt Service v1"));
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/usermgmtservice/swagger/v1/swagger.json", "UserMgmt Service API v1"));
}


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();