using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using devnetcloud.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SessionManager>();


//builder.Services.AddScoped<IUserRepository, UserRepositoryDb>(x => new UserRepositoryDb(builder.Configuration.GetConnectionString("DevNetCloudDB")));

builder.Services.AddScoped<IUserRepository, UserRepositoryAPI>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "all",
    pattern: "{*url}",
    defaults : new {controller = "home", action = "index"}
    );

app.Run();
