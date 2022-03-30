using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Options;
using MandoWebApp.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity.UI.Services;
using MandoWebApp.Services.EmailSender;
using Microsoft.AspNetCore.Identity;
using MandoWebApp.Services.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;

        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddRazorPages();

RegisterOptions(builder);

RegisterServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();

static void RegisterOptions(WebApplicationBuilder builder)
{
    builder.Services.AddOptions<MandoAuthOptions>().BindConfiguration("Authentication");
    builder.Services.AddOptions<EmailOptions>().BindConfiguration("Email");
}

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IClaimsTransformation, RoleClaimsTransformation>();
    builder.Services.AddTransient<IInviteManager, InviteManager>();
    builder.Services.AddTransient<IEmailSender, EmailSender>();
}
