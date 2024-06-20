using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BITS.Data;
using BITS.Models;
using Microsoft.AspNetCore.Identity;
using BITS.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BITSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BITSContext") ?? throw new InvalidOperationException("Connection string 'BITSContext' not found.")));
builder.Services.AddDbContext<UserContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserContext") ?? throw new InvalidOperationException("Connection string 'UserContext' not found.")));

builder.Services
    .AddDefaultIdentity<BITSUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserContext>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.MapRazorPages();

app.Run();
