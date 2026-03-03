using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Service;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddOpenApi();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is missing!");

builder.Services.AddDbContext<ApplicationDBContext>(options => 
                                                               
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));                                                                      
                                                                                        
});

builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
{
    Options.Password.RequireDigit = true;
    Options.Password.RequiredLength = 8;
    Options.Password.RequireNonAlphanumeric = true;
    Options.Password.RequireUppercase = true;
    Options.Password.RequireLowercase = true;

}).AddEntityFrameworkStores<ApplicationDBContext>();

//scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = jwtSettings.GetValue<bool>("ValidateIssuer"),
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = jwtSettings.GetValue<bool>("ValidateAudience"),
        ValidAudience = jwtSettings["Audience"],

        ValidateLifetime = jwtSettings.GetValue<bool>("ValidateLifetime"),
        
        ValidateIssuerSigningKey = jwtSettings.GetValue<bool>("ValidateIssuerSigningKey"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});




builder.Services.AddScoped<IClientiRepository, ClientiRepository>();
builder.Services.AddScoped<IOperazioniRepository, OperazioniRepository>();
//builder.Services.AddScoped<IUtentiRepository, UtentiRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
