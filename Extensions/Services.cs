using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Repositories.CitizenSchemeServicesRepo;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Extensions
{
    public static class Services
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<OraDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("BISP")), ServiceLifetime.Transient);
            services.AddDbContext<DbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("BISP")), ServiceLifetime.Transient);
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowedOrigins",
                                  policy =>
                                  {
                                      policy
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowAnyOrigin();
                                  });
            });
            services.AddHttpClient();
            //services.AddAutoMapper(typeof(Program).Assembly);
            //services.AddTransient<IBankService, BankService>();
            //services.AddTransient<IProvinceService, ProvinceService>();
            //services.AddTransient<IDistrictService, DistrictService>();
            //services.AddTransient<ITehsilService, TehsilService>();
            //services.AddTransient<IEducationService, EducationService>();
            //services.AddTransient<ICitizenService, CitizenService>();
            //services.AddTransient<IRegistrationService, RegistrationService>();
            //services.AddTransient<IEmploymentService, EmploymentService>();
            //services.AddTransient<IEnrollmentService, EnrollmentService>();
            //services.AddTransient<ICitizenSchemeService, CitizenSchemeService>();
        }
    }
}
