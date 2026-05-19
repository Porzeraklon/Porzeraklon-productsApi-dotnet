using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseInMemoryDatabase("Products"));

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // HttpsRedirection zostaje tylko lokalnie. Na OpenShifcie ruch i tak 
    // jest szyfrowany przez Router (Ingress) przed dotarciem do kontenera.
    app.UseHttpsRedirection();
}

// 1. JAWNE URUCHOMIENIE ROUTINGU (Kluczowe dla prawidłowego działania CORS)
app.UseRouting();

// 2. URUCHOMIENIE CORS (Musi być PO UseRouting, ale PRZED Autoryzacją i Mapowaniem)
app.UseCors("AllowAll");

// 3. AUTORYZACJA I MAPOWANIE KONTROLERÓW
app.UseAuthorization();
app.MapControllers();

app.Run();
