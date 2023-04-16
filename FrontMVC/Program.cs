using AutoMapper;
using FrontMVC.Data;
using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Log;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using FrontMVC.Profiles;
using FrontMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IServicePrato<PratoModel>, PratoService>();
builder.Services.AddScoped<IService<MesaModel>, MesaService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IServiceLog<LogModel>, LogService>();
builder.Services.AddScoped<ClientHelpers>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
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

var app = builder.Build();

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
