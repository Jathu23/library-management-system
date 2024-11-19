using library_management_system.Database;
using library_management_system.Repositories;
using library_management_system.Services;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option => { option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

// JWT configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]))
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register other services
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<UserRepo>();

builder.Services.AddScoped<AdminServices>();
builder.Services.AddScoped<AdminRepo>();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BookRepository>();

builder.Services.AddScoped<EbookService>();
builder.Services.AddScoped<EbookRepository>();

builder.Services.AddScoped<AudioBookService>();
builder.Services.AddScoped<AudioBookRepository>();


builder.Services.AddScoped<GlobalSubscriptionService>();
builder.Services.AddScoped<GlobalSubscriptionRepository>();


builder.Services.AddScoped<LentService>();
builder.Services.AddScoped<LentRepository>();



builder.Services.AddScoped<ReturnService>();
builder.Services.AddScoped<ReturnRepository>();

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LoginRepository>();



builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<BCryptService>();
builder.Services.AddScoped<EbookFileService>();
builder.Services.AddScoped<AudioBookFileService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors("AllowAll");

app.UseHttpsRedirection();

// Enable authentication and authorization
app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllers();

app.Run();
