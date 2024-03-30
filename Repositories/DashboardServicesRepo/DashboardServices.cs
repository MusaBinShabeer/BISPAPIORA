using AutoMapper;
using AutoMapper.QueryableExtensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public class DashboardServices : IDashboardServices
    {
        private readonly Dbcontext db;
        private readonly IMapper mapper;
        public DashboardServices(Dbcontext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStatsForApp(string userName, string dateStart, string dateEnd)
        {
            try
            {
                var registeredCitizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Where(x => x.tbl_citizen_registration != null && x.tbl_enrollment == null)
                    .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);
                var enrolledCitizenQuery = db.tbl_citizens
                   .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                   .Where(x => x.tbl_enrollment != null)
                   .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);
                var predicateRegistered = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateRegistered = predicateRegistered.And(x => x.user_name == (userName));
                var predicateEnrolled = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateEnrolled = predicateEnrolled.And(x => x.user_name == (userName));
                if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                {
                    predicateRegistered = predicateRegistered.And(x => x.registered_date >= DateTime.Parse(dateStart) && x.registered_date <= DateTime.Parse(dateEnd));
                    predicateEnrolled = predicateEnrolled.And(x => x.enrolled_date >= DateTime.Parse(dateStart) && x.enrolled_date <= DateTime.Parse(dateEnd));
                }
                var registeredCitizen = registeredCitizenQuery.Where(predicateRegistered).ToList();
                var enrolledCitizen = enrolledCitizenQuery.Where(predicateEnrolled).ToList();
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    data = mapper.Map<DashboardUserPerformanceResponseDTO>((registeredCitizen, enrolledCitizen)),
                    remarks = "Success",
                    success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }
        public async Task<ResponseModel<WebDashboardStats,WebDashboardStats,WebDashboardStats,WebDashboardStats,WebDashboardStats>> GetWebDashboardStats()
        {
            try
            {
               
                #region Predicates
                var predicateRegisteredApplicants = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                var predicateEnrolledApplicants = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                var predicateCitizenSavings = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                #endregion
                #region Querry
                var citizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Include(x=>x.tbl_citizen_scheme)
                    //.Where(x => x.tbl_citizen_registration != null && x.tbl_enrollment == null)
                    .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);
                #endregion
                #region Filters
                predicateRegisteredApplicants = predicateRegisteredApplicants.And(x => x.registration != null && x.enrollment == null);
                predicateEnrolledApplicants = predicateEnrolledApplicants.And(x =>  x.enrollment != null);
                predicateCitizenSavings = predicateCitizenSavings.And(x =>  x.saving_amount != null);
                #endregion
                #region Executing Querry
                var registeredCitizen = await citizenQuery.Where(predicateRegisteredApplicants).ToListAsync();
                var enrolledCitizen = await citizenQuery.Where(predicateEnrolledApplicants).ToListAsync();
                var citizensSavings = await citizenQuery.Where(predicateCitizenSavings).ToListAsync();
                #endregion
                #region Calculating Stats
                var totalRegisteredCount = registeredCitizen.Count;
                var totalEnrolledCount = enrolledCitizen.Count;
                var totalCitizenSavings = citizensSavings.Select(x=>x.saving_amount).Sum();
                var totalCitizenCount = totalRegisteredCount + totalEnrolledCount;
                #endregion
                #region Generating Response
                return new ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>()
                {
                    totalApplicants = mapper.Map<WebDashboardStats>(("Total Applicants", totalRegisteredCount)),
                    totalEnrollments = mapper.Map<WebDashboardStats>(("Total Enrollments",totalEnrolledCount)),
                    totalSavings = mapper.Map<WebDashboardStats>(("Total Savings", totalCitizenSavings)),
                    remarks = "Success",
                    success = true
                };
                #endregion
            }
            catch (Exception ex)
            {
                return new ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }
        public async Task<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>>> GetWebDesktopApplicantDistributionLocationBased( string dateStart, string dateEnd, string provinceId, string districtId, string tehsilId, bool registration, bool enrollment)
        {
            try
            {
                #region Predicates
                var predicateDistrict = PredicateBuilder.New<tbl_district>(true);
                var predicateTehsil = PredicateBuilder.New<tbl_tehsil>(true);
                var predicateCitizen = PredicateBuilder.New<DashboardCitizenLocationModel>(true);
                #endregion
                var totalCitizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_scheme)
                    .ProjectTo<DashboardCitizenLocationModel>(mapper.ConfigurationProvider);
                #region Filters
                if (registration == true)
                {
                    predicateCitizen = predicateCitizen.And(x => x.registration != null && x.enrollment == null);
                    if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                    {
                        predicateCitizen = predicateCitizen.And(x => x.registered_date >= DateTime.Parse(dateStart) && x.registered_date <= DateTime.Parse(dateEnd));
                    }
                }
                if(enrollment == true)
                {
                    predicateCitizen = predicateCitizen.And(x => x.enrollment != null);
                    if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                    {
                        predicateCitizen = predicateCitizen.And(x => x.enrolled_date >= DateTime.Parse(dateStart) && x.enrolled_date <= DateTime.Parse(dateEnd));
                    }
                }
              
                if (!string.IsNullOrEmpty(provinceId))
                {                   
                    if (!string.IsNullOrEmpty(districtId)) 
                    {                       
                        if(!string.IsNullOrEmpty(tehsilId)) 
                        {
                            predicateCitizen = predicateCitizen.And(x => x.tehsil_id == Guid.Parse(tehsilId));
                            predicateTehsil = predicateTehsil.And(x => x.tehsil_id == Guid.Parse(tehsilId));
                        }
                        else
                        {
                            predicateCitizen = predicateCitizen.And(x => x.district_id == Guid.Parse(districtId));
                            predicateDistrict = predicateDistrict.And(x => x.district_id == Guid.Parse(districtId));
                            predicateTehsil = predicateTehsil.And(x => x.fk_district == Guid.Parse(districtId));
                        }
                    }
                    else
                    {
                        predicateCitizen = predicateCitizen.And(x => x.province_id == Guid.Parse(provinceId));
                        predicateDistrict = predicateDistrict.And(x => x.fk_province == Guid.Parse(provinceId));
                        predicateTehsil = predicateTehsil.And(x => x.tbl_district.fk_province == Guid.Parse(provinceId));
                    }
                }                
                var filteredCitizens = await totalCitizenQuery.Where(predicateCitizen).ToListAsync();
                #endregion
                #region Location Base Group By               
                #region Province Query
                var provinces = await db.tbl_provinces.ToListAsync();
                var provinceCitizenGroups = provinces.Select(province =>
                {
                    var provinceFilteredCitizens = filteredCitizens.Where(citizen => citizen.province_name.ToLower() == province.province_name.ToLower()).ToList();
                    var provinceCitizenCount = provinceFilteredCitizens.Count;
                    var citizenPercentage = provinceCitizenCount > 0 ? (double)provinceCitizenCount / filteredCitizens.Count * 100 : 0;

                    return new DashboardProvinceCitizenCountPercentageDTO
                    {
                        provinceName = province.province_name,
                        citizenPercentage = citizenPercentage
                    };
                }).ToList();
             
                #endregion
                #region District Query
                var districtsQuery = db.tbl_districts.Include(x => x.tbl_province).AsQueryable();                    
                var districts= await districtsQuery.Where(predicateDistrict).ToListAsync();
                var districtCitizenGroups = districts.Select(district =>
                {
                    var districtFilteredCitizens = filteredCitizens.Where(citizen => citizen.district_name.ToLower() == district.district_name.ToLower()).ToList();
                    var districtCitizenCount = districtFilteredCitizens.Count;
                    var citizenPercentage = districtCitizenCount > 0 ? (double)districtCitizenCount / filteredCitizens.Count * 100 : 0;
                    return new DashboardDistrictCitizenCountPercentageDTO
                    {
                        districtName = district.district_name,
                        provinceName = district.tbl_province.province_name,
                        citizenPercentage = citizenPercentage
                    };
                }).ToList();
                #endregion
                #region Tehsil Query
                var tehsilsQuery = db.tbl_tehsils.Include(x => x.tbl_district).ThenInclude(x => x.tbl_province).AsQueryable();                   
                var tehsils= await tehsilsQuery.Where(predicateTehsil).ToListAsync();
                var tehsilCitizenGroups = tehsils.Select(tehsil =>
                {
                    var tehsilFilteredCitizens = filteredCitizens.Where(citizen => citizen.tehsil_name.ToLower() == tehsil.tehsil_name.ToLower()).ToList();
                    var tehsilCitizenCount = tehsilFilteredCitizens.Count;
                    var citizenPercentage = tehsilCitizenCount > 0 ? (double)tehsilCitizenCount / filteredCitizens.Count * 100 : 0;
                    return new DashboardTehsilCitizenCountPercentageDTO
                    {
                        tehsilName = tehsil.tehsil_name,
                        districtName = tehsil.tbl_district.district_name,
                        provinceName = tehsil.tbl_district.tbl_province.province_name,
                        citizenPercentage = citizenPercentage
                    };
                }).ToList();
                #endregion
                #endregion
                #region Citizen Gender-Based Region Applicant Distrubution
                var genderBasedCitizenGroups = filteredCitizens.GroupBy(citizen => citizen.citizen_gender);
                var genderGroupsWithPercenntage = genderBasedCitizenGroups.Select(group =>
                {
                    var genderCount = group.Count();
                    var citizenPercentage = (double)genderCount / filteredCitizens.Count * 100;
                    return new DashboardCitizenGenderPercentageDTO
                    {
                        citizenGender = group.Key,
                        citizenGenderPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Marital_Status-Based Region Applicant Distrubution
                // Grouping citizens based on their marital status
                var maritalStatusGroups = filteredCitizens.GroupBy(citizen => citizen.citizen_martial_status);

                // Calculating percentage for each marital status group and projecting it into a DTO
                var maritalStatusGroupsWithPercentage = maritalStatusGroups.Select(group =>
                {
                    // Counting the number of citizens in the current marital status group
                    var statusCount = group.Count();

                    // Calculating the percentage of citizens in the current marital status group
                    var citizenPercentage = (double)statusCount / filteredCitizens.Count * 100;

                    // Creating a DTO object for the current marital status group with its percentage
                    return new DashboardCitizenMaritalStatusPercentageDTO
                    {
                        citizenMaritalStatus = group.Key,
                        citizenMaritalStatusPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Educational Background Group By
                var educations = await db.tbl_educations.ToListAsync();
                var educationGroups = educations.Select(education =>
                {
                    var citizensGroupedByEducation = filteredCitizens.Where(citizen => citizen.educationId== education.education_id).ToList();
                    var citizensGroupedByEducationCount = citizensGroupedByEducation.Count;
                    var citizenPercentage = citizensGroupedByEducationCount > 0 ? (double)citizensGroupedByEducationCount / filteredCitizens.Count * 100 : 0;
                    return new DashboardCitizenEducationalPercentageStatDTO
                    {
                        educationalBackground = education.education_name,
                        educationalBackgroundPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion


                #region Citizen Employement Background Group By
                var employments = await db.tbl_employments.ToListAsync();
                var employmentGroups = employments.Select(employment =>
                {
                    var citizensGroupedByEmployment = filteredCitizens.Where(citizen => citizen.employmentId == employment.employment_id).ToList();
                    var citizensGroupedByEmploymentCount = citizensGroupedByEmployment.Count;
                    //var citizenPercentage = citizensGroupedByEmploymentCount > 0 ? (double)citizensGroupedByEmploymentCount / filteredCitizens.Count * 100 : 0;
                    return new DashboardCitizenEmploymentPercentageStatDTO
                    {
                        employmentBackground = employment.employment_name,
                        employmentBackgroundPercentage = citizensGroupedByEmploymentCount,
                    };
                }).ToList();
                #endregion


                #region Citizen Group By Scheme Saving Amount
                var citizenSchemes = await db.tbl_citizen_schemes.ToListAsync();
                var citizenSchemeGroups = citizenSchemes.Select(citizenScheme =>
                {
                    var citizensGroupedBySavingAoount = filteredCitizens.Where(citizen => citizen.saving_amount == citizenScheme.citizen_scheme_saving_amount).ToList();
                    var citizensGroupedBySavingAoountCount = citizensGroupedBySavingAoount.Count;
                    //var citizenPercentage = citizensGroupedByEmploymentCount > 0 ? (double)citizensGroupedByEmploymentCount / filteredCitizens.Count * 100 : 0;
                    return new DashboardCitizenCountSavingAmountDTO
                    {
                        totalCitizenCount = citizensGroupedBySavingAoountCount,
                        savingAmount = citizenScheme.citizen_scheme_saving_amount,
                    };
                }).GroupBy(citizenScheme => citizenScheme.savingAmount)
                .Select(group => group.First())
                .ToList();
                #endregion

                #region Citizen Trend
                var citizenTrendGroups = filteredCitizens.Where(citizen => citizen.insertion_date != null) // Filter out citizens with null insertion_date
                                                         .GroupBy(citizen => new DateTime(citizen.insertion_date!.Value.Year, citizen.insertion_date.Value.Month, 1)) // Grouping citizens by insertion month
                                                         .Select(group =>
                                                         {
                                                             var insertionMonth = new DateTime(group.Key.Year, group.Key.Month, 1); // The insertion month for the current group
                                                             var citizenCount = group.Count(); // Counting the number of citizens for the current insertion month
                                                             return new DashboardCitizenTrendDTO
                                                             {
                                                                 insertionMonth = insertionMonth,
                                                                 totalCitizenCount = citizenCount
                                                             };
                                                         })
                                                         .ToList();

                // Sort the citizenTrendGroups list by InsertionMonth in descending order
                citizenTrendGroups = citizenTrendGroups.OrderByDescending(x => x.insertionMonth).ToList();

                // Select the latest 6 months
                var citizenTrendGroupslatestSixMonths = citizenTrendGroups.Take(6).ToList();
                #endregion



                #region Response
                 return new ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>,List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>>()
                {
                    provinceWise = provinceCitizenGroups,
                    educationalWise= educationGroups,
                    districtWise = districtCitizenGroups,
                    tehsilWise = tehsilCitizenGroups,
                    genderWise = genderGroupsWithPercenntage,
                    maritalStatusWise = maritalStatusGroupsWithPercentage,
                    employementWise = employmentGroups,
                    savingAmountWise = citizenSchemeGroups,
                    citizenTrendWise = citizenTrendGroupslatestSixMonths,
                    remarks = "Success",
                    success = true
                };
                #endregion
            }
            catch (Exception ex)
            {
               return new ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }  
        public async Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolledForApp()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {

                    var dashboardResponseDto = new DashboardDTO
                    {
                        totalCitizenCount = totalCitizensCount,
                        registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null),
                        enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null)
                    };

                    return new ResponseModel<DashboardDTO>()
                    {
                        success = true,
                        remarks = "Citizen counts retrieved successfully.",
                        data = dashboardResponseDto
                    };

                }
                else
                {
                    return new ResponseModel<DashboardDTO>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<DashboardDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

       
    }
}
