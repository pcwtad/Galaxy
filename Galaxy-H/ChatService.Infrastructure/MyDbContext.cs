﻿using ChatService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Infrastructure
{
    public class MyDbContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> opt) : base(opt)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//连接数据库的配置，我这里连接的SQLServer数据库
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=.;Database=cs;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=true;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //this.GetType().Assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
