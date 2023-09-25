
using DataAccess;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models;
using services.implementation;
using Services.Helpers;
using Services.Implementation;
using Services.Interfaces;
using services.implementation;
using Services.Helpers;
using Services.Implementation;
using Services.Interfaces;
using System.Configuration;
using Services.SmtpService;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString).UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var emailConfig = builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IPropertyData,PropertyData>();
builder.Services.AddScoped<ICityData, CityData>();
builder.Services.AddScoped<IStateData, statedata>();
builder.Services.AddScoped<IPropertyImageData,PropertyImageData>();
builder.Services.AddScoped<IFeatureData,FeatureData>();
builder.Services.AddScoped<IAdditionalDetails,AdditionalDetails>();
builder.Services.AddScoped<IStatusData, StatusData>();
builder.Services.AddScoped<INewsletterData, NewsletterData>();
builder.Services.AddScoped<IPropertyFilter, PropertyFilter>();
builder.Services.AddScoped<IBannerData, BannerData>();
builder.Services.AddScoped<IContactData, ContactData>();
builder.Services.AddScoped<IFaqAns, FaqAns>();
builder.Services.AddScoped<IFaqQue, FaqQue>();
builder.Services.AddScoped<IBlogData, BlogData>();
builder.Services.AddScoped<ICategoryData, CategoryData>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseMigrationsEndPoint();
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
app.UseNotyf();
app.Run();
