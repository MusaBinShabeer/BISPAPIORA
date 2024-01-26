using BISPAPIORA.Models.DBModels.Dbtables;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Models.DBModels.OraDbContextClass
{
    public class OraDbContext : DbContext
    {
        public OraDbContext(DbContextOptions<OraDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Has Data Methods
            #endregion
            #region Relation Ships
            #region tbl_district
            modelBuilder.Entity<tbl_district>()
                .HasOne(p => p.tbl_province)
                .WithMany(b => b.tbl_districts)
                .HasForeignKey(p => p.fk_province)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region tbl_tehsil
            modelBuilder.Entity<tbl_tehsil>()
                .HasOne(p => p.tbl_district)
                .WithMany(b => b.tbl_tehsils)
                .HasForeignKey(p => p.fk_district)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region tbl_citizen
            modelBuilder.Entity<tbl_citizen>()
                .HasOne(p => p.citizen_employement)
                .WithMany(b => b.citizens)
                .HasForeignKey(p => p.fk_citizen_employment)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<tbl_citizen>()
               .HasOne(p => p.citizen_education)
               .WithMany(b => b.citizens)
               .HasForeignKey(p => p.fk_citizen_education)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<tbl_citizen>()
             .HasOne(p => p.citizen_tehsil)
             .WithMany(b => b.citizens)
             .HasForeignKey(p => p.fk_tehsil)
             .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<tbl_citizen>()
               .HasOne(p => p.citizen_registration)
               .WithOne(b => b.tbl_citizen)
               .HasForeignKey<tbl_registration>(p => p.fk_citizen)
               .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<tbl_citizen>().
              Property<DateTime?>("citizen_date_of_birth").HasColumnType("timestamp without time zone");
            #endregion
            #region tbl_citizen_bank_info
            modelBuilder.Entity<tbl_citizen_bank_info>()
              .HasOne(p => p.tbl_citizen)
              .WithOne(b => b.tbl_citizen_bank_info)
              .HasForeignKey<tbl_citizen_bank_info>(p => p.fk_citizen)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<tbl_citizen_bank_info>()
              .HasOne(p => p.tbl_bank)
              .WithOne(b => b.tbl_citizen_bank_info)
              .HasForeignKey<tbl_citizen_bank_info>(p => p.fk_bank)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region tbl_citizen_scheme
            modelBuilder.Entity<tbl_citizen_scheme>()
              .HasOne(p => p.tbl_citizen)
              .WithOne(b => b.tbl_citizen_scheme)
              .HasForeignKey<tbl_citizen_scheme>(p => p.fk_citizen)
              .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #endregion
        }
        public DbSet<tbl_bank> tbl_banks { get; set; }
        public DbSet<tbl_province> tbl_provinces { get; set; }
        public DbSet<tbl_district> tbl_districts { get; set; }
        public DbSet<tbl_tehsil> tbl_tehsils { get; set; }
        public DbSet<tbl_education> tbl_educations { get; set; }
        public DbSet<tbl_employment> tbl_employments { get; set; }
        public DbSet<tbl_citizen> tbl_citizens { get; set; }
        public DbSet<tbl_registration> tbl_registration { get; set; }
        public DbSet<tbl_enrollment> tbl_enrollment { get; set; }
        public DbSet<tbl_citizen_bank_info> tbl_citizen_bank_info { get; set; }
        public DbSet<tbl_citizen_scheme> tbl_citizen_scheme { get; set; }
    }
}
