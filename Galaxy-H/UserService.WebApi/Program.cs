using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewMailKit;
using Quartz;
using Quartz.Spi;
using UserService.Domain;
using UserService.Infrastructure;
using UserService.Infrastructure.Achieve;
using UserService.WebApi;
using UserService.WebApi.Quartzs;

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
builder.Services.AddScoped<Code>();
builder.Services.AddScoped<MyRedis>();
builder.Services.AddScoped<Mail>();
builder.Services.AddScoped<MyDbContext>();
builder.Services.AddScoped<JWT>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<NewUser>();


//配置Quartz
builder.Services.AddQuartz(Q =>
{
    // 使用Microsoft依赖注入作为作业工厂  
    Q.UseMicrosoftDependencyInjectionJobFactory();

    // 添加作业  
    Q.AddJob<MyJob>(opts => opts
        .WithIdentity("myJob", "group1")
        .StoreDurably());

    // 添加触发器  
    Q.AddTrigger(opts => opts
        .ForJob("myJob", "group1")
        .WithIdentity("myTrigger", "group1")
        .StartNow() // 立即开始  
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(1) // 每1分钟执行一次  
            .RepeatForever())); // 重复执行  
});
builder.Services.AddQuartzHostedService(options =>
{
    // 设置为等待所有作业完成后才关闭应用程序（可选）  
    options.WaitForJobsToComplete = true;
});


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
