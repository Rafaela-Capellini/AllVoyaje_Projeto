﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetoAllVoyaje.Data;

#nullable disable

namespace ProjetoAllVoyaje.Migrations
{
    [DbContext(typeof(AllVoyajeDbContext))]
    partial class AllVoyajeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjetoAllVoyaje.Models.Cliente", b =>
                {
                    b.Property<Guid>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("tbClientes", (string)null);
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.ImagemPacote", b =>
                {
                    b.Property<Guid>("ImagemPacoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PacoteViagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImagemPacoteId");

                    b.HasIndex("PacoteViagemId");

                    b.ToTable("tbImagemPacote", (string)null);
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.PacoteCliente", b =>
                {
                    b.Property<Guid>("PacoteClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PacoteViagemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QtdPessoas")
                        .HasColumnType("int");

                    b.HasKey("PacoteClienteId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PacoteViagemId");

                    b.ToTable("tbPacoteClientes", (string)null);
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.PacoteViagem", b =>
                {
                    b.Property<Guid>("PacoteViagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly>("DataRetorno")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataSaida")
                        .HasColumnType("date");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hotel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomePacote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("TipoPacoteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PacoteViagemId");

                    b.HasIndex("TipoPacoteId");

                    b.ToTable("tbPacoteViagens", (string)null);
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.TipoPacote", b =>
                {
                    b.Property<Guid>("TipoPacoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoPacoteId");

                    b.ToTable("tbTiposPacotes", (string)null);
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.ImagemPacote", b =>
                {
                    b.HasOne("ProjetoAllVoyaje.Models.PacoteViagem", "PacoteViagem")
                        .WithMany()
                        .HasForeignKey("PacoteViagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PacoteViagem");
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.PacoteCliente", b =>
                {
                    b.HasOne("ProjetoAllVoyaje.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjetoAllVoyaje.Models.PacoteViagem", "Pacote")
                        .WithMany()
                        .HasForeignKey("PacoteViagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Pacote");
                });

            modelBuilder.Entity("ProjetoAllVoyaje.Models.PacoteViagem", b =>
                {
                    b.HasOne("ProjetoAllVoyaje.Models.TipoPacote", "tipopacote")
                        .WithMany()
                        .HasForeignKey("TipoPacoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tipopacote");
                });
#pragma warning restore 612, 618
        }
    }
}
