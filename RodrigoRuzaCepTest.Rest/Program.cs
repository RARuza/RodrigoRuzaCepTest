using Microsoft.EntityFrameworkCore;
using RodrigoRuzaCepTest.Domain.Repositories;
using RodrigoRuzaCepTest.Domain.Service.Interface;
using RodrigoRuzaCepTest.Infraestructure.Context;
using RodrigoRuzaCepTest.Infraestructure.Repositories;
using RodrigoRuzaCepTest.Shared.HttpHandler.Implementation;
using RodrigoRuzaCepTest.Shared.HttpHandler.Interface;
using RodrigoRuzaCepTest.Shared.Mapping;
using RodrigoRuzaCepTest.Shared.Service;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=85.31.63.18;Port=3306;Database=LocalDB;Uid=mysql;Pwd=Senha#2024!;";

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseMySql(connectionString,
     ServerVersion.AutoDetect(connectionString)
    ));


using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    context.Database.EnsureCreated();
}

builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddScoped<ICepRepository, CepRepository>();
builder.Services.AddScoped<IHttpHandler, HttpHandler>();

builder.Services.AddAutoMapper(typeof(CepProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();