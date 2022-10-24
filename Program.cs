using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RecordShop.Models;
using RecordShop.Services;
using RecordShop.Store;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        BearerFormat = "JWT",
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/oauth/token"),
                AuthorizationUrl = new Uri($"https://{builder.Configuration["Auth0:Domain"]}/authorize?audience={builder.Configuration["Auth0:Audience"]}"),
                Scopes = new Dictionary<string, string>
                  {
                      { "openid", "OpenId" },

                  }
            }
        }
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
          {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
              },
              new[] { "openid" }
          }
      });
});
builder.Services.Configure<MongoDBSettings>(
        builder.Configuration.GetSection("MongoDBSettings")
    );
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
     {
         c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
         c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidAudience = builder.Configuration["Auth0:Audience"],
             ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
         };
     });
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("records:read-write", p => p.
        RequireAuthenticatedUser().
        RequireClaim("scope", "records:read-write"));
});

//Dependencey Injection for Services
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IRecordRepository, RecordRepository>();
builder.Services.AddTransient<IRecordFilterService, RecordFilterService>();



builder.Services.AddSingleton<IMongoDatabase>(options =>
{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

    var client = new MongoClient(settings.ConnectionString);

    return client.GetDatabase(settings.DatabaseName);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(settings =>
    {
        settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
        settings.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
        settings.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
        settings.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
