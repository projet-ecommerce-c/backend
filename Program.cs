using backend_ecommerce.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<EcommerceDbContext>();

    try
    {
        if (await db.Database.CanConnectAsync())
        {
            Console.WriteLine("Connexion Neon PostgreSQL réussie !");
            var nombreProduits = await db.Produits.CountAsync();

            Console.WriteLine($"Nombre de produits dans la base : {nombreProduits}");
        }
        else
        {
            Console.WriteLine("Impossible de se connecter à Neon.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur de connexion : {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();



