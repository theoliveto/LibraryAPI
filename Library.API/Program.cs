using System.Reflection;
using FluentMigrator.Runner;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Services;
using Library.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.FullName);
});

var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A ConnectionString 'DefaultConnection' não foi encontrada.");

builder.Services.AddSingleton(new DatabaseConnection(connectionString));

// Repositories
builder.Services.AddScoped<IPessoasRepository, PessoasRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();

// Services
builder.Services.AddScoped<IPessoasService, PessoasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

builder.Services.AddCors(options => {
    options.AddPolicy("Frontend", policy => {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

using (var scope = app.Services.CreateScope()) {
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();