using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BotashVMS.Models;

public partial class BotashPortalContext : DbContext
{
    public BotashPortalContext()
    {
    }

    public BotashPortalContext(DbContextOptions<BotashPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<Compliance> Compliances { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<FinancialInfo> FinancialInfos { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Ownership> Ownerships { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attachme__3214EC272FFAF6F3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.VendorName).HasMaxLength(250);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Attachmen__Vendo__693CA210");
        });

        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankDeta__3214EC277317B6E7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountHolderName).HasMaxLength(250);
            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.BankBrachAddress).HasMaxLength(250);
            entity.Property(e => e.BankName).HasMaxLength(250);
            entity.Property(e => e.BranchCode).HasMaxLength(50);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.SwiftCode).HasMaxLength(250);
            entity.Property(e => e.VendorName).HasMaxLength(250);

            entity.HasOne(d => d.Process).WithMany(p => p.BankDetails)
                .HasForeignKey(d => d.ProcessId)
                .HasConstraintName("FK__BankDetai__Proce__5441852A");
        });

        modelBuilder.Entity<Compliance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Complian__3214EC274DC16366");

            entity.ToTable("Compliance");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bobs)
                .HasMaxLength(150)
                .HasColumnName("BOBS");
            entity.Property(e => e.CertificateType).HasMaxLength(150);
            entity.Property(e => e.CorrectiveActionPlan).HasMaxLength(50);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EnvironmentStandards).HasMaxLength(50);
            entity.Property(e => e.FirstAidTraining).HasMaxLength(50);
            entity.Property(e => e.FitforDutyPolicy).HasMaxLength(50);
            entity.Property(e => e.HazardousMaterialsPlan).HasMaxLength(50);
            entity.Property(e => e.HealthCoordinator).HasMaxLength(50);
            entity.Property(e => e.HearingConservation).HasMaxLength(50);
            entity.Property(e => e.Hivaidspolicy)
                .HasMaxLength(50)
                .HasColumnName("HIVAIDSPolicy");
            entity.Property(e => e.IncidentInvestigationProcedure).HasMaxLength(50);
            entity.Property(e => e.InspectionandMaintenanceRecords).HasMaxLength(50);
            entity.Property(e => e.Isocertified)
                .HasMaxLength(150)
                .HasColumnName("ISOCertified");
            entity.Property(e => e.MaterialsSafety).HasMaxLength(50);
            entity.Property(e => e.OperationPolicies).HasMaxLength(50);
            entity.Property(e => e.OrientationProgramme).HasMaxLength(50);
            entity.Property(e => e.OtherCertificate).HasMaxLength(250);
            entity.Property(e => e.ProductWarranty).HasMaxLength(50);
            entity.Property(e => e.QualityControl).HasMaxLength(50);
            entity.Property(e => e.QualityStandards).HasMaxLength(50);
            entity.Property(e => e.RespirationProtection).HasMaxLength(50);
            entity.Property(e => e.RiskAssessmentPlan).HasMaxLength(50);
            entity.Property(e => e.SiteSpecificSafetyPlan).HasMaxLength(50);
            entity.Property(e => e.StoragePolicy).HasMaxLength(50);
            entity.Property(e => e.TrainingProgramme).HasMaxLength(50);
            entity.Property(e => e.UseofPpe)
                .HasMaxLength(50)
                .HasColumnName("UseofPPE");
            entity.Property(e => e.VendorId).HasColumnName("VendorID");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Compliances)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Complianc__Vendo__571DF1D5");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC27FC161A5D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FinanceCellphoneNumber).HasMaxLength(50);
            entity.Property(e => e.FinanceContactName).HasMaxLength(250);
            entity.Property(e => e.FinanceEmailAddress).HasMaxLength(250);
            entity.Property(e => e.FinanceTelephoneNumber).HasMaxLength(20);
            entity.Property(e => e.SalesCellphoneNumber).HasMaxLength(50);
            entity.Property(e => e.SalesContactName).HasMaxLength(250);
            entity.Property(e => e.SalesEmailAddress).HasMaxLength(250);
            entity.Property(e => e.SalesTelephoneNumber).HasMaxLength(20);
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.WebAddress).HasMaxLength(250);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacts__Vendor__59FA5E80");
        });

        modelBuilder.Entity<FinancialInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Financia__3214EC27E24093AB");

            entity.ToTable("FinancialInfo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NatureOfBusiness).HasMaxLength(350);
            entity.Property(e => e.NatureOfProducts).HasMaxLength(350);
            entity.Property(e => e.OffsetAccounts).HasMaxLength(50);
            entity.Property(e => e.SettlementTerms).HasMaxLength(50);
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.VendorName).HasMaxLength(50);

            entity.HasOne(d => d.Vendor).WithMany(p => p.FinancialInfos)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Financial__Vendo__60A75C0F");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC071728A88D");

            entity.Property(e => e.DateSent).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Vendo__17036CC0");
        });

        modelBuilder.Entity<Ownership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ownershi__3214EC27C97095F5");

            entity.ToTable("Ownership");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Country).HasMaxLength(250);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(50)
                .HasColumnName("IDNumber");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Nationality).HasMaxLength(250);
            entity.Property(e => e.OwnershipType).HasMaxLength(150);
            entity.Property(e => e.Position).HasMaxLength(250);
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.SharePercentage).HasMaxLength(10);
            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.VendorName).HasMaxLength(300);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Ownerships)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ownership__Vendo__66603565");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Referenc__3214EC275079A909");

            entity.ToTable("Reference");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApproximationValue).HasMaxLength(20);
            entity.Property(e => e.ContactDetails).HasMaxLength(20);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasMaxLength(20);
            entity.Property(e => e.GoodsOrServices).HasMaxLength(20);
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.ProjectLocation).HasMaxLength(20);
            entity.Property(e => e.ProjectName).HasMaxLength(20);
            entity.Property(e => e.Referee).HasMaxLength(20);
            entity.Property(e => e.StartDate).HasMaxLength(20);

            entity.HasOne(d => d.Process).WithMany(p => p.References)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reference__Proce__6383C8BA");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.ProcessId);

            entity.ToTable("Vendor");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.CompanyType).HasMaxLength(250);
            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.ContractType).HasMaxLength(150);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.District).HasMaxLength(250);
            entity.Property(e => e.FaxNumber).HasMaxLength(50);
            entity.Property(e => e.IsFcapproved)
                .HasDefaultValue(0)
                .HasColumnName("IsFCApproved");
            entity.Property(e => e.ParentCompanyAddress).HasMaxLength(250);
            entity.Property(e => e.PhysicalAddress).HasMaxLength(250);
            entity.Property(e => e.PostalAddress).HasMaxLength(250);
            entity.Property(e => e.ProfileStatus)
                .HasMaxLength(20)
                .HasDefaultValue("INCOMPLETE");
            entity.Property(e => e.RegistrationNumber).HasMaxLength(50);
            entity.Property(e => e.SeparateHqaddress)
                .HasMaxLength(250)
                .HasColumnName("SeparateHQAddress");
            entity.Property(e => e.SysproId)
                .HasMaxLength(10)
                .HasColumnName("SysproID");
            entity.Property(e => e.Town).HasMaxLength(250);
            entity.Property(e => e.TradingAs).HasMaxLength(20);
            entity.Property(e => e.VatNumber).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
