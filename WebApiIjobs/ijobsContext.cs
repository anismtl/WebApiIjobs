using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiIjobs
{
    public partial class ijobsContext : DbContext
    {
        public ijobsContext()
        {
        }

        public ijobsContext(DbContextOptions<ijobsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidat> Candidat { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Evenement> Evenement { get; set; }
        public virtual DbSet<Favoris> Favoris { get; set; }
        public virtual DbSet<Offre> Offre { get; set; }
        public virtual DbSet<Rappel> Rappel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=aniss.ca;Database=ijobs;User ID=yuni;Password=yuni2019");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Candidat>(entity =>
            {
                entity.HasKey(e => e.IdCandidat)
                    .HasName("CANDIDAT_ID_PK");

                entity.ToTable("CANDIDAT");

                entity.HasIndex(e => e.Courriel)
                    .HasName("CANDIDAT_COURRIEL_UN")
                    .IsUnique();

                entity.HasIndex(e => e.Tel)
                    .HasName("CANDIDAT_TEL_UN")
                    .IsUnique();

                entity.Property(e => e.IdCandidat).HasColumnName("ID_CANDIDAT");

                entity.Property(e => e.Courriel)
                    .IsRequired()
                    .HasColumnName("COURRIEL")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.MotPasse)
                    .IsRequired()
                    .HasColumnName("MOT_PASSE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NomCandidat)
                    .IsRequired()
                    .HasColumnName("NOM_CANDIDAT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomCandidat)
                    .IsRequired()
                    .HasColumnName("PRENOM_CANDIDAT")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Statut)
                    .IsRequired()
                    .HasColumnName("STATUT")
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnName("TEL")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.IdContact)
                    .HasName("CONTACT_ID_PK");

                entity.ToTable("CONTACT");

                entity.Property(e => e.IdContact).HasColumnName("ID_CONTACT");

                entity.Property(e => e.CourrielContact)
                    .HasColumnName("COURRIEL_CONTACT")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidat).HasColumnName("ID_CANDIDAT");

                entity.Property(e => e.NomContact)
                    .HasColumnName("NOM_CONTACT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Poste)
                    .HasColumnName("POSTE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomContact)
                    .HasColumnName("PRENOM_CONTACT")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TelContact)
                    .HasColumnName("TEL_CONTACT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCandidatNavigation)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.IdCandidat)
                    .HasConstraintName("CONTACT_ID_CONDIDAT_FK");
            });

            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.IdEvenement)
                    .HasName("EVENEMENT_ID_PK");

                entity.ToTable("EVENEMENT");

                entity.Property(e => e.IdEvenement).HasColumnName("ID_EVENEMENT");

                entity.Property(e => e.Adresse)
                    .HasColumnName("ADRESSE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateEvent)
                    .HasColumnName("DATE_EVENT")
                    .HasColumnType("date");

                entity.Property(e => e.Descr)
                    .HasColumnName("DESCR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Heure)
                    .IsRequired()
                    .HasColumnName("HEURE")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidat).HasColumnName("ID_CANDIDAT");

                entity.Property(e => e.IdContact).HasColumnName("ID_CONTACT");

                entity.Property(e => e.IdOffre)
                    .HasColumnName("ID_OFFRE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasColumnName("TITRE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdContactNavigation)
                    .WithMany(p => p.Evenement)
                    .HasForeignKey(d => d.IdContact)
                    .HasConstraintName("EVENEMENT_ID_CONTACT_FK");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Evenement)
                    .HasForeignKey(d => new { d.IdCandidat, d.IdOffre })
                    .HasConstraintName("EVENEMENT_ID_CONDIDAT_OFFRE_FK");
            });

            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.IdCandidat, e.IdOffre })
                    .HasName("FAVORIS_ID_PK");

                entity.ToTable("FAVORIS");

                entity.Property(e => e.IdCandidat).HasColumnName("ID_CANDIDAT");

                entity.Property(e => e.IdOffre)
                    .HasColumnName("ID_OFFRE")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DateFavoris)
                    .HasColumnName("DATE_FAVORIS")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Postule)
                    .HasColumnName("POSTULE")
                    .HasColumnType("numeric(1, 0)");

                entity.HasOne(d => d.IdCandidatNavigation)
                    .WithMany(p => p.Favoris)
                    .HasForeignKey(d => d.IdCandidat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FAVORIS_ID_CONDIDAT_FK");

                entity.HasOne(d => d.IdOffreNavigation)
                    .WithMany(p => p.Favoris)
                    .HasForeignKey(d => d.IdOffre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FAVORIS_ID_OFFRE_FK");
            });

            modelBuilder.Entity<Offre>(entity =>
            {
                entity.HasKey(e => e.IdOffre)
                    .HasName("OFFRE_ID_PK");

                entity.ToTable("OFFRE");

                entity.Property(e => e.IdOffre)
                    .HasColumnName("ID_OFFRE")
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Companie)
                    .HasColumnName("COMPANIE")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DateOffre)
                    .HasColumnName("DATE_OFFRE")
                    .HasColumnType("date");

                entity.Property(e => e.Descr)
                    .HasColumnName("DESCR")
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Titre)
                    .HasColumnName("TITRE")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rappel>(entity =>
            {
                entity.HasKey(e => e.IdRappel)
                    .HasName("RAPPEL_ID_PK");

                entity.ToTable("RAPPEL");

                entity.Property(e => e.IdRappel).HasColumnName("ID_RAPPEL");

                entity.Property(e => e.CourrielRappel)
                    .HasColumnName("COURRIEL_RAPPEL")
                    .HasColumnType("numeric(1, 0)");

                entity.Property(e => e.DateRappel)
                    .HasColumnName("DATE_RAPPEL")
                    .HasColumnType("date");

                entity.Property(e => e.HeureRappel)
                    .IsRequired()
                    .HasColumnName("HEURE_RAPPEL")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvenement).HasColumnName("ID_EVENEMENT");

                entity.Property(e => e.TelRappel)
                    .HasColumnName("TEL_RAPPEL")
                    .HasColumnType("numeric(1, 0)");

                entity.HasOne(d => d.IdEvenementNavigation)
                    .WithMany(p => p.Rappel)
                    .HasForeignKey(d => d.IdEvenement)
                    .HasConstraintName("RAPPEL_ID_EVENEMENT_FK");
            });
        }
    }
}
