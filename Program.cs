using System.Text;
using FlashFood.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using User.Management.Data.Data;
using User.Management.Data.Data.DTOs;
using User.Management.Service.Models;
using User.Management.Service.Repositories.DishRepository;
using User.Management.Service.Repositories.OrderRepository;
using User.Management.Service.Repositories.RestaurantRepository;
using User.Management.Service.Services;
using User.Management.Service.Services.RestaurantService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// For Identity
/*builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();*/

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Tokens.ProviderMap["Email"] = new TokenProviderDescriptor(typeof(EmailTokenProvider<ApplicationUser>));
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});

// Add Configuration for Required Email
builder.Services.Configure<IdentityOptions>(
    opts => opts.SignIn.RequireConfirmedEmail = true
    );

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

// Add Email Configuration
var emailConfig = builder.Configuration
         .GetSection("EmailConfiguration")
         .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

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
