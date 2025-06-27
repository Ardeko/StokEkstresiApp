using StokEkstresiApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Connection string'i appsettings.json'dan alıyoruz
var connectionString = builder.Configuration.GetConnectionString("ArdoConnection");

// ArdoDbHelper'ı DI konteynerine ekliyoruz ve bağlantı stringini enjekte ediyoruz
builder.Services.AddSingleton<ArdoDbHelper>(sp =>
    new ArdoDbHelper(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP request pipeline yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
