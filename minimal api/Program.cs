using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatasContext>(opt => opt.UseInMemoryDatabase("Datas"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
Datas? dataJson = new Datas(); //поскольку требуется результаты считывания файла, нужна глобальная переменная

app.MapGet("/api/allData", (DatasContext db) =>
{
    return Results.Created($"/allData/{dataJson.Id}", dataJson);
});

app.MapGet("/api/scan", async (DatasContext db) =>
    await db.Scan.ToArrayAsync());

//выбрасывает исключение при наличии вопросительного знака, но корректность все равно будет проверена
app.MapGet("api/filenames/{value}", async (bool value, DatasContext db) =>
    await db.Files.Where(r => r.result == value).Select(f => f.filename).ToArrayAsync());

app.MapGet("api/errors", (DatasContext db) =>
{
    var errorsDto = from b in db.Files
                    where b.result == false
                    select new FilesErrorsDTO(b)
                    {
                        filename = b.filename,
                        errors = b.errors
                    };
    return errorsDto;
});


app.MapGet("api/errors/count", async (DatasContext db) =>
    await db.Scan.FindAsync(1)
        is Scan scan
            ? Results.Ok(scan.errorCount)
            : Results.NotFound());

app.MapGet("api/errors/{index}", (int index, DatasContext db) =>
{ 
    var errorsDto = from b in db.Files
                    where b.result == false
                    select new FilesErrorsDTO(b)
                    {
                        filename = b.filename,
                        errors = b.errors
                    };
    if (index < errorsDto.ToList().Count)
        return Results.Ok(errorsDto.ToList()[index]);
    else
        return Results.NotFound();
});



app.MapGet("api/query/check", (DatasContext db) =>
{
    var checkDto = from d in db.Datas
                   select new FilenameCheckDTO(d)
                   {
                       total = d.files.Count(f => f.filename.Contains("query_")),
                       correct = d.files.Count(f => f.filename.Contains("query_") && f.result == true),
                       errors_count = d.files.Count(f => f.filename.Contains("query_") && f.result == false),
                       filenames = d.files.Where(f => f.filename.Contains("query_") && f.result == false).Select(f => f.filename).ToArray()
                   };
    return checkDto;
});

app.MapGet("api/service/serviceinfo", (DatasContext db) =>
{
    return new ServiceInfoDto();
});


app.MapPost("/api/newErrors", async (HttpRequest request, DatasContext db) =>
{
    dataJson = await request.ReadFromJsonAsync<Datas>();
    db.Datas.Add(dataJson);
    await db.SaveChangesAsync();

    if (dataJson == null)
        return Results.NoContent();
    else
        return Results.Created($"/allData/{dataJson.Id}", dataJson);
});

app.Run();