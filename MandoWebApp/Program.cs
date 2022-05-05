using Duende.IdentityServer.Models;
using IdentityModel;
using MandoWebApp.Data;
using MandoWebApp.Models;
using MandoWebApp.Options;
using MandoWebApp.Services.BuildingService;
using MandoWebApp.Services.EmailSender;
using MandoWebApp.Services.InviteService;
using MandoWebApp.Services.ProductService;
using MandoWebApp.Services.UserManangement;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var dbOptions = serviceProvider.GetRequiredService<IOptions<DbOptions>>().Value;

    var connectionString = $"server={dbOptions.Server};port={dbOptions.Port};database={dbOptions.Database};uid={dbOptions.Uid};password={dbOptions.Password}";

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

// Prevent HTTPS redirection loop with traefik in production
// Solution: https://laimis.medium.com/couple-issues-with-https-redirect-asp-net-core-7021cf383e00
builder.Services.Configure<ForwardedHeadersOptions>(options =>
  {
      options.ForwardedHeaders = 
          ForwardedHeaders.XForwardedFor | 
          ForwardedHeaders.XForwardedProto;

      options.KnownNetworks.Clear();
      options.KnownProxies.Clear();
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

app.UseForwardedHeaders();
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
    builder.Services.AddOptions<DbOptions>().BindConfiguration("Db");
}

static void RegisterServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<IInviteService, InviteService>();
    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IUserManagementService, UserManagementService>();
    builder.Services.AddTransient<IBuildingService, BuildingService>();

    builder.Services
        .AddFluentEmail(builder.Configuration["Email:FromEmail"])
        .AddRazorRenderer();
}
