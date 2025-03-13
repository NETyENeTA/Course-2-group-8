using System;
using System.Collections.Generic;
using Ale.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale.Contexts;

public partial class DateBaseContext : DbContext
{
    public DateBaseContext()
    {
    }

    public DateBaseContext(DbContextOptions<DateBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=E:\\C#\\DBeaver\\BooksMaker");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasIndex(e => new { e.Title, e.Description, e.Author, e.CreatedAt }, "BooksIndexes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Connections).HasForeignKey(d => d.IdBook);

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Connections).HasForeignKey(d => d.IdUser);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasIndex(e => e.Name, "TagsIndexes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdBook).HasColumnName("id_book");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Tags).HasForeignKey(d => d.IdBook);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Name, "UsersIndexes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
