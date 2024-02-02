using System;
using System.Collections.Generic;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Models.DBModels.OraDbContextClass;

public partial class Dbcontext : DbContext
{
    public Dbcontext()
    {
    }

    public Dbcontext(DbContextOptions<Dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<HiberProtectionAccount> HiberProtectionAccounts { get; set; }

    public virtual DbSet<tbl_bank> tbl_banks { get; set; }

    public virtual DbSet<tbl_citizen> tbl_citizens { get; set; }

    public virtual DbSet<tbl_citizen_bank_info> tbl_citizen_bank_infos { get; set; }

    public virtual DbSet<tbl_citizen_scheme> tbl_citizen_schemes { get; set; }

    public virtual DbSet<tbl_district> tbl_districts { get; set; }

    public virtual DbSet<tbl_education> tbl_educations { get; set; }

    public virtual DbSet<tbl_employment> tbl_employments { get; set; }

    public virtual DbSet<tbl_enrollment> tbl_enrollments { get; set; }

    public virtual DbSet<tbl_province> tbl_provinces { get; set; }

    public virtual DbSet<tbl_registration> tbl_registrations { get; set; }

    public virtual DbSet<tbl_tehsil> tbl_tehsils { get; set; }
    public virtual DbSet<tbl_citizen_attachment> tbl_citizen_attachments { get; set; }
    public virtual DbSet<tbl_citizen_thumb_print> tbl_citizen_thumb_prints { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseOracle("User Id=admin;Password=vNrGBdITbyvVQtTspIx1;Data Source=oracle-database.cfgeu0k04wh6.us-east-1.rds.amazonaws.com:1521/bispdb;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("ADMIN")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<HiberProtectionAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HIBER_PROTECTION_ACCOUNT");

            entity.HasIndex(e => e.Cnic, "HIBER_PROTECTION_ACCOUNT_UK1").IsUnique();

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

        modelBuilder.Entity<tbl_bank>(entity =>
        {
            entity.HasKey(e => e.bank_id).HasName("SYS_C006036");

            entity.ToTable("TBL_BANK");

            entity.Property(e => e.bank_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("BANK_ID");
            entity.Property(e => e.bank_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("BANK_NAME");
            entity.Property(e => e.bank_prefix_iban)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("BANK_PREFIX_IBAN");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
        });

        modelBuilder.Entity<tbl_citizen>(entity =>
        {
            entity.HasKey(e => e.citizen_id).HasName("SYS_C006018");

            entity.ToTable("TBL_CITIZEN");

            entity.Property(e => e.citizen_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_ID");
            entity.Property(e => e.citizen_address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_ADDRESS");
            entity.Property(e => e.citizen_cnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CITIZEN_CNIC");
            entity.Property(e => e.citizen_date_of_birth)
                .HasPrecision(6)
                .HasColumnName("CITIZEN_DATE_OF_BIRTH");
            entity.Property(e => e.citizen_father_spouce_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_FATHER_SPOUCE_NAME");
            entity.Property(e => e.citizen_gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_GENDER");
            entity.Property(e => e.citizen_martial_status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_MARTIAL_STATUS");
            entity.Property(e => e.citizen_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_NAME");
            entity.Property(e => e.citizen_phone_no)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_PHONE_NO");
            entity.Property(e => e.is_valid_beneficiary)
               .HasPrecision(1)
               .HasColumnName("IS_VALID_BENEFICIARY");
            entity.Property(e => e.pmt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PMT");
            entity.Property(e => e.submission_date)
                .HasPrecision(6)
                .HasColumnName("SUBMISSION_DATE");
            entity.Property(e => e.unique_hh_id)
                .HasColumnType("NUMBER")
                .HasColumnName("UNIQUE_HH_ID");
            entity.Property(e => e.fk_citizen_education).HasColumnName("FK_CITIZEN_EDUCATION");
            entity.Property(e => e.fk_citizen_employment).HasColumnName("FK_CITIZEN_EMPLOYMENT");
            entity.Property(e => e.fk_tehsil).HasColumnName("FK_TEHSIL");

            entity.HasOne(d => d.tbl_citizen_education).WithMany(p => p.tbl_citizens)
                .HasForeignKey(d => d.fk_citizen_education)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_EDUCATION");

            entity.HasOne(d => d.tbl_citizen_employment).WithMany(p => p.tbl_citizens)
                .HasForeignKey(d => d.fk_citizen_employment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_EMPLOYMENT");

            entity.HasOne(d => d.tbl_citizen_tehsil).WithMany(p => p.tbl_citizens)
                .HasForeignKey(d => d.fk_tehsil)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TEHSIL");
        });

        modelBuilder.Entity<tbl_citizen_bank_info>(entity =>
        {
            entity.HasKey(e => e.citizen_bank_info_id).HasName("SYS_C006043");

            entity.ToTable("TBL_CITIZEN_BANK_INFO");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006044").IsUnique();

            entity.Property(e => e.citizen_bank_info_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_BANK_INFO_ID");
            entity.Property(e => e.a_i_o_f)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("A_I_O_F");
            entity.Property(e => e.account_holder_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("ACCOUNT_HOLDER_NAME");
            entity.Property(e => e.account_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("ACCOUNT_TYPE");
            entity.Property(e => e.family_saving_account)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("FAMILY_SAVING_ACCOUNT");
            entity.Property(e => e.fk_bank).HasColumnName("FK_BANK");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.iban_no)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("IBAN_NO");

            entity.HasOne(d => d.tbl_bank).WithMany(p => p.tbl_citizen_bank_infos)
                .HasForeignKey(d => d.fk_bank)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BANK_CITIZEN_BANK_INFO");

            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_bank_info)
                .HasForeignKey<tbl_citizen_bank_info>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_CITIZEN_BANK_INFO");
        });

        modelBuilder.Entity<tbl_citizen_scheme>(entity =>
        {
            entity.HasKey(e => e.citizen_scheme_id).HasName("SYS_C006023");

            entity.ToTable("TBL_CITIZEN_SCHEME");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006024").IsUnique();

            entity.Property(e => e.citizen_scheme_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_SCHEME_ID");
            entity.Property(e => e.citizen_scheme_quarter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_SCHEME_QUARTER");
            entity.Property(e => e.citizen_scheme_saving_amount)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CITIZEN_SCHEME_SAVING_AMOUNT");
            entity.Property(e => e.citizen_scheme_starting_month)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_SCHEME_STARTING_MONTH");
            entity.Property(e => e.citizen_scheme_quarter_code)
              .HasPrecision(10)
              .HasDefaultValueSql("0")
              .HasColumnName("CITIZEN_SCHEME_QUARTER_CODE");
            entity.Property(e => e.citizen_scheme_year)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_SCHEME_YEAR");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");

            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_scheme)
                .HasForeignKey<tbl_citizen_scheme>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_SCHEME_CITIZEN");
        });

        modelBuilder.Entity<tbl_district>(entity =>
        {
            entity.HasKey(e => e.district_id).HasName("SYS_C006004");

            entity.ToTable("TBL_DISTRICT");

            entity.Property(e => e.district_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("DISTRICT_ID");
            entity.Property(e => e.district_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("DISTRICT_NAME");
            entity.Property(e => e.fk_province).HasColumnName("FK_PROVINCE");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");

            entity.HasOne(d => d.tbl_province).WithMany(p => p.tbl_districts)
                .HasForeignKey(d => d.fk_province)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DISTRICT_PROVINCE");
        });

        modelBuilder.Entity<tbl_education>(entity =>
        {
            entity.HasKey(e => e.education_id).HasName("SYS_C006012");

            entity.ToTable("TBL_EDUCATION");

            entity.Property(e => e.education_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("EDUCATION_ID");
            entity.Property(e => e.education_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("EDUCATION_NAME");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
        });

        modelBuilder.Entity<tbl_employment>(entity =>
        {
            entity.HasKey(e => e.employment_id).HasName("SYS_C006015");

            entity.ToTable("TBL_EMPLOYMENT");

            entity.Property(e => e.employment_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("EMPLOYMENT_ID");
            entity.Property(e => e.employment_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("EMPLOYMENT_NAME");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
        });

        modelBuilder.Entity<tbl_enrollment>(entity =>
        {
            entity.HasKey(e => e.enrollment_id).HasName("SYS_C006031");

            entity.ToTable("TBL_ENROLLMENT");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006032").IsUnique();

            entity.Property(e => e.enrollment_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("ENROLLMENT_ID");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");

            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_enrollment)
                .HasForeignKey<tbl_enrollment>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ENROLLMENT_CITIZEN");
        });

        modelBuilder.Entity<tbl_province>(entity =>
        {
            entity.HasKey(e => e.province_id).HasName("SYS_C006001");

            entity.ToTable("TBL_PROVINCE");

            entity.Property(e => e.province_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("PROVINCE_ID");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.province_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("PROVINCE_NAME");
        });

        modelBuilder.Entity<tbl_registration>(entity =>
        {
            entity.HasKey(e => e.registration_id).HasName("SYS_C006027");

            entity.ToTable("TBL_REGISTRATION");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006028").IsUnique();

            entity.Property(e => e.registration_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("REGISTRATION_ID");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");

            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_registration)
                .HasForeignKey<tbl_registration>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_REGISTRATION_CITIZEN");
        });

        modelBuilder.Entity<tbl_tehsil>(entity =>
        {
            entity.HasKey(e => e.tehsil_id).HasName("SYS_C006008");

            entity.ToTable("TBL_TEHSIL");

            entity.Property(e => e.tehsil_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("TEHSIL_ID");
            entity.Property(e => e.fk_district).HasColumnName("FK_DISTRICT");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.tehsil_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("TEHSIL_NAME");

            entity.HasOne(d => d.tbl_district).WithMany(p => p.tbl_tehsils)
                .HasForeignKey(d => d.fk_district)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DISTRICT_TEHSIL");
        });
        modelBuilder.Entity<tbl_citizen_attachment>(entity =>
        {
            entity.HasKey(e => e.citizen_attachment_id).HasName("SYS_C006061");

            entity.ToTable("TBL_CITIZEN_ATTACHMENT");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006062").IsUnique();

            entity.Property(e => e.citizen_attachment_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_ATTACHMENT_ID");
            entity.Property(e => e.attachment_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CITIZEN_ATTACHMENT_NAME");
            entity.Property(e => e.attachment_path)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CITIZEN_ATTACHMENT_PATH");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_attachment)
               .HasForeignKey<tbl_citizen_attachment>(d => d.fk_citizen)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_CITIZEN_ATTACHMENT_CITIZEN");
        });

        modelBuilder.Entity<tbl_citizen_thumb_print>(entity =>
        {
            entity.HasKey(e => e.citizen_thumb_print_id).HasName("SYS_C006067");

            entity.ToTable("TBL_CITIZEN_THUMB_PRINT");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006068").IsUnique();

            entity.Property(e => e.citizen_thumb_print_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_THUMB_PRINT_ID");
            entity.Property(e => e.citizen_thumb_print_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CITIZEN_THUMB_PRINT_NAME");
            entity.Property(e => e.citizen_thumb_print_path)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CITIZEN_THUMB_PRINT_PATH");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_thumb_print)
               .HasForeignKey<tbl_citizen_thumb_print>(d => d.fk_citizen)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_CITIZEN_THUMB_PRINT_CITIZEN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
