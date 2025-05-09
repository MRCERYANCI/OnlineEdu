using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineEdu.API.Extensions;
using OnlineEdu.BusinessLayer.Configurations;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.EntityLayer.Entities;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddServiceExtensions(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<OnlineEduContext>();

builder.Services.AddDbContext<OnlineEduContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
    //options.UseLazyLoadingProxies();
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 1073741824; // 1 GB
});




builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); // CORS politikasını burada etkinleştiriyoruz
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
