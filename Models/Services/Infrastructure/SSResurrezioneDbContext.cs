using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SSResurrezioneBR.Models.Entities;

namespace SSResurrezioneBR.Models.Services.Infrastructure;

public partial class SSResurrezioneDbContext : DbContext
{
    public SSResurrezioneDbContext()
    {
    }

    public SSResurrezioneDbContext(DbContextOptions<SSResurrezioneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almanacco> Almanaccos { get; set; }

    public virtual DbSet<AlmanaccoFoto> AlmanaccoFotos { get; set; }
    public virtual DbSet<CoroPolifonicoMaterMisericordie> CoroPolifonicoMaterMisericordies { get; set; }

    public virtual DbSet<CoroPolifonicoMaterMisericordieFoto> CoroPolifonicoMaterMisericordieFotos { get; set; }

    public virtual DbSet<ImgForCarousel> ImgForCarousels { get; set; }

    public virtual DbSet<Incarichi> Incarichis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almanacco>(entity =>
        {
            entity.HasKey(e => e.IdAlmanacco);

            entity.ToTable("Almanacco");

            entity.HasIndex(e => e.IdAlmanacco, "IX_Almanacco_Id_Almanacco").IsUnique();

            entity.Property(e => e.IdAlmanacco).HasColumnName("Id_Almanacco");
            entity.Property(e => e.DescrizioneAlmanacco).HasColumnName("Descrizione_Almanacco");
            entity.Property(e => e.TitoloAlmanacco).HasColumnName("Titolo_Almanacco");
            entity.Property(e => e.DataEventoAlmanacco).HasColumnName("Data_Evento_Almanacco").HasConversion( v => v.ToString("dd/MM/yyyy"),v => DateTime.Parse(v));
            entity.Property(e => e.ImageVisibileAlmanacco).HasColumnName("Image_Visible_Almanacco");
            entity.Property(e => e.CreatoreEventoAlmanacco).HasColumnName("Creatore_Evento_Almanacco");
            entity.Property(e => e.RowVersion).HasColumnName("RowVersion");

            entity.HasMany(Almanacco => Almanacco.AlmanaccoFotos) 
                .WithOne(AlmanaccoFoto => AlmanaccoFoto.IdAlmanaccoNavigation)
                .HasForeignKey(AlmanaccoFoto => AlmanaccoFoto.IdAlmanacco)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AlmanaccoFoto>(entity =>
        {
            entity.HasKey(e => e.IdAlmanaccoFoto);

            entity.ToTable("Almanacco_Foto");

            entity.HasIndex(e => e.IdAlmanaccoFoto, "IX_Almanacco_Foto_Id_Almanacco_Foto").IsUnique();

            entity.Property(e => e.IdAlmanaccoFoto).HasColumnName("Id_Almanacco_Foto");
            entity.Property(e => e.IdAlmanacco).HasColumnName("id_Almanacco");
            entity.Property(e => e.PathFotoAlmanaccoFoto).HasColumnName("Path_Foto_Almanacco_Foto");

            /*
            entity.HasOne(d => d.IdAlmanaccoNavigation).WithMany(p => p.AlmanaccoFotos)
                .HasForeignKey(d => d.IdAlmanacco)
                .OnDelete(DeleteBehavior.ClientSetNull);
            */
        });
        modelBuilder.Entity<CoroPolifonicoMaterMisericordie>(entity =>
        {
            entity.HasKey(e => e.IdCoroPolifonicoMaterMisericordie);

            entity.ToTable("CoroPolifonicoMaterMisericordie");

            entity.HasIndex(e => e.IdCoroPolifonicoMaterMisericordie, "IX_CoroPolifonicoMaterMisericordie_Id_CoroPolifonicoMaterMisericordie").IsUnique();

            entity.Property(e => e.IdCoroPolifonicoMaterMisericordie).HasColumnName("Id_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.DescrizioneEventoCoroPolifonicoMaterMisericordie).HasColumnName("Descrizione_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.TitoloCoroPolifonicoMaterMisericordie).HasColumnName("Titolo_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.DataEventoCoroPolifonicoMaterMisericordie).HasColumnName("Data_Evento_CoroPolifonicoMaterMisericordie").HasConversion( v => v.ToString("dd/MM/yyyy"),v => DateTime.Parse(v));
            entity.Property(e => e.ImageVisibileCoroPolifonicoMaterMisericordie).HasColumnName("Image_Visible_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.CreatoreEventoCoroPolifonicoMaterMisericordie).HasColumnName("Creatore_Evento_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.RowVersion).HasColumnName("RowVersion");

            entity.HasMany(CoroPolifonicoMaterMisericordie => CoroPolifonicoMaterMisericordie.CoroPolifonicoMaterMisericordieFotos) 
                .WithOne(CoroPolifonicoMaterMisericordieFoto => CoroPolifonicoMaterMisericordieFoto.IdCoroPolifonicoMaterMisericordieNavigation)
                .HasForeignKey(CoroPolifonicoMaterMisericordieFoto => CoroPolifonicoMaterMisericordieFoto.IdCoroPolifonicoMaterMisericordie)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
        modelBuilder.Entity<CoroPolifonicoMaterMisericordieFoto>(entity =>
        {
            entity.HasKey(e => e.IdCoroPolifonicoMaterMisericordieFoto);

            entity.ToTable("CoroPolifonicoMaterMisericordie_Foto");

            entity.HasIndex(e => e.IdCoroPolifonicoMaterMisericordieFoto, "IX_CoroPolifonicoMaterMisericordie_Foto_Id_CoroPolifonicoMaterMisericordie_Foto").IsUnique();

            entity.Property(e => e.IdCoroPolifonicoMaterMisericordieFoto).HasColumnName("Id_CoroPolifonicoMaterMisericordie_Foto");
            entity.Property(e => e.IdCoroPolifonicoMaterMisericordie).HasColumnName("id_CoroPolifonicoMaterMisericordie");
            entity.Property(e => e.PathFotoCoroPolifonicoMaterMisericordieFoto).HasColumnName("Path_Foto_CoroPolifonicoMaterMisericordie_Foto");
        });

        modelBuilder.Entity<ImgForCarousel>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Img_For_Carousel");

            entity.Property(e => e.ImageId).HasColumnName("Image_Id");
            entity.Property(e => e.ImageContentDescription).HasColumnName("Image_Content_Description");
            entity.Property(e => e.ImageContentTitle).HasColumnName("Image_Content_Title");
            entity.Property(e => e.ImageLabel).HasColumnName("Image_Label");
            entity.Property(e => e.ImagePath).HasColumnName("Image_Path");
            entity.Property(e => e.ImageVisible).HasColumnName("Image_Visible");
        });

        modelBuilder.Entity<Incarichi>(entity =>
        {
            entity.ToTable("Incarichi");

            entity.HasIndex(e => e.Cognome, "CognomeIdx");

            entity.Property(e => e.Cap).HasColumnName("CAP");
            entity.Property(e => e.LinkDiocesi).HasColumnName("Link_Diocesi");
            entity.Property(e => e.NumeroCivico).HasColumnName("Numero_Civico");
            entity.Property(e => e.TelefonoCellulare).HasColumnName("Telefono_Cellulare");
            entity.Property(e => e.TelefonoFisso).HasColumnName("Telefono_Fisso");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
