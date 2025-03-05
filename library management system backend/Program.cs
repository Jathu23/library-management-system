using DinkToPdf.Contracts;
using DinkToPdf;
using library_management_system.Database;
using library_management_system.Repositories;
using library_management_system.Services;
using library_management_system.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using VersOne.Epub.Schema;
using PdfSharp.Charting;
using MailSend.Models;
using MailSend.Repo;
using MailSend.Service;
using Microsoft.Extensions.Options;
using System.Runtime.Loader;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IConverter, SynchronizedConverter>(provider => new SynchronizedConverter(new PdfTools()));
// Load unmanaged DLL (wkhtmltox) - Ensure correct DLL path
string dllPath = Path.Combine(AppContext.BaseDirectory, "libwkhtmltox.dll");

if (!File.Exists(dllPath))
{
    throw new FileNotFoundException($"libwkhtmltox.dll not found at: {dllPath}");
}

var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(dllPath);


builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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

// Register EmailConfig
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

//Email Services

builder.Services.AddSingleton(provider => new Email(
    smtpUser: builder.Configuration["Gmail:Email"], // Read from appsettings.json or environment variables
    smtpPass: builder.Configuration["Gmail:Password"]
));

builder.Services.AddControllers();
builder.Services.AddSingleton<IConverter, SynchronizedConverter>(
    provider => new SynchronizedConverter(new PdfTools()));

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
builder.Services.AddScoped<EmailService>();


builder.Services.AddScoped<LentService>();
builder.Services.AddScoped<LentRepository>();



builder.Services.AddScoped<ReturnService>();
builder.Services.AddScoped<ReturnRepository>();

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LoginRepository>();

builder.Services.AddScoped<SubscriptionService>();
builder.Services.AddScoped<SubscriptionRepository>();

builder.Services.AddScoped< ChartRepository>();
builder.Services.AddScoped< ChartService>();

builder.Services.AddScoped< LikeDislikeRepository>();
builder.Services.AddScoped< ReviewRepository>();

builder.Services.AddScoped<LikeDislikeService>();
builder.Services.AddScoped<ReviewService>();


builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<BCryptService>();
builder.Services.AddScoped<EbookFileService>();
builder.Services.AddScoped<AudioBookFileService>();
builder.Services.AddScoped<PdfGeneratorService>();

builder.Services.AddScoped<ForgotPasswordRepository>();
builder.Services.AddScoped<ForgotPasswordService>();

// Register services
builder.Services.AddScoped<sendmailService>();
builder.Services.AddScoped<SendMailRepository>();
builder.Services.AddScoped<EmailServiceProvider>();

// Ensure EmailConfig is available as a singleton if needed
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Urls.Add("https://localhost:7261");
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


public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDllFromPath(absolutePath); // Fixed method
    }
}