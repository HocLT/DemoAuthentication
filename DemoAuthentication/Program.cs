using DemoAuthentication.Data;
using DemoAuthentication.Profiles;
using DemoAuthentication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<AuthenticationDbContext>(o =>o.UseSqlServer(builder.Configuration.GetConnectionString("AuthenticationConnectionString")));

// Register Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthentication("CustomLogin").AddCookie("CustomLogin", o => {
    o.LoginPath = @"/Authentication/Login";
    o.LogoutPath = @"/Authentication/Logout";
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

// register profile
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// register session
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromSeconds(20);
    o.Cookie.IsEssential = true;
    o.Cookie.Name = "DemoAuthentication.WebApp";
    o.Cookie.SameSite = SameSiteMode.Lax;
    o.Cookie.HttpOnly = true;
});

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
