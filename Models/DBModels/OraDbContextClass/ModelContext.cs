using System;
using System.Collections.Generic;
using BISPAPIORA.Models.DBModels.Dbtables;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Models.DbModels.OraDbContextClass;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HiberProtectionAccount> HiberProtectionAccounts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseOracle("User Id=savings;Password=savings;Data Source=localhost:1521/savings;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("SAVINGS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<HiberProtectionAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIBER_PROTECTION_ACCOUNT");

            entity.Property(e => e.AccountNature)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ACCOUNT_NATURE");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ACCOUNT_TITLE");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Bank)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("BANK");
            entity.Property(e => e.BankAccess)
                .HasColumnType("NUMBER")
                .HasColumnName("BANK_ACCESS");
            entity.Property(e => e.BankAccount)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("BANK_ACCOUNT");
            entity.Property(e => e.BankOther)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("BANK_OTHER");
            entity.Property(e => e.Cnic)
                .HasColumnType("NUMBER")
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo)
                .HasColumnType("NUMBER")
                .HasColumnName("CONTACT_NO");
            entity.Property(e => e.District)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.EducationalStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EDUCATIONAL_STATUS");
            entity.Property(e => e.EmployementStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMPLOYEMENT_STATUS");
            entity.Property(e => e.EmployementStatusSpouse)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EMPLOYEMENT_STATUS_SPOUSE");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("GENDER");
            entity.Property(e => e.HasBank)
                .HasColumnType("NUMBER")
                .HasColumnName("HAS_BANK");
            entity.Property(e => e.InsertionTimestamp)
                .HasPrecision(6)
                .HasDefaultValueSql("sysdate")
                .HasColumnName("INSERTION_TIMESTAMP");
            entity.Property(e => e.IsValidBeneficiary)
                .HasColumnType("NUMBER")
                .HasColumnName("IS_VALID_BENEFICIARY");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MARITAL_STATUS");
            entity.Property(e => e.MobileNo)
                .HasColumnType("NUMBER")
                .HasColumnName("MOBILE_NO");
            entity.Property(e => e.MonthlyEarning)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MONTHLY_EARNING");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.OtherEmpStatusSpouse)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OTHER_EMP_STATUS_SPOUSE");
            entity.Property(e => e.OtherEmployementStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OTHER_EMPLOYEMENT_STATUS");
            entity.Property(e => e.Pmt)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PMT");
            entity.Property(e => e.Province)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PROVINCE");
            entity.Property(e => e.SpouseCnic)
                .HasColumnType("NUMBER")
                .HasColumnName("SPOUSE_CNIC");
            entity.Property(e => e.SpouseEducationalStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("SPOUSE_EDUCATIONAL_STATUS");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SPOUSE_NAME");
            entity.Property(e => e.SubmissionDate)
                .HasPrecision(6)
                .HasColumnName("SUBMISSION_DATE");
            entity.Property(e => e.Tehsil)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TEHSIL");
            entity.Property(e => e.UniqueHhId)
                .HasColumnType("NUMBER")
                .HasColumnName("UNIQUE_HH_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
