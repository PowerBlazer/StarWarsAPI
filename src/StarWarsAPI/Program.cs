using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Common;
using StarWarsAPI.Context;
using StarWarsAPI.Context.Abstraction;
using StarWarsAPI.Services;
using StarWarsAPI.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

#region BaseConfiguration
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(coreOptions =>
    coreOptions.AddPolicy("All", options =>
    {
        options.AllowAnyHeader();
        options.AllowAnyMethod();
        options.AllowAnyOrigin();
    }));

builder.Services
    .AddEndpointsApiExplorer()
    .AddHttpContextAccessor();

builder.Services.AddSwaggerConfiguration();


#endregion

#region ApiVersionSetting

builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

#endregion

#region BusinessConfiguration

builder.Services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration["Databases:SqlLite"]));

builder.Services
    .AddScoped<ICharacterService, CharacterService>()
    .AddScoped<AuthorizationService>();

builder.Services
    .AddAutoMapper(typeof(MapperProfile));

#endregion

#region AuthorizationSetting

var jwtOptions = builder.Configuration.GetSection("JWT")
    .Get<JwtOptions>()!;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = false,
            ValidAudience = jwtOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped(_ => new JwtHelper(jwtOptions));

#endregion

var app = builder.Build();

app.UseCors("All");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup(app.Services
        .GetRequiredService<IApiVersionDescriptionProvider>());
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();