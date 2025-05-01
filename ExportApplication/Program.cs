using ExportApplication.Repository;
using Rotativa.AspNetCore;
using static ExportApplication.Repository.Interface.Interface;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan ini
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
var app = builder.Build();

// Setup Rotativa dengan path yang benar
RotativaConfiguration.Setup(builder.Environment.WebRootPath, "wkhtmltopdf/acc");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Export}/{action=Index}/{id?}");

app.Run();
