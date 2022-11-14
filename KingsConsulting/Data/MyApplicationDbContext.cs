using System;
using System.Collections.Generic;
using KingsConsulting.Entities;
using Microsoft.EntityFrameworkCore;

namespace KingsConsulting.Data;

public partial class MyApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MyApplicationDbContext()
    {
    }

    public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<OrderContent> OrderContents { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderContent>(entity =>
        {
            entity.HasKey(e => e.OrderContentsId).HasName("orderCPKID");

            entity.Property(e => e.OrderContentsId).HasColumnName("orderContentsId");
            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.Quanitity).HasColumnName("quanitity");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkOC_OID");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderContents)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkOC_SID");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orderPKID");

            entity.ToTable("OrderInfo");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("orderDate");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.OrderInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkOI_UID");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasKey(e => e.ServiceTypeId).HasName("serviceID");

            entity.ToTable("ServiceType");

            entity.Property(e => e.ServiceTypeId).HasColumnName("serviceTypeID");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serviceName");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pkUID");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
               .HasMaxLength(100)
               .IsUnicode(false)
               .HasColumnName("firstName");
            entity.Property(e => e.LastName)
               .HasMaxLength(100)
               .IsUnicode(false)
               .HasColumnName("lastName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
