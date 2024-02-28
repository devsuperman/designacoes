using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using App.Extensions;
using App.Data;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = AppContext.BaseDirectory });

builder.Services.AddControllersWithViews();

var cnn = ConnectionHelper.GetConnectionString(builder.Configuration);

builder.Services.AddDbContext<Contexto>(a =>
                a.UseMySql(cnn, ServerVersion.AutoDetect(cnn)));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = "/Home/Login";
                    o.ExpireTimeSpan = new System.TimeSpan(5, 0, 0, 0);
                });

builder.Services.AddAuthorization();


var portVar = Environment.GetEnvironmentVariable("PORT");

if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UsarCulturaBrasileira();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

var scope = app.Services.CreateScope();
await DataHelper.ManageDataAsync(scope.ServiceProvider);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
