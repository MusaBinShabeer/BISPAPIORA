using AutoMapper;
using AutoMapper.QueryableExtensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.ENUMS;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;

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

        // Method to retrieve user performance statistics for a given user within a specified date range
        public async Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStatsForApp(string userName, string dateStart, string dateEnd)
        {
            try
            {
                // Query to retrieve registered citizens with their registration information
                var registeredCitizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Where(x => x.tbl_citizen_registration != null && x.tbl_enrollment == null)
                    .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);

                // Query to retrieve enrolled citizens with their enrollment information
                var enrolledCitizenQuery = db.tbl_citizens
                   .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                   .Where(x => x.tbl_enrollment != null)
                   .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);

                // Predicates to filter citizens based on userName and date range
                var predicateRegistered = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateRegistered = predicateRegistered.And(x => x.user_name == (userName));
                var predicateEnrolled = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                predicateEnrolled = predicateEnrolled.And(x => x.user_name == (userName));
                if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                {
                    // Adding conditions for date range if provided
                    predicateRegistered = predicateRegistered.And(x => x.registered_date >= DateTime.Parse(dateStart) && x.registered_date <= DateTime.Parse(dateEnd));
                    predicateEnrolled = predicateEnrolled.And(x => x.enrolled_date >= DateTime.Parse(dateStart) && x.enrolled_date <= DateTime.Parse(dateEnd));
                }

                // Fetching registered and enrolled citizens based on predicates
                var registeredCitizen = registeredCitizenQuery.Where(predicateRegistered).ToList();
                var enrolledCitizen = enrolledCitizenQuery.Where(predicateEnrolled).ToList();

                // Returning response model with mapped data and success status
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    data = mapper.Map<DashboardUserPerformanceResponseDTO>((registeredCitizen, enrolledCitizen)),
                    remarks = "Success",
                    success = true
                };
            }
            catch (Exception ex)
            {
                // Handling exceptions and returning error response
                return new ResponseModel<DashboardUserPerformanceResponseDTO>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }

        // Method to retrieve statistics for the web dashboard
        public async Task<ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>> GetWebDashboardStats()
        {
            try
            {
                // Predicates for filtering different types of statistics
                var predicateRegisteredApplicants = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                var predicateEnrolledApplicants = PredicateBuilder.New<DashboardCitizenBaseModel>(true);
                var predicateCitizenSavings = PredicateBuilder.New<DashboardCitizenBaseModel>(true);

                // Query to retrieve citizen information with related data
                var citizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Include(x => x.tbl_citizen_scheme)
                    //.Where(x => x.tbl_citizen_registration != null && x.tbl_enrollment == null)
                    .ProjectTo<DashboardCitizenBaseModel>(mapper.ConfigurationProvider);

                // Applying filters to each statistic type
                predicateRegisteredApplicants = predicateRegisteredApplicants.And(x => x.registration != null /*&& x.enrollment == null*/);
                predicateEnrolledApplicants = predicateEnrolledApplicants.And(x => x.enrollment != null);
                predicateCitizenSavings = predicateCitizenSavings.And(x => x.saving_amount != null);

                // Executing queries to fetch data based on applied predicates
                var registeredCitizen = await citizenQuery.Where(predicateRegisteredApplicants).ToListAsync();
                var enrolledCitizen = await citizenQuery.Where(predicateEnrolledApplicants).ToListAsync();
                var citizensSavings = await citizenQuery.Where(predicateCitizenSavings).ToListAsync();

                // Calculating statistics based on fetched data
                var totalRegisteredCount = registeredCitizen.Count;
                var totalEnrolledCount = enrolledCitizen.Count;
                var totalCitizenSavings = citizensSavings.Select(x => x.saving_amount).Sum();
                var totalCitizenCount = totalRegisteredCount + totalEnrolledCount;

                // Generating response model with mapped statistics and success status
                return new ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>()
                {
                    totalApplicants = mapper.Map<WebDashboardStats>(("Total Applicants", totalRegisteredCount)),
                    totalEnrollments = mapper.Map<WebDashboardStats>(("Total Enrollments", totalEnrolledCount)),
                    totalSavings = mapper.Map<WebDashboardStats>(("Total Savings", totalCitizenSavings)),
                    remarks = "Success",
                    success = true
                };
            }
            catch (Exception ex)
            {
                // Handling exceptions and returning error response
                return new ResponseModel<WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats, WebDashboardStats>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }

        // Method to retrieve statistics for the web dashboard
        public async Task<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>> GetWebDesktopApplicantDistributionLocationBased(string dateStart, string dateEnd, string provinceId, string districtId, string tehsilId, bool registration, bool enrollment)
        {
            try
            {
                #region Predicates
                // Initializing predicates for filtering data
                var predicateDistrict = PredicateBuilder.New<tbl_district>(true);
                var predicateTehsil = PredicateBuilder.New<tbl_tehsil>(true);
                var predicateCitizen = PredicateBuilder.New<DashboardCitizenLocationModel>(true);
                var predicateRegisteredCitizen= PredicateBuilder.New<DashboardCitizenLocationModel>(true);
                var predicateEnrolledCitizen= PredicateBuilder.New<DashboardCitizenLocationModel>(true);
                #endregion

                // Query to fetch citizens with related data
                var totalCitizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .Include(x => x.tbl_citizen_education)
                    .Include(x => x.tbl_citizen_employment)
                    .Include(x => x.tbl_citizen_scheme)
                    .ProjectTo<DashboardCitizenLocationModel>(mapper.ConfigurationProvider);

                #region Filters
                predicateRegisteredCitizen = predicateRegisteredCitizen.And(x => x.registration != null && x.enrollment == null);
                predicateEnrolledCitizen = predicateEnrolledCitizen.And(x =>x.enrollment != null);
                // Applying filters based on registration and enrollment statuses
                if (registration == true)
                {
                    predicateCitizen = predicateCitizen.And(x => x.registration != null && x.enrollment == null);
                    if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                    {
                        // Applying date range filter for registration
                        predicateCitizen = predicateCitizen.And(x => x.registered_date >= DateTime.Parse(dateStart) && x.registered_date <= DateTime.Parse(dateEnd));
                        
                    }
                }
                else if (enrollment == true)
                {
                    predicateCitizen = predicateCitizen.And(x => x.enrollment != null);
                    if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                    {
                        // Applying date range filter for enrollment
                        predicateCitizen = predicateCitizen.And(x => x.enrolled_date >= DateTime.Parse(dateStart) && x.enrolled_date <= DateTime.Parse(dateEnd));
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                    {
                        // Applying date range filter for enrollment
                        predicateCitizen = predicateCitizen.And(x => x.insertion_date >= DateTime.Parse(dateStart) && x.insertion_date <= DateTime.Parse(dateEnd));
                        predicateRegisteredCitizen = predicateRegisteredCitizen.And(x => x.insertion_date >= DateTime.Parse(dateStart) && x.insertion_date <= DateTime.Parse(dateEnd));
                        predicateEnrolledCitizen = predicateEnrolledCitizen.And(x => x.insertion_date >= DateTime.Parse(dateStart) && x.insertion_date <= DateTime.Parse(dateEnd));
                    }
                }

                // Applying filters based on selected location (province, district, tehsil)
                if (!string.IsNullOrEmpty(provinceId))
                {
                    if (!string.IsNullOrEmpty(districtId))
                    {
                        if (!string.IsNullOrEmpty(tehsilId))
                        {
                            // Applying filter for specific tehsil
                            predicateCitizen = predicateCitizen.And(x => x.tehsil_id == Guid.Parse(tehsilId));
                            predicateTehsil = predicateTehsil.And(x => x.tehsil_id == Guid.Parse(tehsilId));
                        }
                        else
                        {
                            // Applying filter for specific district
                            predicateCitizen = predicateCitizen.And(x => x.district_id == Guid.Parse(districtId));
                            predicateDistrict = predicateDistrict.And(x => x.district_id == Guid.Parse(districtId));
                            predicateTehsil = predicateTehsil.And(x => x.fk_district == Guid.Parse(districtId));
                        }
                    }
                    else
                    {
                        // Applying filter for specific province
                        predicateCitizen = predicateCitizen.And(x => x.province_id == Guid.Parse(provinceId));
                        predicateDistrict = predicateDistrict.And(x => x.fk_province == Guid.Parse(provinceId));
                        predicateTehsil = predicateTehsil.And(x => x.tbl_district.fk_province == Guid.Parse(provinceId));
                    }
                }

                // Fetching filtered citizens
                var filteredCitizens = await totalCitizenQuery.Where(predicateCitizen).ToListAsync();
                var onlyRegisteredCitizens = await totalCitizenQuery.Where(predicateRegisteredCitizen).ToListAsync();              
                var onlyEnrolledCitizens = await totalCitizenQuery.Where(predicateEnrolledCitizen).ToListAsync();
                double onlyRegisteredCitizensPercentage = 0.0;
                double onlyEnrolledCitizensPercentage = 0.0;
                if(onlyRegisteredCitizens.Count > 0) 
                {
                    onlyRegisteredCitizensPercentage = (double)onlyRegisteredCitizens.Count() / filteredCitizens.Count * 100;
                }
                if (onlyEnrolledCitizens.Count() > 0)
                {
                    onlyEnrolledCitizensPercentage = (double)onlyEnrolledCitizens.Count() / filteredCitizens.Count * 100;
                }
                #endregion

                #region Citizen Count Wise
                // Create a list to store WebDashboardStats objects
                List<WebDashboardStats> citizenStats = new List<WebDashboardStats>();
                citizenStats.Add(new WebDashboardStats()
                {
                    //StatCount = filteredCitizens.Count() >0?filteredCitizens.Count() / filteredCitizens.Count * 100:0,
                    StatName = "Compliant"
                }); 
                citizenStats.Add(new WebDashboardStats()
                {
                    StatCount = onlyRegisteredCitizensPercentage,
                    StatName = "Registered Only"
                }); 
                citizenStats.Add(new WebDashboardStats()
                {
                    StatCount = onlyEnrolledCitizensPercentage,
                    StatName = "Enrolled Only"
                });
                citizenStats.Add(new WebDashboardStats()
                {
                    //StatCount = filteredCitizens.Count(citizen => citizen.enrollment != null) > 0 ? filteredCitizens.Count(citizen => citizen.enrollment != null) / filteredCitizens.Count * 100 : 0,
                    StatName = "Subscribed"
                });
                citizenStats.Add(new WebDashboardStats()
                {
                    //StatCount = filteredCitizens.Count(citizen => citizen.enrollment != null) > 0 ? filteredCitizens.Count(citizen => citizen.enrollment != null) / filteredCitizens.Count * 100 : 0,
                    StatName = "Unsubscribed"
                });
                #endregion

                #region Location Base Group By
                #region Province Query
                // Fetching provinces
                var provinces = await db.tbl_provinces.ToListAsync();

                // Grouping citizens by province
                var provinceCitizenGroups = provinces.Select(province =>
                {
                    // Filtering citizens for the current province
                    var provinceFilteredCitizens = filteredCitizens.Where(citizen => citizen.province_id == province.province_id).ToList();
                    var provinceCitizenCount = provinceFilteredCitizens.Count;
                    var citizenPercentage = provinceCitizenCount > 0 ? (double)provinceCitizenCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for province citizen count and percentage
                    return new DashboardProvinceCitizenCountPercentageDTO
                    {
                        provinceName = province.province_name,
                        citizenPercentage = citizenPercentage
                    };
                }).ToList();
                #endregion

                #region District Query
                // Fetching districts
                var districtsQuery = db.tbl_districts.Include(x => x.tbl_province).AsQueryable();
                var districts = await districtsQuery.Where(predicateDistrict).ToListAsync();

                // Grouping citizens by district
                var districtCitizenGroups = districts.Select(district =>
                {
                    // Filtering citizens for the current district
                    var districtFilteredCitizens = filteredCitizens.Where(citizen => citizen.district_name.ToLower() == district.district_name.ToLower()).ToList();
                    var districtCitizenCount = districtFilteredCitizens.Count;
                    var citizenPercentage = districtCitizenCount > 0 ? (double)districtCitizenCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for district citizen count and percentage
                    return new DashboardDistrictCitizenCountPercentageDTO
                    {
                        districtName = district.district_name,
                        provinceName = district.tbl_province.province_name,
                        citizenPercentage = citizenPercentage
                    };
                }).ToList();
                #endregion

                #region Tehsil Query
                // Fetching tehsils
                var tehsilsQuery = db.tbl_tehsils.Include(x => x.tbl_district).ThenInclude(x => x.tbl_province).AsQueryable();
                var tehsils = await tehsilsQuery.Where(predicateTehsil).ToListAsync();

                // Grouping citizens by tehsil
                var tehsilCitizenGroups = tehsils.Select(tehsil =>
                {
                    // Filtering citizens for the current tehsil
                    var tehsilFilteredCitizens = filteredCitizens.Where(citizen => citizen.tehsil_name.ToLower() == tehsil.tehsil_name.ToLower()).ToList();
                    var tehsilCitizenCount = tehsilFilteredCitizens.Count;
                    var citizenPercentage = tehsilCitizenCount > 0 ? (double)tehsilCitizenCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for tehsil citizen count and percentage
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

                #region Citizen Gender-Based Region Applicant Distribution
                // Grouping citizens by gender
                var genderListEnum = Enum.GetValues(typeof(GenderEnum)).Cast<GenderEnum>().ToList();

                // Calculating percentage for each gender group
                var genderGroupsWithPercenntage = genderListEnum.Select(status =>
                {
                    var group = filteredCitizens.Where(c => c.citizen_gender == status.ToString());
                    var statusCount = group.Count();
                    var citizenPercentage = statusCount > 0 ? (double)statusCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for marital status-wise distribution
                    return new DashboardCitizenGenderPercentageDTO
                    {
                        citizenGender = status.ToString(),
                        citizenGenderPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Marital_Status-Based Region Applicant Distribution
                // Grouping citizens by marital status
                var maritalListEnums = Enum.GetValues(typeof(MartialStatusEnum)).Cast<MartialStatusEnum>().ToList();

                // Calculating percentage for each marital status group
                var maritalStatusGroupsWithPercentage = maritalListEnums.Select(status =>
                {
                    var group = filteredCitizens.Where(c => c.citizen_martial_status == status.ToString());
                    var statusCount = group.Count();
                    var citizenPercentage = statusCount > 0 ? (double)statusCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for marital status-wise distribution
                   return  new DashboardCitizenMaritalStatusPercentageDTO
                    {
                        citizenMaritalStatus = status.ToString(),
                        citizenMaritalStatusPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Educational Background Group By
                // Fetching educations
                var educations = await db.tbl_educations.ToListAsync();

                // Grouping citizens by educational background
                var educationGroups = educations.Select(education =>
                {
                    // Filtering citizens by educational background
                    var citizensGroupedByEducation = filteredCitizens.Where(citizen => citizen.educationId == education.education_id).ToList();
                    var citizensGroupedByEducationCount = citizensGroupedByEducation.Count;
                    var citizenPercentage = citizensGroupedByEducationCount > 0 ? (double)citizensGroupedByEducationCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for educational background-wise distribution
                    return new DashboardCitizenEducationalPercentageStatDTO
                    {
                        educationalBackground = education.education_name,
                        educationalBackgroundPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Employement Background Group By
                // Fetching employments
                var employments = await db.tbl_employments.ToListAsync();

                // Grouping citizens by employment background
                var employmentGroups = employments.Select(employment =>
                {
                    // Filtering citizens by employment background
                    var citizensGroupedByEmployment = filteredCitizens.Where(citizen => citizen.employmentId == employment.employment_id).ToList();
                    var citizensGroupedByEmploymentCount = citizensGroupedByEmployment.Count;
                    var citizenPercentage = citizensGroupedByEmploymentCount > 0 ? (double)citizensGroupedByEmploymentCount / filteredCitizens.Count * 100 : 0;

                    // Creating DTO for employment background-wise distribution
                    return new DashboardCitizenEmploymentPercentageStatDTO
                    {
                        employmentBackground = employment.employment_name,
                        employmentBackgroundPercentage = citizenPercentage,
                    };
                }).ToList();
                #endregion

                #region Citizen Group By Scheme Saving Amount
                // Fetching citizen schemes
                var savingAmountEnum = Enum.GetValues(typeof(SavingAmountEnum))
                                 .Cast<int>();

               // Grouping citizens by scheme saving amount
                var citizenSchemeGroups = savingAmountEnum.Select(savingAmount =>
                {
                    var savingAmountName = (SavingAmountEnum)Enum.Parse(typeof(SavingAmountEnum), savingAmount.ToString());
                    var x = savingAmountName.ToString().Split('A');
                    // Filtering citizens by scheme saving amount
                    var citizensGroupedBySavingAmount = filteredCitizens.Where(citizen => citizen.saving_amount == decimal.Parse(x[1])).ToList();
                    var citizensGroupedBySavingAmountCount = citizensGroupedBySavingAmount.Count;
                    // Creating DTO for scheme saving amount-wise distribution
                    return new DashboardCitizenCountSavingAmountDTO
                    {
                        totalCitizenCount = citizensGroupedBySavingAmountCount,
                        savingAmount = Decimal.Parse(x[1]),
                    };
                }).GroupBy(citizenScheme => citizenScheme.savingAmount)
                .Select(group => group.First())
                .ToList();
                #endregion

                #region Citizen Trend
                // Grouping citizens by insertion month
                var citizenTrendGroups = filteredCitizens.Where(citizen => citizen.insertion_date != null) // Filter out citizens with null insertion_date
                                                         .GroupBy(citizen => new DateTime(citizen.insertion_date!.Value.Year, citizen.insertion_date.Value.Month, 1)) // Grouping citizens by insertion month
                                                         .Select(group =>
                                                         {
                                                             var insertionMonth = new DateTime(group.Key.Year, group.Key.Month, 1); // The insertion month for the current group
                                                             var citizenCount = group.Count(); // Counting the number of citizens for the current insertion month
                                                             return new DashboardCitizenTrendDTO
                                                             {
                                                                 insertionMonth = insertionMonth.ToString("MM/dd/yyyy HH:mm:ss"), // Format the insertionMonth
                                                                 totalCitizenCount = citizenCount
                                                             };
                                                         })
                                                         .ToList();

                // Sort the citizenTrendGroups list by InsertionMonth in descending order
                citizenTrendGroups = citizenTrendGroups.OrderByDescending(x => DateTime.ParseExact(x.insertionMonth, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)).ToList();

                // Select the latest 6 months
                
                var citizenTrendGroupslatestSixMonths = citizenTrendGroups.Take(6).ToList();
                
                #endregion

                #region Response
                // Returning response model with filtered and mapped statistics
                return new ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>()
                {
                    provinceWise = provinceCitizenGroups,
                    educationalWise = educationGroups,
                    districtWise = districtCitizenGroups,
                    tehsilWise = tehsilCitizenGroups,
                    genderWise = genderGroupsWithPercenntage,
                    maritalStatusWise = maritalStatusGroupsWithPercentage,
                    employementWise = employmentGroups,
                    savingAmountWise = citizenSchemeGroups,
                    citizenTrendWise = citizenTrendGroupslatestSixMonths,
                    citizenCountWise = citizenStats,
                    remarks = "Success",
                    success = true
                };
                #endregion
            }
            catch (Exception ex)
            {
                // Handling exception and returning error response
                return new ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>, List<WebDashboardStats>>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }

        // Retrieves the total count of citizens and the count of citizens who are registered and enrolled in the application.
        public async Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolledForApp()
        {
            try
            {
                // Retrieve all citizens including their registration and enrollment details
                var totalCitizens = await db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration)
                    .Include(x => x.tbl_enrollment)
                    .ToListAsync();

                // Count the total number of citizens
                var totalCitizensCount = totalCitizens.Count();

                // Check if there are any citizens
                if (totalCitizensCount > 0)
                {
                    // Calculate the count of registered citizens and enrolled citizens
                    var registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null);
                    var enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null);

                    // Create a DashboardDTO object with the counts
                    var dashboardResponseDto = new DashboardDTO
                    {
                        totalCitizenCount = totalCitizensCount,
                        registeredCount = registeredCount,
                        enrolledCount = enrolledCount
                    };

                    // Return a success response with the DashboardDTO object
                    return new ResponseModel<DashboardDTO>()
                    {
                        success = true,
                        remarks = "Citizen counts retrieved successfully.",
                        data = dashboardResponseDto
                    };
                }
                else
                {
                    // Return a failure response if there are no citizens
                    return new ResponseModel<DashboardDTO>()
                    {
                        success = false,
                        remarks = "There are no citizens."
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response if there is an exception
                return new ResponseModel<DashboardDTO>()
                {
                    success = false,
                    remarks = $"There was a fatal error: {ex.Message}"
                };
            }
        }

    }
}
