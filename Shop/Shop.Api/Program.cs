using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNet;
using Common.AspNet.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure;
using Shop.Api.Infrastructure.jwt;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = (context =>
        {
            var result = new ApiResult()
            {
                IsSuccess = false,
                MetaData = new()
                {
                    StatusCode = AppStatusCode.BadRequest,
                    Message = context.ModelState.GetModelStateError()
                }
            };
            return new BadRequestObjectResult(result);
        });
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.RegisterShopDependency(connectionString);
CommonBootstrapper.Init(builder.Services);
builder.Services.RegisterApiDependency();
builder.Services.AddTransient<IFileService, FileService>();

//Jwt
builder.Services.AddJwtConfig(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseApiCustomExceptionHandler();
app.Run();
