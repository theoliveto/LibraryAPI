using System.Text;
using System.Reflection;
using Microsoft.OpenApi;
using FluentMigrator.Runner;
using Library.Domain.Services;
using Library.Domain.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using Library.Infrastructure.Services;
using Library.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("A chave JWT não foi configurada.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("O Issuer JWT não foi configurado.");

var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("O Audience JWT não foi configurado.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.FullName);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe apenas o token JWT."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
        }
    );
});

var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("A ConnectionString 'DefaultConnection' não foi encontrada.");

builder.Services.AddSingleton(new DatabaseConnection(connectionString));

builder.Services.AddScoped<IPessoasRepository, PessoasRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IRecuperacaoSenhaRepository, RecuperacaoSenhaRepository>();
builder.Services.AddScoped<IAutorRepository, AutoresRepository>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();

builder.Services.AddScoped<IPessoasService, PessoasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRecuperacaoSenhaService, RecuperacaoSenhaService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAutorService, AutoresService>();
builder.Services.AddScoped<ICategoriasService, CategoriasService>();

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

using (var scope = app.Services.CreateScope()) {
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();