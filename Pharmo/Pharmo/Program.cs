using Microsoft.EntityFrameworkCore;
using Pharmo.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

IServiceCollection serviceCollection = builder.Services.AddDbContext<DbApiContext>(options => options.UseSqlServer(connection));

// Использую Swagger чтобы посмотреть работоспособность 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Проверка и заполнение БД
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbApiContext>();
    context.Database.EnsureCreated();
    context.SeedData();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/pharmacies/{medicineCode:int}", async (int medicineCode, DbApiContext db) =>
{
    var pharmacies = await db.Pharmacies.Where(p => p.MedicineCode == medicineCode).ToListAsync();
    return Results.Json(pharmacies);
});

app.MapDelete("/api/medicines/expired", async (DbApiContext db) =>
{
    var medicines = await db.Medicines.Where(m => m.ExpiryDate < DateTime.Now).ToListAsync();
    db.Medicines.RemoveRange(medicines);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/api/medicines", async (Medicine medicine, DbApiContext db) =>
{
    db.Medicines.Add(medicine);
    await db.SaveChangesAsync();
    return Results.Created($"/api/medicines/{medicine.Id}", medicine);
});

app.MapPost("/api/pharmacies", async (Pharmacy pharmacy, DbApiContext db) =>
{
    db.Pharmacies.Add(pharmacy);
    await db.SaveChangesAsync();
    return Results.Created($"/api/pharmacies/{pharmacy.Id}", pharmacy);
});

app.Run();
