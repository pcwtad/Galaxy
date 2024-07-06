﻿// <auto-generated />
using System;
using FollowService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FollowService.Infrastructure.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240612035930_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FollowService.Domain.Entities.Follow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Beingfollowed")
                        .HasColumnType("int");

                    b.Property<DateTime>("FollowTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Followers")
                        .HasColumnType("int");

                    b.Property<int>("IsFollowing")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("T_Follows", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
