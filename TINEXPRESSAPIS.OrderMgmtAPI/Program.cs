using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MySqlConnector;

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
    builder.Services.AddDbContext<UserMgmtDbContext>
    (options => options.UseMySql(
        builder.Configuration.GetConnectionString("DevConnectionUserMgt"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DevConnectionUserMgt"))
        )
    );
    //builder.Services.Configure<DbResource>(builder.Configuration.GetSection("DevConnectionOrder"));
    //builder.Services.Configure<DbResource>(builder.Configuration.GetSection("DevConnectionOrder"));

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IAgingReportService, AgingReportService>();
    builder.Services.AddScoped<IAgingReportRepository, AgingReportRepository>();

    builder.Services.AddScoped<IDashboardService, DashboardService>();
    builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

    builder.Services.AddScoped<IExportRepository, ExportRepository>();

    builder.Services.AddScoped<IShipmentService, ShipmentService>();
    builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

    builder.Services.AddScoped<IPackageTypeService, PackageTypeService>();
    builder.Services.AddScoped<IPackageTypeRepository, PackageTypeRepository>();

    builder.Services.AddScoped<IPackageContentService, PackageContentService>();
    builder.Services.AddScoped<IPackageContentRepository, PackageContentRepository>();

    builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
    builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

    builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
    builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

    builder.Services.AddScoped<IGetQuoteService, GetQuoteService>();
    builder.Services.AddScoped<IGetQuoteRepository, GetQuoteRepository>();

    builder.Services.AddScoped<ICourierBookingService, CourierBookingService>();
    builder.Services.AddScoped<ICourierBookingRepository, CourierBookingRepository>();

    builder.Services.AddScoped<ICustomerBudgetService, CustomerBudgetService>();
    builder.Services.AddScoped<ICustomerBudgetRepository, CustomerBudgetRepository>();

    builder.Services.AddScoped<ICustomerPriorityService, CustomerPriorityService>();
    builder.Services.AddScoped<ICustomerPriorityRepository, CustomerPriorityRepository>();

    builder.Services.AddScoped<ICourierStatusesService, CourierStatusesService>();
    builder.Services.AddScoped<ICourierStatusesRepository, CourierStatusesRepository>();

    builder.Services.AddScoped<IOrderStatusesService, OrderStatusesService>();
    builder.Services.AddScoped<IOrderStatusesRepository, OrderStatusesRepository>();

    builder.Services.AddScoped<ICourierStatusMappingService, CourierStatusMappingService>();
    builder.Services.AddScoped<ICourierStatusMappingRepository, CourierStatusMappingRepository>();

    builder.Services.AddScoped<IUserLoginsRepository, UserLoginsRepository>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddScoped<IFavAddressesService, FavAddressesService>();
    builder.Services.AddScoped<IFavAddressesRepository, FavAddressesRepository>();

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
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Service API", Version = "v1" });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service v1"));
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/orderservice/swagger/v1/swagger.json", "Order Service API v1"));
    }


    app.UseHttpsRedirection();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

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