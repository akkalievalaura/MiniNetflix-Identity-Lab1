using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MiniNetflix.Identity.Data;
using MiniNetflix.Identity.Mapping;
using MiniNetflix.Identity.Repositories.Impl;
using MiniNetflix.Identity.Repositories.Interfaces;
using MiniNetflix.Identity.Services.Impl;
using MiniNetflix.Identity.Services.Interfaces;
using MiniNetflix.Identity.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Контроллеры (API).
builder.Services.AddControllers();

// Swagger (документация API).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MiniNetflix Identity API",
        Version = "v1"
    });

    // Подключаем XML-комментарии (summary/param/returns) из кода.
    var xmlFileName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    // Кнопка Authorize и заголовок Authorization: Bearer {token}.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Вставь токен так: `Bearer {token}`"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// JWT-аутентификация.
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretKey = builder.Configuration["Jwt:SecretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new InvalidOperationException("Jwt:SecretKey is not configured.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

// База данных (PostgreSQL).
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI: сервисы и репозитории.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

// HTTP-клиенты для внешних сервисов.
builder.Services.AddHttpClient<ISubscriptionClient, SubscriptionClient>(client =>
{
    var baseUrl = builder.Configuration["Services:Subscription"];
    client.BaseAddress = new Uri(baseUrl ?? "http://localhost:5003");
});

// AutoMapper.
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Миграции и сидинг (роли).
DbInitializer.MigrateAndSeed(app.Services);

// HTTP-конвейер (middleware).
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniNetflix Identity API");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();