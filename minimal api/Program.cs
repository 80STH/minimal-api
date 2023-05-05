using Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatasContext>(opt => opt.UseInMemoryDatabase("Datas"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/api/allData", async (DatasContext db) =>
    await db.Datas.ToListAsync());

app.MapGet("/api/scan", async (DatasContext db) =>
    await db.Scan.ToListAsync());

app.MapGet("api/filenames/{value}", async (bool value, DatasContext db) =>
    await db.Files.Where(r => r.result == false).ToListAsync());

app.MapGet("api/errors/count", async (DatasContext db) =>
    await db.Scan.FindAsync(1)
        is Scan scan
            ? Results.Ok(scan.errorCount)
            : Results.NotFound());

//app.MapGet("/api/filenames?correct={value}", async (bool value, AllDataContext db) =>
//    await db.Files.FindAsync(value)
//        is Files file
//            ? Results.Ok(file)
//            : Results.NotFound());

app.MapPost("/api/newErrors", async (HttpRequest request, DatasContext db) =>
{
    var data = await request.ReadFromJsonAsync<Datas>();
    db.Datas.Add(data);
    await db.SaveChangesAsync();

    return Results.Created($"/allData/{data.Id}", data);
});

app.Run();