using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public partial class HeavensPlaceContext : DbContext
{
    public HeavensPlaceContext()
    {
    }

    public HeavensPlaceContext(DbContextOptions<HeavensPlaceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<DependentBook> DependentBooks { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceOrder> InvoiceOrders { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Product> Products { get; set; }

     

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_book_1");

            entity.ToTable("book");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .HasColumnName("book_name");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.ToTable("dependent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DependentBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_book");

            entity.ToTable("dependent_book");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.DependentId).HasColumnName("dependent_id");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceD__3213E83FE21965CA");

            entity.ToTable("InvoiceDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.GrandTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("grand_total");
            entity.Property(e => e.InvoiceDate)
                .HasColumnType("date")
                .HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .HasColumnName("invoice_number");
        });

        modelBuilder.Entity<InvoiceOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceO__3213E83F6D2E78E1");

            entity.ToTable("InvoiceOrder");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.LineTotal)
                .HasComputedColumnSql("([quantity]*[price])", false)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("line_total");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceOrders)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_InvoiceOrder_InvoiceDetail");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceOrders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_InvoiceOrder_Products1");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("member");

            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__member__A9D105342BF05F03").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PasswordSalt).HasMaxLength(256);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3213E83FC5DE18DF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
