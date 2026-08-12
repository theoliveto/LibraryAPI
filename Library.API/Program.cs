using FluentMigrator.Runner;
using Library.Domain.Interfaces;
using Library.Domain.Services;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A ConnectionString 'DefaultConnection' não foi encontrada.");

builder.Services.AddSingleton(new DatabaseConnection(connectionString));

// Repositories
builder.Services.AddScoped<IPessoasRepository, PessoasRepository>();

// Services
builder.Services.AddScoped<IPessoasService, PessoasService>();

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddSqlServer()
        .WithGlobalConnectionString(connectionString)
        .ScanIn(
            Assembly.Load("Library.Infrastructure")
        )
        .For.Migrations()
    );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();