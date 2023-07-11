using SiteWeb.Services;
using SiteWeb.Routes;
using System.Configuration;
using SiteWeb.Services.Middlewares;
using SiteWeb.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    
//});

builder.AddMyServices();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseMiddleware<RedirectToLoginMiddleware>();

app.UseAuthorization();

app.ConfigureRoutes();
app.MapHub<ChatHub>("/chatHub");

//app.UseTokenMiddleware();

app.Run();
