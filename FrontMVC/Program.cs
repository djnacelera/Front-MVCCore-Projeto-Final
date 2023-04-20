using AutoMapper;
using FrontMVC.Data;
using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Cliente;
using FrontMVC.Models.Log;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using FrontMVC.Profiles;
using FrontMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IServicePrato<PratoModel>, PratoService>();
builder.Services.AddScoped<IServiceCliente<ClienteModel>, ClienteService>();
builder.Services.AddScoped<IServiceMesa<MesaModel>, MesaService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IServiceLog<LogModel>, LogService>();
builder.Services.AddScoped<ClientHelpers>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MesaProfile());
  
});
var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
//{
//    options.Password.RequiredLength = 8;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[] { new CultureInfo("pt-BR") };
//    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
//    options.SupportedCultures = supportedCultures;
//    options.SupportedUICultures = supportedCultures;
//});

var app = builder.Build();

//app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
