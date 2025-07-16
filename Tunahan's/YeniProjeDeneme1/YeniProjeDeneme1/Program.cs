using Microsoft.EntityFrameworkCore;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Validator;
using FluentValidation;
using YeniProjeDeneme1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
var key = "bunu-sakinnn-32-karakter-uzat-****"; // EN AZ 32 karakter

// Add services to the container.
builder.Services.AddScoped<IValidator<SensorDataCreateDto>, Sensor_DataCreateValidator>();
builder.Services.AddScoped<IValidator<SensorDataUpdateDto>, Sensor_DataUpdateValidator>();
builder.Services.AddScoped<IValidator<SensorCreateDto>, SensorCreateValidator>();
builder.Services.AddScoped<IValidator<SensorUpdateDto>, SensorUpdateValidator>();
builder.Services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
builder.Services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();

builder.Services.AddScoped<IValidator<ProjectCreateDto>, ProjectCreateValidator>();
builder.Services.AddScoped<IValidator<ProjectUpdateDto>, ProjectUpdateValidator>();
builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();
builder.Services.AddScoped<IValidator<ProjectUserCreateDto>, ProjectUserCreateValidator>();
builder.Services.AddScoped<IValidator<ProjectUserUpdateDto>, ProjectUserUpdateValidator>();

builder.Services.AddScoped<IValidator<ProjectProductCreateDto>, ProjectProductCreateValidator>();
builder.Services.AddScoped<IValidator<ProjectProductUpdateDto>, ProjectProductUpdateValidator>();
builder.Services.AddScoped<Sensor_DataService>();
builder.Services.AddScoped<SensorService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProjectProductService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectUserService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();

// SWAGGER TOKEN KULLANIMI
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Bearer [token] þeklinde yaz.",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // BURASI ÞART!
app.UseAuthorization();

app.MapControllers();

app.Run();
