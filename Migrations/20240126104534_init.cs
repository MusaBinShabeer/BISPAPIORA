using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BISPAPIORA.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_banks",
                columns: table => new
                {
                    bank_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    bank_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_banks", x => x.bank_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_educations",
                columns: table => new
                {
                    education_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    education_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_educations", x => x.education_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_employments",
                columns: table => new
                {
                    employment_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    employment_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_employments", x => x.employment_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_provinces",
                columns: table => new
                {
                    province_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    province_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_provinces", x => x.province_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_districts",
                columns: table => new
                {
                    district_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    district_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    fk_province = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_districts", x => x.district_id);
                    table.ForeignKey(
                        name: "FK_tbl_districts_tbl_provinces_fk_province",
                        column: x => x.fk_province,
                        principalTable: "tbl_provinces",
                        principalColumn: "province_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_tehsils",
                columns: table => new
                {
                    tehsil_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    tehsil_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    is_active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    fk_district = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tehsils", x => x.tehsil_id);
                    table.ForeignKey(
                        name: "FK_tbl_tehsils_tbl_districts_fk_district",
                        column: x => x.fk_district,
                        principalTable: "tbl_districts",
                        principalColumn: "district_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_citizens",
                columns: table => new
                {
                    citizen_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    citizen_cnic = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_father_spouce_name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_phone_no = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_gender = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_address = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_martial_status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    citizen_date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    fk_citizen_education = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    fk_citizen_employment = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    fk_tehsil = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_citizens", x => x.citizen_id);
                    table.ForeignKey(
                        name: "FK_tbl_citizens_tbl_educations_fk_citizen_education",
                        column: x => x.fk_citizen_education,
                        principalTable: "tbl_educations",
                        principalColumn: "education_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_citizens_tbl_employments_fk_citizen_employment",
                        column: x => x.fk_citizen_employment,
                        principalTable: "tbl_employments",
                        principalColumn: "employment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_citizens_tbl_tehsils_fk_tehsil",
                        column: x => x.fk_tehsil,
                        principalTable: "tbl_tehsils",
                        principalColumn: "tehsil_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_citizen_bank_info",
                columns: table => new
                {
                    citizen_bank_info_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    iban_no = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    account_type = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    fk_bank = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    fk_citizen = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_citizen_bank_info", x => x.citizen_bank_info_id);
                    table.ForeignKey(
                        name: "FK_tbl_citizen_bank_info_tbl_banks_fk_bank",
                        column: x => x.fk_bank,
                        principalTable: "tbl_banks",
                        principalColumn: "bank_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_citizen_bank_info_tbl_citizens_fk_citizen",
                        column: x => x.fk_citizen,
                        principalTable: "tbl_citizens",
                        principalColumn: "citizen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_enrollment",
                columns: table => new
                {
                    enrollment_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    fk_citizen = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    tbl_citizencitizen_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_enrollment", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK_tbl_enrollment_tbl_citizens_tbl_citizencitizen_id",
                        column: x => x.tbl_citizencitizen_id,
                        principalTable: "tbl_citizens",
                        principalColumn: "citizen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_registration",
                columns: table => new
                {
                    registration_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    fk_citizen = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_registration", x => x.registration_id);
                    table.ForeignKey(
                        name: "FK_tbl_registration_tbl_citizens_fk_citizen",
                        column: x => x.fk_citizen,
                        principalTable: "tbl_citizens",
                        principalColumn: "citizen_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_citizen_bank_info_fk_bank",
                table: "tbl_citizen_bank_info",
                column: "fk_bank",
                unique: true,
                filter: "\"fk_bank\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_citizen_bank_info_fk_citizen",
                table: "tbl_citizen_bank_info",
                column: "fk_citizen",
                unique: true,
                filter: "\"fk_citizen\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_citizens_fk_citizen_education",
                table: "tbl_citizens",
                column: "fk_citizen_education");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_citizens_fk_citizen_employment",
                table: "tbl_citizens",
                column: "fk_citizen_employment");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_citizens_fk_tehsil",
                table: "tbl_citizens",
                column: "fk_tehsil");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_districts_fk_province",
                table: "tbl_districts",
                column: "fk_province");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_enrollment_tbl_citizencitizen_id",
                table: "tbl_enrollment",
                column: "tbl_citizencitizen_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_registration_fk_citizen",
                table: "tbl_registration",
                column: "fk_citizen",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tehsils_fk_district",
                table: "tbl_tehsils",
                column: "fk_district");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_citizen_bank_info");

            migrationBuilder.DropTable(
                name: "tbl_enrollment");

            migrationBuilder.DropTable(
                name: "tbl_registration");

            migrationBuilder.DropTable(
                name: "tbl_banks");

            migrationBuilder.DropTable(
                name: "tbl_citizens");

            migrationBuilder.DropTable(
                name: "tbl_educations");

            migrationBuilder.DropTable(
                name: "tbl_employments");

            migrationBuilder.DropTable(
                name: "tbl_tehsils");

            migrationBuilder.DropTable(
                name: "tbl_districts");

            migrationBuilder.DropTable(
                name: "tbl_provinces");
        }
    }
}
