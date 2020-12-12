﻿// <auto-generated />
using System;
using App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("App.Models.Designacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AjudanteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataDeRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DesignadoId")
                        .HasColumnType("int");

                    b.Property<string>("Observacao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AjudanteId");

                    b.HasIndex("DesignadoId");

                    b.ToTable("Designacoes");
                });

            modelBuilder.Entity("App.Models.Publicador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Publicadores");
                });

            modelBuilder.Entity("App.Models.Designacao", b =>
                {
                    b.HasOne("App.Models.Publicador", "Ajudante")
                        .WithMany()
                        .HasForeignKey("AjudanteId");

                    b.HasOne("App.Models.Publicador", "Designado")
                        .WithMany()
                        .HasForeignKey("DesignadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ajudante");

                    b.Navigation("Designado");
                });
#pragma warning restore 612, 618
        }
    }
}
