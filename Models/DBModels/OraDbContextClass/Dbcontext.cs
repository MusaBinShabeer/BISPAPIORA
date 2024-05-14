using System;
using System.Collections.Generic;
using System.Data.Common;
using BISPAPIORA.Models.DBModels.Dbtables;
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

    //public virtual DbSet<HiberProtectionAccount> HiberProtectionAccounts { get; set; }

    public virtual DbSet<tbl_bank> tbl_banks { get; set; }

    public virtual DbSet<tbl_functionality> tbl_functionalities { get; set; }

    public virtual DbSet<tbl_group_permission> tbl_group_permissions { get; set; }

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

    public virtual DbSet<tbl_citizen_compliance> tbl_citizen_compliances { get; set; }

    public virtual DbSet<tbl_transaction> tbl_transactions { get; set; }

    public virtual DbSet<tbl_employment_other_specification> tbl_employment_other_specifications { get; set; }

    public virtual DbSet<tbl_bank_other_specification> tbl_bank_other_specifications { get; set; }

    public virtual DbSet<tbl_image_citizen_attachment> tbl_image_citizen_attachments { get; set; }

    public virtual DbSet<tbl_image_citizen_finger_print> tbl_image_citizen_finger_prints { get; set; }

    public virtual DbSet<tbl_user> tbl_users { get; set; }

    public virtual DbSet<tbl_user_type> tbl_user_types { get; set; }

    public virtual DbSet<tbl_citizen_family_bank_info> tbl_citizen_family_bank_infos { get; set; }

    public virtual DbSet<tbl_app_version> tbl_app_versions { get; set; }

    public virtual DbSet<tbl_payment> tbl_payments { get; set; }

    public virtual DbSet<tbl_bank_statement> tbl_bank_statements { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseOracle("User Id=admin;Password=vNrGBdITbyvVQtTspIx1;Data Source=oracle-database.cfgeu0k04wh6.us-east-1.rds.amazonaws.com:1521/bispdb;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("ADMIN")
            .UseCollation("USING_NLS_COMP");
        //modelBuilder
        //    .HasDefaultSchema("SAVINGS")
        //    .UseCollation("USING_NLS_COMP");



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

        modelBuilder.Entity<tbl_functionality>(entity =>
        {
            entity.HasKey(e => e.functionality_id).HasName("TBL_FUNCTIONALITY_PK");

            entity.ToTable("TBL_FUNCTIONALITY");

            entity.Property(e => e.functionality_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("FUNCTIONALITY_ID");
            entity.Property(e => e.functionality_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("FUNCTIONALITY_NAME");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
        });

        modelBuilder.Entity<tbl_group_permission>(entity =>
        {
            entity.HasKey(e => e.group_permission_id).HasName("GROUP_PERMISSION_PK");

            entity.ToTable("TBL_GROUP_PERMISSION");

            entity.Property(e => e.group_permission_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("GROUP_PERMISSION_ID");
            entity.Property(e => e.can_create)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("CAN_CREATE");
            entity.Property(e => e.can_delete)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("CAN_DELETE");
            entity.Property(e => e.can_read)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("CAN_READ");
            entity.Property(e => e.can_update)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("CAN_UPDATE");
            entity.Property(e => e.fk_functionality)
                .HasDefaultValueSql("NULL")
                .HasColumnName("FK_FUNCTIONALITY");
            entity.Property(e => e.fk_user_type)
                .HasDefaultValueSql("NULL")
                .HasColumnName("FK_USER_TYPE");

            entity.HasOne(d => d.tbl_user_type).WithMany(p => p.tbl_group_permissions)
                .HasForeignKey(d => d.fk_user_type)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GROUP_PERMISSION_FK_USER_TYPE");

            entity.HasOne(d => d.tbl_functionality).WithMany(p => p.tbl_group_permissions)
                .HasForeignKey(d => d.fk_functionality)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("GROUP_PERMISSION_FK_FUNCTIONALITY");
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
                .HasColumnType("DATE")
                .HasColumnName("CITIZEN_DATE_OF_BIRTH");
            entity.Property(e => e.citizen_father_spouce_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_FATHER_SPOUSE_NAME");
            entity.Property(e => e.citizen_gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_GENDER");
            entity.Property(e => e.citizen_martial_status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("CITIZEN_MARITAL_STATUS");
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
            entity.Property(e => e.id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ID");
            entity.Property(e => e.is_valid_beneficiary)
               .HasPrecision(1)
               .HasColumnName("IS_VALID_BENEFICIARY");
            entity.Property(e => e.pmt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PMT");
            entity.Property(e => e.submission_date)
                .HasColumnType("DATE")
                .HasColumnName("SUBMISSION_DATE");

            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");

            entity.Property(e => e.unique_hh_id)
                .HasColumnType("NUMBER")
                .HasColumnName("UNIQUE_HH_ID");
            entity.Property(e => e.fk_citizen_education).HasColumnName("FK_CITIZEN_EDUCATION");
            entity.Property(e => e.fk_citizen_employment).HasColumnName("FK_CITIZEN_EMPLOYMENT");
            entity.Property(e => e.fk_tehsil).HasColumnName("FK_TEHSIL");

            entity.HasOne(d => d.tbl_citizen_employment).WithMany(p => p.tbl_citizens)
                .HasForeignKey(d => d.fk_citizen_employment)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_EMPLOYMENT");
            entity.HasOne(d => d.tbl_citizen_education).WithMany(p => p.tbl_citizens)
                .HasForeignKey(d => d.fk_citizen_education)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_EDUCATION");

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
            entity.Property(e => e.account_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("ACCOUNT_TYPE");
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

        modelBuilder.Entity<tbl_citizen_family_bank_info>(entity =>
        {
            entity.HasKey(e => e.citizen_bank_info_id).HasName("SYS_C006043");

            entity.ToTable("TBL_CITIZEN_FAMILY_BANK_INFO");

            entity.HasIndex(e => e.fk_citizen, "SYS_C006044").IsUnique();

            entity.Property(e => e.citizen_bank_info_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_BANK_INFO_ID");
            entity.Property(e => e.family_income)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("FAMILY_INCOME");
            entity.Property(e => e.account_holder_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("ACCOUNT_HOLDER_NAME");           
            entity.Property(e => e.fk_bank).HasColumnName("FK_BANK");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.iban_no)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("IBAN_NO");

            entity.HasOne(d => d.tbl_bank).WithMany(p => p.tbl_citizen_family_bank_infos)
                .HasForeignKey(d => d.fk_bank)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BANK_CITIZEN_FAMILY_BANK_INFO");

            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_citizen_family_bank_info)
                .HasForeignKey<tbl_citizen_family_bank_info>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CITIZEN_CITIZEN_FAMILY_BANK_INFO");
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
                .HasPrecision(10)
                .HasDefaultValueSql("0")
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
            entity.Property(e => e.id)
               .HasPrecision(10)
               .HasDefaultValueSql("0")
               .HasColumnName("ID");
            entity.Property(e => e.province_id)
              .HasPrecision(10)
              .HasDefaultValueSql("0")
              .HasColumnName("PROVINCE_ID");
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

            entity.Property(e => e.enrolled_date)
                .HasColumnType("DATE")
                .HasColumnName("ENROLLED_DATE");

            entity.Property(e => e.fk_enrolled_by).HasColumnName("FK_ENROLLED_BY");

            entity.HasOne(d => d.enrolled_by).WithMany(p => p.tbl_enrolled_citizens)
              .HasForeignKey(d => d.fk_enrolled_by)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_ENROLLED_BY_USER");
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
            entity.Property(e => e.id)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("ID");
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

            entity.Property(e => e.registered_date)
                .HasColumnType("DATE")
                .HasColumnName("REGISTERED_DATE");

            entity.Property(e => e.fk_registered_by).HasColumnName("FK_REGISTERED_BY");
            entity.HasOne(d => d.registered_by).WithMany(p => p.tbl_registered_citizens)
              .HasForeignKey(d => d.fk_registered_by)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_REGISTERED_BY_USER");
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
            entity.Property(e => e.id)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("ID");
            entity.Property(e => e.district_id)
               .HasPrecision(10)
               .HasDefaultValueSql("0")
               .HasColumnName("DISTRICT_ID");

            entity.HasOne(d => d.tbl_district).WithMany(p => p.tbl_tehsils)
                .HasForeignKey(d => d.fk_district)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DISTRICT_TEHSIL");
        });

        modelBuilder.Entity<tbl_bank_other_specification>(entity =>
        {
            entity.HasKey(e => e.bank_other_specification_id).HasName("SYS_C006191");

            entity.ToTable("TBL_BANK_OTHER_SPECIFICATION");

            entity.Property(e => e.bank_other_specification_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("BANK_OTHER_SPECIFICATION_ID");
            entity.Property(e => e.bank_other_specification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("BANK_OTHER_SPECIFICATION");            
            entity.Property(e => e.fk_citizen_bank_info).HasColumnName("FK_CITIZEN_BANK_INFO");
            entity.HasOne(d => d.tbl_citizen_bank_info).WithOne(p => p.tbl_bank_other_specification)
               .HasForeignKey<tbl_bank_other_specification>(d => d.fk_citizen_bank_info)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_BANK_OTHER_SPECIFICATION_CITIZEN_BANK_INFO");
            entity.Property(e => e.fk_citizen_family_bank_info).HasColumnName("FK_CITIZEN_FAMILY_BANK_INFO");
            entity.HasOne(d => d.tbl_citizen_family_bank_info).WithOne(p => p.tbl_bank_other_specification)
               .HasForeignKey<tbl_bank_other_specification>(d => d.fk_citizen_family_bank_info)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_BANK_OTHER_SPECIFICATION_CITIZEN_FAMILY_BANK_INFO"); ;
        });

        modelBuilder.Entity<tbl_citizen_compliance>(entity =>
        {
            entity.HasKey(e => e.citizen_compliance_id).HasName("SYS_C006184");

            entity.ToTable("TBL_CITIZEN_COMPLIANCE");

            entity.Property(e => e.citizen_compliance_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("CITIZEN_COMPLIANCE_ID");
            entity.Property(e => e.citizen_compliance_actual_saving_amount)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CITIZEN_COMPLIANCE_ACTUAL_SAVING_AMOUNT");
            entity.Property(e => e.closing_balance_on_quarterly_bank_statement)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("CLOSING_BALANCE_ON_QUARTERLY_BANK_STATEMENT");
            entity.Property(e => e.is_compliant)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_COMPLIANT");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.starting_balance_on_quarterly_bank_statement)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("STARTING_BALANCE_ON_QUARTERLY_BANK_STATEMENT");
            entity.Property(e => e.citizen_compliance_quarter_code)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("CITIZEN_COMPLIANCE_QUARTER_CODE");
            entity.HasOne(d => d.tbl_citizen)
                .WithMany(p => p.tbl_citizen_compliances) // Assuming tbl_citizen has a collection navigation property for compliance records
                .HasForeignKey(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_CITIZEN_COMPLIANCE_CITIZEN");
            entity.Property(e => e.fk_compliant_by).HasColumnName("FK_COMPLIANT_BY");
            entity.HasOne(d => d.compliant_by).WithMany(p => p.tbl_citizen_compliances)
              .HasForeignKey(d => d.fk_compliant_by)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_COMPLIANT_BY_USER");

            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");
        });

        modelBuilder.Entity<tbl_employment_other_specification>(entity =>
        {
            entity.HasKey(e => e.employment_other_specification_id).HasName("SYS_C006194");

            entity.ToTable("TBL_EMPLOYMENT_OTHER_SPECIFICATION");

            entity.Property(e => e.employment_other_specification_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("EMPLOYMENT_OTHER_SPECIFICATION_ID");
            entity.Property(e => e.employment_other_specification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("EMPLOYMENT_OTHER_SPECIFICATION");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.HasOne(d => d.tbl_citizen).WithOne(p => p.tbl_employment_other_specification)
                 .HasForeignKey<tbl_employment_other_specification>(d => d.fk_citizen)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_BANK_OTHER_SPECIFICATION_CITIZEN");
        });

        modelBuilder.Entity<tbl_transaction>(entity =>
        {
            entity.HasKey(e => e.transaction_id).HasName("SYS_C006188");

            entity.ToTable("TBL_TRANSACTION");

            entity.Property(e => e.transaction_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("TRANSACTION_ID");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.fk_compliance).HasColumnName("FK_COMPLIANCE");
            entity.Property(e => e.transaction_amount)
                .HasDefaultValueSql("0.0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TRANSACTION_AMOUNT");
            entity.Property(e => e.transaction_date)
                .HasPrecision(6)
                .HasColumnName("TRANSACTION_DATE");
            entity.Property(e => e.transaction_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("TRANSACTION_TYPE");
            entity.Property(e => e.transaction_quarter_code)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("TRANSACTION_QUARTER_CODE");
            entity.HasOne(d => d.tbl_citizen).WithMany(p => p.tbl_transactions)
                 .HasForeignKey(d => d.fk_citizen)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_TRANSACTION_CITIZEN");
            entity.HasOne(d => d.tbl_citizen_compliance).WithMany(p => p.tbl_transactions)
               .HasForeignKey(d => d.fk_compliance)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_COMPLIANCE");
        });

        modelBuilder.Entity<tbl_image_citizen_attachment>(entity =>
        {
            entity.HasKey(e => e.id).HasName("SYS_C007846");

            entity.ToTable("TBL_IMAGE_CITIZEN_ATTACHMENT");

            entity.Property(e => e.id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("ID");          
            entity.Property(e => e.cnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.content_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONTENT_TYPE");
            entity.Property(e => e.data)
                .HasColumnType("BLOB")
                .HasColumnName("DATA");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");

            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");

            entity.HasOne(d => d.tbl_citizen)
                  .WithOne(p => p.tbl_image_citizen_attachment)
                  .HasForeignKey<tbl_image_citizen_attachment>(d => d.fk_citizen)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_IMAGE_ATTACHMENT_CITIZEN");
        });

        modelBuilder.Entity<tbl_image_citizen_finger_print>(entity =>
        {
            entity.HasKey(e => e.id).HasName("SYS_C007849");

            entity.ToTable("TBL_IMAGE_CITIZEN_FINGER_PRINT");

            entity.Property(e => e.id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("ID");           
            entity.Property(e => e.cnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.finger_print_content_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FINGER_PRINT_CONTENT_TYPE");
            entity.Property(e => e.finger_print_data)
                .HasColumnType("BLOB")
                .HasColumnName("FINGER_PRINT_DATA");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.finger_print_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FINGER_PRINT_NAME");
            entity.Property(e => e.thumb_print_content_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("THUMB_PRINT_CONTENT_TYPE");
            entity.Property(e => e.thumb_print_data)
                .HasColumnType("BLOB")
                .HasColumnName("THUMB_PRINT_DATA");
            entity.Property(e => e.thumb_print_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("THUMB_PRINT_NAME");

            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");

            entity.HasOne(d => d.tbl_citizen)
                .WithOne(p => p.tbl_image_citizen_finger_print)
                .HasForeignKey<tbl_image_citizen_finger_print>(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_IMAGE_FINGER_PRINT_CITIZEN");
        });

        modelBuilder.Entity<tbl_user>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("SYS_C008199");

            entity.ToTable("TBL_USER");

            entity.Property(e => e.user_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("USER_ID");
            entity.Property(e => e.fk_user_type).HasColumnName("FK_USER_TYPE");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.is_ftp_set)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("0 ")
                .HasColumnName("IS_FTP_SET");
            entity.Property(e => e.user_email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("USER_EMAIL");
            entity.Property(e => e.user_password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'12345'")
                .HasColumnName("USER_PASSWORD");
            entity.Property(e => e.user_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("USER_NAME");
            entity.Property(e => e.user_otp)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_OTP");
            entity.Property(e => e.user_token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("USER_TOKEN");

            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");

            entity.HasOne(d => d.tbl_user_type).WithMany(p => p.tbl_users)
                .HasForeignKey(d => d.fk_user_type)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_USER_USER_TYPE");
        });

        modelBuilder.Entity<tbl_user_type>(entity =>
        {
            entity.HasKey(e => e.user_type_id).HasName("SYS_C008196");

            entity.ToTable("TBL_USER_TYPE");

            entity.Property(e => e.user_type_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("USER_TYPE_ID");
            entity.Property(e => e.is_active)
                .IsRequired()
                .HasPrecision(1)
                .HasDefaultValueSql("1 ")
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.user_type_name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("USER_TYPE_NAME");
        });

        modelBuilder.Entity<tbl_app_version>(entity =>
        {
            entity.HasKey(e => e.app_version_id).HasName("SYS_C008281");

            entity.ToTable("TBL_APP_VERSION");

            entity.Property(e => e.app_version_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("APP_VERSION_ID");
            entity.Property(e => e.app_update_url)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("APP_UPDATE_URL");
            entity.Property(e => e.app_version)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("''")
                .HasColumnName("APP_VERSION");
        });

        modelBuilder.Entity<tbl_payment>(entity =>
        {
            entity.HasKey(e => e.payment_id).HasName("SYS_C006917");

            entity.ToTable("TBL_PAYMENT");

            entity.Property(e => e.payment_id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("PAYMENT_ID");
            entity.Property(e => e.actual_due_amount)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(19,2)")
                .HasColumnName("ACTUAL_DUE_AMOUNT");
            entity.Property(e => e.quarterly_due_amount)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(19,2)")
                .HasColumnName("QUARTERLY_DUE_AMOUNT");
            entity.Property(e => e.fk_citizen).HasColumnName("FK_CITIZEN");
            entity.Property(e => e.fk_compliance).HasColumnName("FK_COMPLIANCE");
            entity.Property(e => e.paid_amount)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(19,2)")
                .HasColumnName("PAID_AMOUNT");
            entity.Property(e => e.payment_quarter_code)
                .HasPrecision(10)
                .HasDefaultValueSql("0")
                .HasColumnName("PAYMENT_QUARTER_CODE");

            entity.HasOne(d => d.tbl_citizen).WithMany(p => p.tbl_payments)
                .HasForeignKey(d => d.fk_citizen)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PAYMENT_CITIZEN");

            entity.HasOne(d => d.tbl_citizen_compliance).WithMany(p => p.tbl_payments)
                .HasForeignKey(d => d.fk_compliance)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PAYMENT_CITIZEN_COMPLIANCE");
        });

        modelBuilder.Entity<tbl_bank_statement>(entity =>
        {
            entity.HasKey(e => e.id).HasName("SYS_C006978");

            entity.ToTable("TBL_BANK_STATEMENT");

            entity.HasIndex(e => e.fk_citizen_compliance, "SYS_C006979").IsUnique();

            entity.Property(e => e.id)
                .HasDefaultValueSql("SYS_GUID() ")
                .HasColumnName("ID");
            entity.Property(e => e.cnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CNIC");
            entity.Property(e => e.content_type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("CONTENT_TYPE");
            entity.Property(e => e.data)
                .HasColumnType("BLOB")
                .HasColumnName("DATA");
            entity.Property(e => e.fk_citizen_compliance).HasColumnName("FK_CITIZEN_COMPLIANCE");
            entity.Property(e => e.insertion_date)
                .HasColumnType("DATE")
                .HasColumnName("INSERTION_DATE");
            entity.Property(e => e.name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("'' ")
                .HasColumnName("NAME");

            entity.HasOne(d => d.tbl_citizen_compliance).WithOne(p => p.tbl_bank_statement)
                .HasForeignKey<tbl_bank_statement>(d => d.fk_citizen_compliance)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BANK_STATEMENT_COMPLIANCE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
