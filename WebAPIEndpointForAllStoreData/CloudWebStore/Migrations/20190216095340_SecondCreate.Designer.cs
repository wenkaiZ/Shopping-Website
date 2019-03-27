﻿// <auto-generated />
using System;
using CloudWebStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CloudWebStore.Migrations
{
    [DbContext(typeof(MvcProductContext))]
    [Migration("20190216095340_SecondCreate")]
    partial class SecondCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("CloudWebStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Price");

                    b.Property<string>("ProductName");

                    b.Property<DateTime>("ReleaseDate");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CloudWebStore.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProductName");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCartItem");
                });
#pragma warning restore 612, 618
        }
    }
}
