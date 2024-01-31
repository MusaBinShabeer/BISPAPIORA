﻿using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Repositories.CitizenSchemeServicesRepo;
using BISPAPIORA.Repositories.BankServicesRepo;
using Microsoft.EntityFrameworkCore;
using BISPAPIORA.Repositories.ProvinceServicesRepo;
using BISPAPIORA.Repositories.DistrictServicesRepo;
using BISPAPIORA.Repositories.TehsilServicesRepo;
using BISPAPIORA.Repositories.EducationServicesRepo;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using BISPAPIORA.Repositories.RegistrationServicesRepo;
using BISPAPIORA.Repositories.EmploymentServicesRepo;
using BISPAPIORA.Repositories.EnrollmentServicesRepo;
using BISPAPIORA.Repositories.CitizenBankInfoServicesRepo;
using BISPAPIORA.Repositories.FileManagerServicesRepo;
using BISPAPIORA.Repositories.CitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;

namespace BISPAPIORA.Extensions
{
    public static class Services
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<Dbcontext>(options =>
            options.UseOracle(("User Id=admin;Password=vNrGBdITbyvVQtTspIx1;Data Source=oracle-database.cfgeu0k04wh6.us-east-1.rds.amazonaws.com:1521/bispdb;")), ServiceLifetime.Transient);
            //services.AddDbContext<Dbcontext>(options =>
            //           options.UseOracle(("User Id=savings;Password=savings;Data Source=localhost:1521/savings;")), ServiceLifetime.Transient);

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
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddTransient<IBankService, BankService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<ITehsilService, TehsilService>();
            services.AddTransient<IEducationService, EducationService>();
            services.AddTransient<ICitizenService, CitizenService>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IEmploymentService, EmploymentService>();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
            services.AddTransient<ICitizenSchemeService, CitizenSchemeService>();
            services.AddTransient<ICitizenBankInfoService, CitizenBankInfoService>();
            services.AddTransient<IFileManagerService, FileManagerService>();
            services.AddTransient<ICitizenAttachmentService, CitizenAttachmentService>();
            services.AddTransient<ICitizenThumbPrintService, CitizenThumbPrintService>();
        }
    }
}
