﻿using System;
using System.Collections.Generic;
using Hope.EntityComponent.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace Hope.EntityComponent;

public partial class HopePharmacyManagementContext : DbContext
{
    public HopePharmacyManagementContext()
    {
    }

    public HopePharmacyManagementContext(DbContextOptions<HopePharmacyManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssignUsersToRole> AssignUsersToRoles { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<InvoiceMaster> InvoiceMasters { get; set; }

    public virtual DbSet<JobDescription> JobDescriptions { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineDepartment> MedicineDepartments { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleRole> ModuleRoles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AMH\\AMH;Database=HopePharmacyManagement;Trusted_Connection=True;TrustServerCertificate=True;User Id=sa;password=123456789;Integrated Security=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignUsersToRole>(entity =>
        {
            entity.HasKey(e => e.AssignUserToRoleId);

            entity.ToTable("AssignUsersToRole");

            entity.HasOne(d => d.Role).WithMany(p => p.AssignUsersToRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignUsersToRole_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.AssignUsersToRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssignUsersToRole_Users");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId);

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorException)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ErrorMessage)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ErrorLog_Users");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailsId);

            entity.Property(e => e.Qty).HasColumnName("QTY");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.InvoiceMaster).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_InvoiceMaster");

            entity.HasOne(d => d.Medicine).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDetails_Medicines");
        });

        modelBuilder.Entity<InvoiceMaster>(entity =>
        {
            entity.ToTable("InvoiceMaster");

            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TotalCostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSellingPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<JobDescription>(entity =>
        {
            entity.ToTable("JobDescription");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.MedicineName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.MedicineDepartment).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.MedicineDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicines_MedicineDepartment");
        });

        modelBuilder.Entity<MedicineDepartment>(entity =>
        {
            entity.ToTable("MedicineDepartment");

            entity.Property(e => e.DepartmentName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.ModuleId).ValueGeneratedNever();
            entity.Property(e => e.ModuleName).HasMaxLength(50);
        });

        modelBuilder.Entity<ModuleRole>(entity =>
        {
            entity.ToTable("ModuleRole");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleRoles)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleRole_Modules");

            entity.HasOne(d => d.Role).WithMany(p => p.ModuleRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleRole_Roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.MaxDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrginalQty).HasColumnName("OrginalQTY");
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");
            entity.Property(e => e.RemaningQty).HasColumnName("RemaningQTY");
            entity.Property(e => e.SellingPriceAfterTax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SellingPriceBeforeTax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxValue).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Stores)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stores_Medicines");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Stores)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stores_Suppliers");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.Mobile)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.SupplierName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.SupplingArea).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ResignationDate).HasColumnType("datetime");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.JobDescription).WithMany(p => p.Users)
                .HasForeignKey(d => d.JobDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_JobDescription");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
