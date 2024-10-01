using Dot.Net.WebApi.Controllers;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
