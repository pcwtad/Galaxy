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
builder.Services.AddScoped<Code>();
builder.Services.AddScoped<MyRedis>();
builder.Services.AddScoped<Mail>();
builder.Services.AddScoped<MyDbContext>();
builder.Services.AddScoped<JWT>();
builder.Services.AddScoped<Login>();
builder.Services.AddScoped<NewUser>();


//����Quartz
builder.Services.AddQuartz(Q =>
{
    // ʹ��Microsoft����ע����Ϊ��ҵ����  
    Q.UseMicrosoftDependencyInjectionJobFactory();

    // �����ҵ  
    Q.AddJob<MyJob>(opts => opts
        .WithIdentity("myJob", "group1")
        .StoreDurably());

    // ��Ӵ�����  
    Q.AddTrigger(opts => opts
        .ForJob("myJob", "group1")
        .WithIdentity("myTrigger", "group1")
        .StartNow() // ������ʼ  
        .WithSimpleSchedule(x => x
            .WithIntervalInMinutes(1) // ÿ1����ִ��һ��  
            .RepeatForever())); // �ظ�ִ��  
});
builder.Services.AddQuartzHostedService(options =>
{
    // ����Ϊ�ȴ�������ҵ��ɺ�Źر�Ӧ�ó��򣨿�ѡ��  
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
