using Cars.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Get the base URL for HttpClient services from configuration
string baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Create a reusable method to register HttpClient services
void RegisterHttpClient<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
{
    builder.Services.AddHttpClient<TInterface, TImplementation>(client =>
    {
        client.BaseAddress = new Uri(baseUrl);
    });
}

// Register HttpClient services for Car, Inquiry, ContactUs, and CarRequest services
RegisterHttpClient<ICarService, CarService>();
RegisterHttpClient<IInquiryService, InquiryService>();
RegisterHttpClient<IContactUsService, ContactUsService>();
RegisterHttpClient<ICarRequestService, CarRequestService>();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Default routing for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=CarListing}/{id?}");

// Map Admin to Login action
app.MapControllerRoute(
    name: "adminLogin",
    pattern: "Admin",
    defaults: new { controller = "Admin", action = "Login" });

// Optional: Map direct access to Login action
app.MapControllerRoute(
    name: "adminLoginAction",
    pattern: "Admin/Login",
    defaults: new { controller = "Admin", action = "Login" });

app.Run();
