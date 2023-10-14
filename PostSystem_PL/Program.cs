using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostSystem_BL.EmailSenderProcess;
using PostSystem_BL.ImplementationOfManagers;
using PostSystem_BL.InterfacesOfManagers;
using PostSystem_DL.ContextInfo;
using PostSystem_DL.ImplementationsOfRepos;
using PostSystem_DL.InterfaceOfRepos;
using PostSystem_EL.IdentityModels;
using PostSystem_EL.Mappings;
using PostSystem_EL.ViewModels;
using PostSystem_PL.CreateDefaultData;
using PostSystem_PL.Models;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//culture info
var cultureInfo = new CultureInfo("tr-TR");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// serilog logger ayarlari

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//contexti ayarliyoruz.
builder.Services.AddDbContext<PostSystemContext>(options =>
{
    //klasik mvcde connection string web configte yer alir.
    //core mvcde connection string appsetting.json dosyasindan alinir.
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyLocal"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

//appuser ve approle identity ayari
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&%";

}).AddDefaultTokenProviders().AddEntityFrameworkStores<PostSystemContext>();

//automapper ayari 
builder.Services.AddAutoMapper(a =>
{
    a.AddExpressionMapping();
    a.AddProfile(typeof(Maps));
    a.CreateMap<PostIndexVM, UserPostDTO>().ReverseMap();
});

//interfacelerin DI yasam dongusu
builder.Services.AddScoped<IEmailManager, EmailManager>();

builder.Services.AddScoped<IUserPostRepo,UserPostRepo>();
builder.Services.AddScoped<IUserPostManager, UserPostManager>();

builder.Services.AddScoped<IPostTagRepo, PostTagRepo>();
builder.Services.AddScoped<IPostTagManager, PostTagManager>();

builder.Services.AddScoped<IPostMediaRepo, PostMediaRepo>();
builder.Services.AddScoped<IPostMediaManager, PostMediaManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //login logout
app.UseAuthorization();//yetki

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Sistem ilk ayaga kalktiginnda rolleri ekleyelim
//ADMIN, MEMBER, WAITINGFORACTIVATION, PASSIVE

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    CreateData c = new CreateData(logger);
    c.CreateRoles(serviceProvider);


}
app.Run();
