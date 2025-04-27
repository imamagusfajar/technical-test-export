using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan ini
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Setup Rotativa dengan path yang benar
var wkhtmlPath = Path.Combine(builder.Environment.WebRootPath, "wkhtmltopdf", "bin", "wkhtmltopdf.exe");
Console.WriteLine($"wkhtmltopdf.exe path: {wkhtmlPath}");
RotativaConfiguration.Setup(builder.Environment.WebRootPath, "wkhtmltopdf/bin");

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
