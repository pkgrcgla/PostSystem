using Microsoft.EntityFrameworkCore;
using PostSystem_BL.EmailSenderProcess;
using PostSystem_BL.ImplementationOfManagers;
using PostSystem_BL.InterfacesOfManagers;
using PostSystem_DL.ContextInfo;
using PostSystem_DL.ImplementationsOfRepos;
using PostSystem_DL.InterfaceOfRepos;
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

//interfacelerin DI yasam dongusu
builder.Services.AddScoped<IEmailManager, EmailManager>();

builder.Services.AddScoped<IUserPostRepo,UserPostRepo>();
builder.Services.AddScoped<IUserPostManager, UserPostManager>();

builder.Services.AddScoped<IPostTagRepo, PostTagRepo>();
builder.Services.AddScoped<IPostTagManager, PostTagManager>();

builder.Services.AddScoped<IPostMediaRepo, PostTagRepo>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
