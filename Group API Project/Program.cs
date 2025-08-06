using NSwag.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Group_API_Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<JsonDataService>();

builder.Services.AddOpenApiDocument(cfg =>
{
    cfg.DocumentName = "v1";
    cfg.PostProcess = doc =>
    {
        doc.Info.Title = "Group API Project";
        doc.Info.Version = "1.0.0";
        doc.Info.Description = "CRUD API for our TeamMembers, Pets, Hobbies, and FavoriteFoods.";
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();