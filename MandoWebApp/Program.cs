using Duende.IdentityServer.Models;
using IdentityModel;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Options;
using MandoWebApp.Services;
using MandoWebApp.Services.BuildingService;
using MandoWebApp.Services.EmailSender;
using MandoWebApp.Services.ProductService;
using MandoWebApp.Services.UserManangement;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    // TODO: Move to config:
    var server = "localhost";
    var port = "3306";
    var database = "mando";
    var uid = "root";
    var password = "";
    var connectionString = $"server={server};port={port};database={database};uid={uid};password={password}";

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;

        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedAccount = true;

        options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(opts =>
    {
        opts.IdentityResources.Add(new IdentityResource("roles", new[] { JwtClaimTypes.Role }));

        opts.Clients.First().AllowedScopes.Add("roles");
        opts.Clients.First().UpdateAccessTokenClaimsOnRefresh = true;
        opts.ApiResources.First().UserClaims.Add(JwtClaimTypes.Role);
    });

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddRazorPages();

builder.Services.Configure<JwtBearerOptions>(
    IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
    options =>
    {
        options.MapInboundClaims = false;
    });

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
    builder.Services.AddTransient<IInviteManager, InviteManager>();
    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IUserManagementService, UserManagementService>();
    builder.Services.AddTransient<IBuildingService, BuildingService>();
}
