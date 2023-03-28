using Micro.MessageBus;
using Micro.Web;
using Micro.Web.Services.Abstract;
using Micro.Web.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IProductService, ProductService>();
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddSingleton<IRabbitMQMessageBus, RabbitMQMessageBus>();


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Cookie.Name = "AccessToken";
//        //options.ExpireTimeSpan = TimeSpan.FromDays(1);
//        //options.SlidingExpiration = true;
//        options.AccessDeniedPath = "/Forbidden/";
//        options.LoginPath = "/Login/Index";
//    });

//builder.Services.AddAuthentication(
//    x =>
//    {
//        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    }
//    ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
//    {
//        var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["token-key"]));

//        x.TokenValidationParameters = new TokenValidationParameters()
//        {
//            IssuerSigningKey = _key,
//            ValidIssuer = "www.mywebsite.com/api",
//            ValidateIssuer = true,
//            ValidAudience = "www.mywebsite.com",
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ClockSkew = TimeSpan.Zero
//        };
//        x.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                var token = context.Request.Cookies["token"];
//                context.Token = token;
//                return Task.CompletedTask;
//            }
//        };
//    });

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
