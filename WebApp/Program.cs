using BusinessLayer.GlobalServices;
using BusinessLayer.GlobalServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session service
builder.Services.AddSession();

//load json files and add ti configuration
#region json files
builder.Configuration.AddJsonFile("applicationurl.json");
#endregion

//register interface and implementation
#region services
builder.Services.AddSingleton<IApplicationUrl, ApplicationUrlImpl>();
#endregion

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

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "AdminPortal",
        areaName: "AdminPortal",
        pattern: "AdminPortal/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "FacultyPortal",
        areaName: "FacultyPortal",
        pattern: "FacultyPortal/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "StudentPortal",
        areaName: "StudentPortal",
        pattern: "StudentPortal/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}"
    );
});




app.Run();
