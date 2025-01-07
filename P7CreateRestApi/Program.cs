using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

async Task SeedRoles(IServiceProvider serviceProvider, ILogger logger)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                logger.LogInformation($"Role '{roleName}' created successfully.");
            }
            else
            {
                logger.LogWarning($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            logger.LogInformation($"Role '{roleName}' already exists.");
        }
    }
}

async Task SeedUsers(IServiceProvider serviceProvider, ILogger logger)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var user = await userManager.FindByEmailAsync("admin@example.com");

    if (user != null)
    {
        logger.LogInformation("Admin user already exists.");

        // Check if the user is in the Admin role
        var isInRole = await userManager.IsInRoleAsync(user, "Admin");
        logger.LogInformation($"Is Admin user in 'Admin' role? {isInRole}");

        if (!isInRole)
        {
            var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
            if (addToRoleResult.Succeeded)
            {
                logger.LogInformation("Admin user assigned to 'Admin' role successfully.");
            }
            else
            {
                logger.LogWarning($"Failed to assign role 'Admin' to user: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
            }
        }
    }
    else
    {
        user = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        var createResult = await userManager.CreateAsync(user, "Password123!");

        if (createResult.Succeeded)
        {
            logger.LogInformation("Admin user created successfully.");
            var addToRoleResult = await userManager.AddToRoleAsync(user, "Admin");
            if (addToRoleResult.Succeeded)
            {
                logger.LogInformation("Admin user assigned to 'Admin' role successfully.");
            }
            else
            {
                logger.LogWarning($"Failed to assign role 'Admin' to user: {string.Join(", ", addToRoleResult.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            logger.LogWarning($"Failed to create admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
        }
    }
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
 {
 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

 // Add JWT Authentication scheme
 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
 {
     In = ParameterLocation.Header,
     Description = "Please enter a valid token",
     Name = "Authorization",
     Type = SecuritySchemeType.Http,
     BearerFormat = "JWT",
     Scheme = "Bearer"
 });
     c.AddSecurityRequirement(new OpenApiSecurityRequirement
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



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<LocalDbContext>()
    .AddDefaultTokenProviders();

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = ClaimTypes.Role
    };
});

///
//services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = configuration["Jwt:Issuer"],
//        ValidAudience = configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
//        RoleClaimType = ClaimTypes.Role

//    };
//});

//



builder.Services.AddAuthorization();    


//Injection de dépendences
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
builder.Services.AddScoped<RatingService>();

builder.Services.AddScoped<BidListService>();
builder.Services.AddScoped<IRepository<BidList>, BidListRepository>();

builder.Services.AddScoped<CurvePointService>();
builder.Services.AddScoped<IRepository<CurvePoint>, CurvePointRepository>();

builder.Services.AddScoped<RuleNameService>();
builder.Services.AddScoped<IRepository<RuleName>, RuleNameRepository>();

builder.Services.AddScoped<TradeService>();
builder.Services.AddScoped<IRepository<Trade>, TradeRepository>();


builder.Services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, CustomClaimsPrincipalFactory>();

//builder.Services.AddDbContext<LocalDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,            // Number of retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(10),  // Delay between retries
            errorNumbersToAdd: null      // Null lets it retry on all transient errors
        )
    )
);



builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401; // Return Unauthorized instead of redirecting to login
        return Task.CompletedTask;
    };
});

var app = builder.Build();

// Add UseRouting before UseAuthentication and UseAuthorization
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    var logger = scopedProvider.GetRequiredService<ILogger<Program>>();

    await SeedRoles(scopedProvider, logger);
    await SeedUsers(scopedProvider, logger);
}




// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    c.RoutePrefix = string.Empty;  
});

app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();

