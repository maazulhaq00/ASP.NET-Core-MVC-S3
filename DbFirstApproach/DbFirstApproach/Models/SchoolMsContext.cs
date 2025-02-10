using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirstApproach.Models;

public partial class SchoolMsContext : DbContext
{
    public SchoolMsContext()
    {
    }

    public SchoolMsContext(DbContextOptions<SchoolMsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.; Initial Catalog=SchoolMS; User Id=sa; Password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B993D03CC7E");

            entity.Property(e => e.StudentEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
