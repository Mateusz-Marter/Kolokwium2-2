using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context;

public partial class KolosContext : DbContext
{
    public KolosContext()
    {
    }

    public KolosContext(DbContextOptions<KolosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commitment> Commitments { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=kolos;User Id=sa;Password=mypassword1!;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commitment>(entity =>
        {
            entity.HasKey(e => e.IdCommitment).HasName("Commitment_pk");

            entity.ToTable("Commitment");

            entity.Property(e => e.PaymentDeadline).HasColumnType("datetime");

            entity.HasOne(d => d.IdSubscriptionNavigation).WithMany(p => p.Commitments)
                .HasForeignKey(d => d.IdSubscription)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Commitment_Subscription");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.IdProvider).HasName("Provider_pk");

            entity.ToTable("Provider");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("Service_pk");

            entity.ToTable("Service");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProviderNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Service_Provider");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.IdSubscription).HasName("Subscription_pk");

            entity.ToTable("Subscription");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.IdService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Subscription_Service");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Subscription_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
