using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EcommLogin.Models;

public partial class EcommContext : DbContext
{
    public EcommContext()
    {
    }

    public EcommContext(DbContextOptions<EcommContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.; Initial Catalog=Ecomm; User Id=sa; Password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CAD87D7AC");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF822CFA457").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456329345DA").IsUnique();

            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('noimage')");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
