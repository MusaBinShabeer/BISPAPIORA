﻿// <auto-generated />
using System;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace BISPAPIORA.Migrations
{
    [DbContext(typeof(OraDbContext))]
    [Migration("20240126104534_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_bank", b =>
                {
                    b.Property<Guid>("bank_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("bank_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("bank_id");

                    b.ToTable("tbl_banks");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", b =>
                {
                    b.Property<Guid>("citizen_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("citizen_address")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("citizen_cnic")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime?>("citizen_date_of_birth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("citizen_father_spouce_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("citizen_gender")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("citizen_martial_status")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("citizen_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("citizen_phone_no")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid?>("fk_citizen_education")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid?>("fk_citizen_employment")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid?>("fk_tehsil")
                        .HasColumnType("RAW(16)");

                    b.HasKey("citizen_id");

                    b.HasIndex("fk_citizen_education");

                    b.HasIndex("fk_citizen_employment");

                    b.HasIndex("fk_tehsil");

                    b.ToTable("tbl_citizens");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen_bank_info", b =>
                {
                    b.Property<Guid>("citizen_bank_info_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("account_type")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid?>("fk_bank")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid?>("fk_citizen")
                        .HasColumnType("RAW(16)");

                    b.Property<string>("iban_no")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("citizen_bank_info_id");

                    b.HasIndex("fk_bank")
                        .IsUnique()
                        .HasFilter("\"fk_bank\" IS NOT NULL");

                    b.HasIndex("fk_citizen")
                        .IsUnique()
                        .HasFilter("\"fk_citizen\" IS NOT NULL");

                    b.ToTable("tbl_citizen_bank_info");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_district", b =>
                {
                    b.Property<Guid>("district_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("district_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<Guid>("fk_province")
                        .HasColumnType("RAW(16)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("district_id");

                    b.HasIndex("fk_province");

                    b.ToTable("tbl_districts");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_education", b =>
                {
                    b.Property<Guid>("education_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("education_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("education_id");

                    b.ToTable("tbl_educations");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_employment", b =>
                {
                    b.Property<Guid>("employment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("employment_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.HasKey("employment_id");

                    b.ToTable("tbl_employments");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_enrollment", b =>
                {
                    b.Property<Guid>("enrollment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("fk_citizen")
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("tbl_citizencitizen_id")
                        .HasColumnType("RAW(16)");

                    b.HasKey("enrollment_id");

                    b.HasIndex("tbl_citizencitizen_id");

                    b.ToTable("tbl_enrollment");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_province", b =>
                {
                    b.Property<Guid>("province_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("province_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("province_id");

                    b.ToTable("tbl_provinces");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_registration", b =>
                {
                    b.Property<Guid>("registration_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("fk_citizen")
                        .HasColumnType("RAW(16)");

                    b.HasKey("registration_id");

                    b.HasIndex("fk_citizen")
                        .IsUnique();

                    b.ToTable("tbl_registration");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_tehsil", b =>
                {
                    b.Property<Guid>("tehsil_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<Guid>("fk_district")
                        .HasColumnType("RAW(16)");

                    b.Property<bool>("is_active")
                        .HasColumnType("NUMBER(1)");

                    b.Property<string>("tehsil_name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("tehsil_id");

                    b.HasIndex("fk_district");

                    b.ToTable("tbl_tehsils");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_education", "citizen_education")
                        .WithMany("citizens")
                        .HasForeignKey("fk_citizen_education")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_employment", "citizen_employement")
                        .WithMany("citizens")
                        .HasForeignKey("fk_citizen_employment")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_tehsil", "citizen_tehsil")
                        .WithMany("citizens")
                        .HasForeignKey("fk_tehsil")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("citizen_education");

                    b.Navigation("citizen_employement");

                    b.Navigation("citizen_tehsil");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen_bank_info", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_bank", "tbl_bank")
                        .WithOne("tbl_citizen_bank_info")
                        .HasForeignKey("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen_bank_info", "fk_bank")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", "tbl_citizen")
                        .WithOne("tbl_citizen_bank_info")
                        .HasForeignKey("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen_bank_info", "fk_citizen")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("tbl_bank");

                    b.Navigation("tbl_citizen");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_district", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_province", "tbl_province")
                        .WithMany("tbl_districts")
                        .HasForeignKey("fk_province")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_province");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_enrollment", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", "tbl_citizen")
                        .WithMany()
                        .HasForeignKey("tbl_citizencitizen_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_citizen");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_registration", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", "tbl_citizen")
                        .WithOne("citizen_registration")
                        .HasForeignKey("BISPAPIORA.Models.DBModels.Dbtables.tbl_registration", "fk_citizen")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_citizen");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_tehsil", b =>
                {
                    b.HasOne("BISPAPIORA.Models.DBModels.Dbtables.tbl_district", "tbl_district")
                        .WithMany("tbl_tehsils")
                        .HasForeignKey("fk_district")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_district");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_bank", b =>
                {
                    b.Navigation("tbl_citizen_bank_info");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_citizen", b =>
                {
                    b.Navigation("citizen_registration");

                    b.Navigation("tbl_citizen_bank_info");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_district", b =>
                {
                    b.Navigation("tbl_tehsils");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_education", b =>
                {
                    b.Navigation("citizens");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_employment", b =>
                {
                    b.Navigation("citizens");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_province", b =>
                {
                    b.Navigation("tbl_districts");
                });

            modelBuilder.Entity("BISPAPIORA.Models.DBModels.Dbtables.tbl_tehsil", b =>
                {
                    b.Navigation("citizens");
                });
#pragma warning restore 612, 618
        }
    }
}
