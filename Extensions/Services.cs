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
using BISPAPIORA.Repositories.CitizenComplianceServicesRepo;
using BISPAPIORA.Repositories.TransactionServicesRepo;
using BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo;
using BISPAPIORA.Repositories.EmploymentOtherSpecificationServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;

using BISPAPIORA.Repositories.UserTypeServicesRepo;
using BISPAPIORA.Repositories.UserServicesRepo;
using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Repositories.JWTServicesRepo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BISPAPIORA.Repositories.AuthServicesRepo;
using BISPAPIORA.Repositories.InnerServicesRepo;

using BISPAPIORA.Repositories.ReportingResponseServicesRepo;
using BISPAPIORA.Repositories.FunctionalityServicesRepo;
using BISPAPIORA.Models.DTOS.GroupPermissionDTO;
using BISPAPIORA.Repositories.GroupPermissionServicesRepo;
using BISPAPIORA.Repositories.ComplexMappersRepo;
using BISPAPIORA.Repositories.AppVersionValidatingServicesRepo;
using BISPAPIORA.Repositories.DashboardServicesRepo;
using BISPAPIORA.Repositories.PaymentServicesRepo;
using BISPAPIORA.Repositories.BankStatementServicesRepo;
using BISPAPIORA.Repositories.AppVersionServiceRepo;





namespace BISPAPIORA.Extensions
{
    public static class Services
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            //services.AddDbContext<Dbcontext>(options =>
            //options.UseOracle(("User Id=savings;Password=savings;Data Source=localhost:1521/savings;")), ServiceLifetime.Transient);

            //services.AddDbContext<Dbcontext>(options =>
            //options.UseOracle(("User Id=admin;Password=vNrGBdITbyvVQtTspIx1;Data Source=oracle-database.cfgeu0k04wh6.us-east-1.rds.amazonaws.com:1521/bispdb;")), ServiceLifetime.Transient);
            //////services.AddDbContext<Dbcontext>(options =>
            //           options.UseOracle(("User Id=savings;Password=savings;Data Source=localhost:1521/savings;")), ServiceLifetime.Transient);
            //services.AddDbContext<Dbcontext>(options =>
            //options.UseOracle(("User Id=savings;Password=Oracle_123;Data Source=exadata.bisp.gov.pk:1521/bispsc;")), ServiceLifetime.Transient);
            services.AddDbContext<Dbcontext>(options =>
            options.UseOracle((configuration.GetConnectionString("BISP"))), ServiceLifetime.Transient);

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
            services.AddTransient<IInnerServices, InnerServices>();
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
            services.AddTransient<ICitizenComplianceService, CitizenComplianceService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IBankOtherSpecificationService, BankOtherSpecificationService>();
            services.AddTransient<IEmploymentOtherSpecificationService, EmploymentOtherSpecificationService>();
            services.AddTransient<IImageCitizenAttachmentService, ImageCitizenAttachmentService>();
            services.AddTransient<IImageCitizenFingerPrintService, ImageCitizenFingerPrintService>();
            services.AddTransient<IUserTypeService, UserTypeService>();
            services.AddTransient<IJwtUtils, JWTUtils>();
            services.AddTransient<UserAuthorizeAttribute>();
            services.AddTransient<IUserService, UserService>(); 
            services.AddTransient<IAuthServices, AuthServices>();
            services.AddTransient<IReportingResponseService, ReportingResponseService>();
            services.AddTransient<IFunctionalityService, FunctionalityService>();
            services.AddTransient<IGroupPermissionService, GroupPermissionService>();
            services.AddTransient<IAppVersionValidatingServices, AppVersionValidatingServices>();
            services.AddTransient<IComplexMapperServices, ComplexMapperServices>();
            services.AddTransient<IAppVersionService, AppVersionService>();
            services.AddTransient<IDashboardServices, DashboardServices>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IBankStatementService, BankStatementService>();
        }
    }
}
