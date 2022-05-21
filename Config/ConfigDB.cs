using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using notebook.Model;


namespace notebook.Config
{
    public partial class ConfigDB : DbContext
    {
        public ConfigDB()
        {
        }

        public ConfigDB(DbContextOptions<ConfigDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Record> Records { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Record>(entity =>
            {
                entity.ToTable("records");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('global_seq'::regclass)");

                entity.Property(e => e.dateTime).HasColumnName("date_time");

                entity.Property(e => e.description).HasColumnName("description");

                entity.Property(e => e.userId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("records_user_id_fkey").IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('global_seq'::regclass)");

                entity.Property(e => e.email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.enabled)
                    .IsRequired()
                    .HasColumnName("enabled")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.registered)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("registered")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey( ur => new { ur.userId});

                entity.ToTable("user_roles");

                entity.HasIndex(e => new { e.userId, e.role }, "user_roles_idx")
                    .IsUnique();

                entity.Property(e => e.role)
                    .HasColumnType("character varying")
                    .HasColumnName("role");

                entity.Property(e => e.userId).HasColumnName("user_id");

                entity.HasOne(d => d.user)
                    .WithMany()
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("user_roles_user_id_fkey").IsRequired();
            });

            modelBuilder.HasSequence("global_seq").StartsAt(100000);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}


