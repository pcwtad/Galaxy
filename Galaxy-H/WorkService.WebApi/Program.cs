using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkService.Infrastructure;
using WorkService.Infrastructure.Achieve;
using WorkService.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//数据库
builder.Services.AddDbContext<MyDbContext>(x =>
{
    x.UseSqlServer("Server=.;Database=Galaxy-S;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=true;");
});
//统一SaveChanges
builder.Services.Configure<MvcOptions>(x =>
{
    x.Filters.Add<UnitOfWorkFilter>();
});
//跨域
builder.Services.AddCors(i => i.AddDefaultPolicy(v =>
{
    v.WithOrigins(new string[] { "http://localhost:5173" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));
//注入
builder.Services.AddScoped<MyDbContext>();
builder.Services.AddScoped<NewDetailsImg>();
builder.Services.AddScoped<preserve>();
builder.Services.AddScoped<StorageClient>();
builder.Services.AddScoped<Display>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
