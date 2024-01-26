using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{

    //using (var ctx = scope.ServiceProvider.GetRequiredService<OraDbContext>())
    //{
    //    if (ctx.Database.GetPendingMigrations().Any())
    //    {
    //        await ctx.Database.MigrateAsync();
    //    }
    //    await ctx.Database.EnsureCreatedAsync();
    //}
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
