﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Translate.Infrastructure.Data;

#nullable disable

namespace Translate.Infrastructure.Migrations
{
    [DbContext(typeof(TranslateContext))]
    [Migration("20240315004112_RefactorFrase")]
    partial class RefactorFrase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Translate.Domain.Entities.Frase", b =>
                {
                    b.Property<Guid>("FraseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FraseOriginal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FraseTraduzida")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("IdiomaOriginal")
                        .HasColumnType("int");

                    b.Property<int>("IdiomaTraduzido")
                        .HasColumnType("int");

                    b.Property<int>("QtdCaracteresFraseOriginal")
                        .HasColumnType("int");

                    b.Property<int>("QtdCaracteresFraseTraduzida")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("FraseId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Frases");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Log", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Endpoint")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Parametros")
                        .HasColumnType("longtext");

                    b.Property<int>("StatusResposta")
                        .HasColumnType("int");

                    b.Property<string>("TipoRequisicao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("LogId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLatest")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeUsuarioSistema")
                        .IsRequired()
                        .HasColumnType("varchar(95)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email");

                    b.HasIndex("NomeUsuarioSistema");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Translate.Domain.Entities.UsuarioRole", b =>
                {
                    b.Property<Guid>("UsuarioRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("UsuarioRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuariosRoles");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Frase", b =>
                {
                    b.HasOne("Translate.Domain.Entities.Usuario", "Usuarios")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Log", b =>
                {
                    b.HasOne("Translate.Domain.Entities.Usuario", "Usuarios")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Translate.Domain.Entities.UsuarioRole", b =>
                {
                    b.HasOne("Translate.Domain.Entities.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Translate.Domain.Entities.Usuario", "Usuarios")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Translate.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("UsuarioRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
