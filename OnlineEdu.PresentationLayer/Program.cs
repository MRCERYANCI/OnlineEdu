using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.EntityLayer.Entities;
using OnlineEdu.PresentationLayer.Services;
using OnlineEdu.PresentationLayer.Services.MailServices;
using OnlineEdu.PresentationLayer.Services.RoleServices;
using OnlineEdu.PresentationLayer.Services.UserService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IEmailSender, EMailSender>();

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpClient();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = "/Account/Login"; //Sisteme Giri� Yapmadan Eri�meye �al���yorsa Bu Sayfaya Atacak
    cfg.LogoutPath = "/Account/LogOut";
});

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OnlineEduContext>().AddErrorDescriber<CustomErrorDescriber>().AddDefaultTokenProviders();

builder.Services.AddDbContext<OnlineEduContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

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

app.UseStatusCodePagesWithReExecute("/Error/{0}");
//app.UseExceptionHandler("/Pages/HandleError"); // Sunucu hatalar� i�in y�nlendirme

app.UseAuthentication(); //Sisteme Giri� Yapmak ��in
app.UseAuthorization(); //Yetkilendirme ��in

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.Run();
