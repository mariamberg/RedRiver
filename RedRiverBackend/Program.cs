using RedRiverApp.Core.Domain.Users;
using RedRiverApp.Core.Domain.Users.Port;
using RedRiverApp.Core.Domain.Books;
using RedRiverApp.Core.Domain.Books.Port;
using RedRiverApp.Core.Domain.Quotes;
using RedRiverApp.Core.Domain.Quotes.Port;
using RedRiverApp.Core.Domain.LogIns;
using RedRiverApp.Infrastructure.Persistence.Users;
using RedRiverApp.Infrastructure.Persistence.Books;
using RedRiverApp.Infrastructure.Persistence.Quotes;
using RedRiverApp.WebApi.Converter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

AddScopedServices(builder);
AddLoggingConfig(builder);
AddCorsConfig(builder);
AddAuthenticationAndAuthorization(builder);

builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

AddBookTestData(app);
AddQuoteTestData(app);
AddTestUser(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Not required?
// app.UseHttpsRedirection();

app.Run();

static void AddScopedServices(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IBookRepository, BookInMemoryRepository>();
    builder.Services.AddScoped<BookService>();
    builder.Services.AddScoped<BookConverter>();
    builder.Services.AddSingleton<IQuoteRepository, QuoteInMemoryRepository>();
    builder.Services.AddScoped<QuoteService>();
    builder.Services.AddScoped<QuoteConverter>();
    builder.Services.AddSingleton<IUserRepository, UserInMemoryRepository>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<LogInService>();
    builder.Services.AddScoped<UserConverter>();
}

static void AddCorsConfig(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
    });
}

static void AddAuthenticationAndAuthorization(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("this_is_a_very_secret_key_12345678!")) // Load from config!
        };
    });

    builder.Services.AddAuthorization();
}

static void AddBookTestData(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IBookRepository>();

        repo.Save(new Book(
                   Guid.Parse("dd2242e9-9cee-4927-828f-7f35a1ffe501"),
                    "Clean Code",
                    "Robert C. Martin",
                    2009
                ));
        repo.Save(new Book(
                Guid.Parse("d7edbda8-b61c-451a-9719-957f22da7809"),
                "Döda kvinnor förlåtar inte",
                "Katarina Wennstam",
                2023
            ));
        repo.Save(new Book(
                Guid.Parse("234b794e-8ecb-4fdb-ae05-4a1844a7605d"),
                "Klockan K",
                "Agatha Christie",
                1944
            ));
        repo.Save(new Book(
                Guid.Parse("e5efcded-a025-4dc3-8bb4-559954619308"),
                "Clean Architecture",
                "Robert C. Martin",
                2017
        ));
        repo.Save(new Book(
                Guid.Parse("196e069b-fe7a-4a1a-8248-06e1930f4ae1"),
                "Functional Programming in Java",
                "Venkat Subramaniam",
                2023
            ));
    }

}

static void AddQuoteTestData(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();

        repo.Save(new Quote(
                   Guid.NewGuid(),
                    "Felet med vår värld är att de dumma är så säkra på sin sak och de kloka så fulla av tvivel",
                    "Bertran Russel"

                ));
        repo.Save(new Quote(
                Guid.NewGuid(),
                "Var dig själv, alla andra är redan upptagna",
                "Oscar Wilde"

            ));
        repo.Save(new Quote(
            Guid.NewGuid(),
            "I'm not so think as you drunk I am",
            "Margaret Houlihan"
            ));
        repo.Save(new Quote(
            Guid.NewGuid(),
            "Jag tänker , därför är jag",
            "René Descartes"
            ));
    }

}

static void AddTestUser(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        repo.Create(new User("user1", "pw1"));
        repo.Create(new User("user2", "pw2"));
        repo.Create(new User("user3", "pw3"));
    }

}
static void AddLoggingConfig(WebApplicationBuilder builder)
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug(); // Add Debug provider
    builder.Logging.SetMinimumLevel(LogLevel.Information);

    // Add test log message
    var logger = LoggerFactory.Create(config =>
    {
        config.AddConsole();
        config.SetMinimumLevel(LogLevel.Information);
    }).CreateLogger("Program");

    logger.LogInformation("Application starting...");
}
