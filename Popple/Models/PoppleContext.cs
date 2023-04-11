using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Popple.Models;

public partial class PoppleContext : DbContext
{
    public PoppleContext()
    {
    }

    public PoppleContext(DbContextOptions<PoppleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=poppledb.cyyjwudya0vk.us-east-2.rds.amazonaws.com;database=popple;uid=admin;pwd=PoppleDropple!1;port=3306", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.Property(e => e.AccountId).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Comic>(entity =>
        {
            entity.HasKey(e => e.ComicsId).HasName("PRIMARY");

            entity.HasIndex(e => e.CreatorId, "fk_Comics_1_idx");

            entity.Property(e => e.ComicsId).ValueGeneratedNever();
            entity.Property(e => e.CreatorId).HasColumnName("creatorId");
            entity.Property(e => e.Description)
                .HasMaxLength(750)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.PostDate)
                .HasColumnType("datetime")
                .HasColumnName("postDate");

            entity.HasOne(d => d.Creator).WithMany(p => p.Comics)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Comics_1");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.AccountId, "fk_table1_Accounts1_idx");

            entity.HasIndex(e => e.ComicsId, "fk_table1_Comics1_idx");

            entity.HasOne(d => d.Account).WithMany()
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_table1_Accounts1");

            entity.HasOne(d => d.Comics).WithMany()
                .HasForeignKey(d => d.ComicsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_table1_Comics1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
