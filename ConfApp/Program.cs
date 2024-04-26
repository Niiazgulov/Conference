using ConfApp.Context;
using ConfApp.Migrations;
using ConfApp.Extensions;
using FluentMigrator.Runner;
using System.Reflection;
using DataAccess;
using Application.Validators;
using Application.Handlers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReaders();
builder.Services.AddHandlers();
builder.Services.AddValidators();
builder.Services.AddControllers();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();
builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
        .AddFluentMigratorCore()
        .ConfigureRunner(c => c.AddPostgres92()
            .WithGlobalConnectionString(rb => rb.GetService<IConfiguration>()?.GetConnectionString("NpgConnection"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();
