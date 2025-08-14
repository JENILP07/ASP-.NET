using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session support
builder.Services.AddDistributedMemoryCache(); // In-memory cache for session state
builder.Services.AddHttpContextAccessor();    // Access HTTP context in services if needed
builder.Services.AddSession(options =>
{
    // Configure session settings
    options.Cookie.HttpOnly = true;  // Makes cookie accessible only through HTTP
    options.Cookie.IsEssential = true;  // Ensures the cookie is sent to the client even if consent is not given
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithRedirects("/Error404");
app.UseStaticFiles();

// Use session middleware
app.UseSession();

app.UseRouting();

// Enable authorization middleware
app.UseAuthorization();

// Define the default controller route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();