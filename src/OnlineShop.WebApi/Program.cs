using OnlineShop.Application;
using OnlineShop.Infrastructure;
using OnlineShop.Infrastructure.Persistance.MongoDb.Seed;
using OnlineShop.WebApi;
using OnlineShop.WebApi.Middlewares;
using ConfigureWebApiServices = OnlineShop.WebApi.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMongoDbInitializer();
}

app.UseHttpsRedirection();

app.UseCors(ConfigureWebApiServices.CorsPolicy);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

