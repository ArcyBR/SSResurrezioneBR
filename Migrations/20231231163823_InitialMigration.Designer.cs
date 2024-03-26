﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSResurrezioneBR.Models.Services.Infrastructure;

#nullable disable

namespace SSResurrezioneBR.Migrations
{
    [DbContext(typeof(SSResurrezioneDbContext))]
    [Migration("20231231163823_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.Almanacco", b =>
                {
                    b.Property<long>("IdAlmanacco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id_Almanacco");

                    b.Property<string>("CreatoreEventoAlmanacco")
                        .HasColumnType("TEXT")
                        .HasColumnName("Creatore_Evento_Almanacco");

                    b.Property<string>("DataEventoAlmanacco")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Data_Evento_Almanacco");

                    b.Property<string>("DescrizioneAlmanacco")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Descrizione_Almanacco");

                    b.Property<int?>("ImageVisibileAlmanacco")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Image_Visible_Almanacco");

                    b.Property<string>("RowVersion")
                        .HasColumnType("TEXT")
                        .HasColumnName("RowVersion");

                    b.Property<string>("TitoloAlmanacco")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Titolo_Almanacco");

                    b.HasKey("IdAlmanacco");

                    b.HasIndex(new[] { "IdAlmanacco" }, "IX_Almanacco_Id_Almanacco")
                        .IsUnique();

                    b.ToTable("Almanacco", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.AlmanaccoFoto", b =>
                {
                    b.Property<long>("IdAlmanaccoFoto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id_Almanacco_Foto");

                    b.Property<long>("IdAlmanacco")
                        .HasColumnType("INTEGER")
                        .HasColumnName("id_Almanacco");

                    b.Property<string>("PathFotoAlmanaccoFoto")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Path_Foto_Almanacco_Foto");

                    b.HasKey("IdAlmanaccoFoto");

                    b.HasIndex("IdAlmanacco");

                    b.HasIndex(new[] { "IdAlmanaccoFoto" }, "IX_Almanacco_Foto_Id_Almanacco_Foto")
                        .IsUnique();

                    b.ToTable("Almanacco_Foto", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.CoroPolifonicoMaterMisericordie", b =>
                {
                    b.Property<long>("IdCoroPolifonicoMaterMisericordie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id_CoroPolifonicoMaterMisericordie");

                    b.Property<string>("CreatoreEventoCoroPolifonicoMaterMisericordie")
                        .HasColumnType("TEXT")
                        .HasColumnName("Creatore_Evento_CoroPolifonicoMaterMisericordie");

                    b.Property<string>("DataEventoCoroPolifonicoMaterMisericordie")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Data_Evento_CoroPolifonicoMaterMisericordie");

                    b.Property<string>("DescrizioneEventoCoroPolifonicoMaterMisericordie")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Descrizione_CoroPolifonicoMaterMisericordie");

                    b.Property<int?>("ImageVisibileCoroPolifonicoMaterMisericordie")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Image_Visible_CoroPolifonicoMaterMisericordie");

                    b.Property<string>("RowVersion")
                        .HasColumnType("TEXT")
                        .HasColumnName("RowVersion");

                    b.Property<string>("TitoloCoroPolifonicoMaterMisericordie")
                        .HasColumnType("TEXT")
                        .HasColumnName("Titolo_CoroPolifonicoMaterMisericordie");

                    b.HasKey("IdCoroPolifonicoMaterMisericordie");

                    b.HasIndex(new[] { "IdCoroPolifonicoMaterMisericordie" }, "IX_CoroPolifonicoMaterMisericordie_Id_CoroPolifonicoMaterMisericordie")
                        .IsUnique();

                    b.ToTable("CoroPolifonicoMaterMisericordie", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.CoroPolifonicoMaterMisericordieFoto", b =>
                {
                    b.Property<long>("IdCoroPolifonicoMaterMisericordieFoto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id_CoroPolifonicoMaterMisericordie_Foto");

                    b.Property<long>("IdCoroPolifonicoMaterMisericordie")
                        .HasColumnType("INTEGER")
                        .HasColumnName("id_CoroPolifonicoMaterMisericordie");

                    b.Property<string>("PathFotoCoroPolifonicoMaterMisericordieFoto")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Path_Foto_CoroPolifonicoMaterMisericordie_Foto");

                    b.HasKey("IdCoroPolifonicoMaterMisericordieFoto");

                    b.HasIndex("IdCoroPolifonicoMaterMisericordie");

                    b.HasIndex(new[] { "IdCoroPolifonicoMaterMisericordieFoto" }, "IX_CoroPolifonicoMaterMisericordie_Foto_Id_CoroPolifonicoMaterMisericordie_Foto")
                        .IsUnique();

                    b.ToTable("CoroPolifonicoMaterMisericordie_Foto", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.ImgForCarousel", b =>
                {
                    b.Property<long>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Image_Id");

                    b.Property<string>("ImageContentDescription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Image_Content_Description");

                    b.Property<string>("ImageContentTitle")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Image_Content_Title");

                    b.Property<string>("ImageLabel")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Image_Label");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Image_Path");

                    b.Property<long>("ImageVisible")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Image_Visible");

                    b.HasKey("ImageId");

                    b.ToTable("Img_For_Carousel", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.Incarichi", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cap")
                        .HasColumnType("TEXT")
                        .HasColumnName("CAP");

                    b.Property<string>("Città")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Incarico")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Indirizzo")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkDiocesi")
                        .HasColumnType("TEXT")
                        .HasColumnName("Link_Diocesi");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroCivico")
                        .HasColumnType("TEXT")
                        .HasColumnName("Numero_Civico");

                    b.Property<string>("TelefonoCellulare")
                        .HasColumnType("TEXT")
                        .HasColumnName("Telefono_Cellulare");

                    b.Property<string>("TelefonoFisso")
                        .HasColumnType("TEXT")
                        .HasColumnName("Telefono_Fisso");

                    b.Property<string>("Titolo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Cognome" }, "CognomeIdx");

                    b.ToTable("Incarichi", (string)null);
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.AlmanaccoFoto", b =>
                {
                    b.HasOne("SSResurrezioneBR.Models.Entities.Almanacco", "IdAlmanaccoNavigation")
                        .WithMany("AlmanaccoFotos")
                        .HasForeignKey("IdAlmanacco")
                        .IsRequired();

                    b.Navigation("IdAlmanaccoNavigation");
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.CoroPolifonicoMaterMisericordieFoto", b =>
                {
                    b.HasOne("SSResurrezioneBR.Models.Entities.CoroPolifonicoMaterMisericordie", "IdCoroPolifonicoMaterMisericordieNavigation")
                        .WithMany("CoroPolifonicoMaterMisericordieFotos")
                        .HasForeignKey("IdCoroPolifonicoMaterMisericordie")
                        .IsRequired();

                    b.Navigation("IdCoroPolifonicoMaterMisericordieNavigation");
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.Almanacco", b =>
                {
                    b.Navigation("AlmanaccoFotos");
                });

            modelBuilder.Entity("SSResurrezioneBR.Models.Entities.CoroPolifonicoMaterMisericordie", b =>
                {
                    b.Navigation("CoroPolifonicoMaterMisericordieFotos");
                });
#pragma warning restore 612, 618
        }
    }
}
