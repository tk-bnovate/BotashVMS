global using BotashVMS.Models;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
using BotashVMS.Models.Validators;
//using FluentValidation;
//using FluentValidation.AspNetCore;
//using FluentValidation.Internal;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using FluentValidation;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation(conf =>
{
    conf.ValidationStrategy = SharpGrip.FluentValidation.AutoValidation.Mvc.Enums.ValidationStrategy.Annotations;
});
builder.Services.AddScoped<IValidator<Vendor>, VendorValidator>();


// Add services to the container.
//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BotashPortalContext>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseSwagger(opt =>
{
    opt.RouteTemplate = "openapi/{documentName}.json";
});
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar Example";
    opt.Theme = ScalarTheme.Mars;
    opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
