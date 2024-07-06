using FollowService.Infrastructure;
using FollowService.Infrastructure.Achieve;
using FollowService.Infrastructure.other;
using FollowService.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//���ݿ�
builder.Services.AddDbContext<MyDbContext>(x =>
{
    x.UseSqlServer("Server=.;Database=Galaxy-S;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=true;");
});
//ͳһSaveChanges
builder.Services.Configure<MvcOptions>(x =>
{
    x.Filters.Add<UnitOfWorkFilter>();
});
//����
builder.Services.AddCors(i => i.AddDefaultPolicy(v =>
{
    v.WithOrigins(new string[] { "http://localhost:5173" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

//ע��
builder.Services.AddScoped<MyDbContext>();
builder.Services.AddScoped<MyRedis>();
builder.Services.AddScoped<NewFollow>();
builder.Services.AddScoped<MyRabbitMQ>();

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
