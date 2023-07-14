using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using NCourses.Services.Discount.Api.Services;
using NCourses.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSharedDependencies();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var requireAuthorizePolicy = new AuthorizationPolicyBuilder()
    .RequireClaim("sub")
    .RequireAuthenticatedUser()
    .Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServerURL"];
        options.Audience = "resource_discount";
        options.RequireHttpsMetadata = false;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();