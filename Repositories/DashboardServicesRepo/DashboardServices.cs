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
        public async Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userName, string dateStart, string dateEnd)
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

        public async Task<ResponseModel<DashboardCitizenCountPercentageDTO>> GetWebDesktopApplicantDistribution(string userName, string dateStart, string dateEnd)
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

                var totalRegisteredCount = registeredCitizen.Count;
                var totalEnrolledCount = enrolledCitizen.Count;

                var totalCitizenCount = totalRegisteredCount + totalEnrolledCount;

                var registeredPercentage = totalCitizenCount > 0 ? (double)totalRegisteredCount / totalCitizenCount * 100 : 0;
                var enrolledPercentage = totalCitizenCount > 0 ? (double)totalEnrolledCount / totalCitizenCount * 100 : 0;


                return new ResponseModel<DashboardCitizenCountPercentageDTO>()
                {
                    data = mapper.Map<DashboardCitizenCountPercentageDTO>((registeredPercentage, enrolledPercentage)),
                    remarks = "Success",
                    success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<DashboardCitizenCountPercentageDTO>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }

        public async Task<ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>> GetWebDesktopApplicantDistributionLocationBased(string userName, string dateStart, string dateEnd, string provinceName, string districtName, string tehsilname)
        {
            try
            {
                var totalCitizenQuery = db.tbl_citizens
                    .Include(x => x.tbl_citizen_registration).ThenInclude(x => x.registered_by)
                    .Include(x => x.tbl_enrollment).ThenInclude(x => x.enrolled_by)
                    .Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province)
                    .ProjectTo<DashboardCitizenLocationModel>(mapper.ConfigurationProvider);

                var predicateRegistered = PredicateBuilder.New<DashboardCitizenLocationModel>(true);
                predicateRegistered = predicateRegistered.And(x => x.user_name == (userName));

                if (!string.IsNullOrEmpty(dateStart) && !string.IsNullOrEmpty(dateEnd))
                {
                    predicateRegistered = predicateRegistered.And(x => x.registered_date >= DateTime.Parse(dateStart) && x.registered_date <= DateTime.Parse(dateEnd));
                }

                if (!string.IsNullOrEmpty(districtName))
                {
                    predicateRegistered = predicateRegistered.And(x => x.province_name.ToLower() == provinceName.ToLower() && x.district_name.ToLower() == districtName.ToLower());
                }
                else if (!string.IsNullOrEmpty(provinceName))
                {
                    predicateRegistered = predicateRegistered.And(x => x.province_name.ToLower() == provinceName.ToLower());
                }

                var CitizensCount = totalCitizenQuery.Where(predicateRegistered).ToList().Count();
                var filteredCitizens = totalCitizenQuery.Where(predicateRegistered).ToList();
                var totalCitizensInDistrict = CitizensCount;

                if (!string.IsNullOrEmpty(districtName))
                {
                    var tehsilCitizenGroups = filteredCitizens
                    .GroupBy(c => c.tehsil_name)
                    .Select(group => new DashboardTehsilCitizenCountPercentageDTO
                    {
                        provinceName = group.FirstOrDefault()?.province_name ?? string.Empty,
                        districtName = districtName,
                        tehsilName = group.Key,
                        citizenPercentage = totalCitizensInDistrict > 0 ? (double)group.Count() / totalCitizensInDistrict * 100 : 0
                    })
                    .ToList();

                    return new ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>()
                    {
                        data = tehsilCitizenGroups,
                        remarks = "Success",
                        success = true
                    };
                }
                else if (!string.IsNullOrEmpty(provinceName))
                {
                    var districtCitizenGroups = filteredCitizens
                    .GroupBy(c => c.district_name)
                    .Select(group => new DashboardDistrictCitizenCountPercentageDTO
                    {
                        provinceName = group.FirstOrDefault()?.province_name ?? string.Empty,
                        districtName = group.Key,
                        citizenPercentage = totalCitizensInDistrict > 0 ? (double)group.Count() / totalCitizensInDistrict * 100 : 0
                    })
                    .ToList();

                    return new ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>()
                    {
                        data = mapper.Map<List<DashboardTehsilCitizenCountPercentageDTO>>((districtCitizenGroups)),
                        remarks = "Success",
                        success = true
                    };
                }
                else
                {

                    var provinceCitizenGroups = filteredCitizens
                    .GroupBy(c => c.province_name)
                    .Select(group => new DashboardProvinceCitizenCountPercentageDTO
                    {
                        provinceName = group.Key,
                        citizenPercentage = totalCitizensInDistrict > 0 ? (double)group.Count() / totalCitizensInDistrict * 100 : 0
                    })
                    .ToList();

                    return new ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>()
                    {
                        data = mapper.Map<List<DashboardTehsilCitizenCountPercentageDTO>>((provinceCitizenGroups)),
                        remarks = "Success",
                        success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<DashboardTehsilCitizenCountPercentageDTO>>()
                {
                    remarks = $"There was a fatal error {ex.ToString()}",
                    success = false,
                };
            }
        }

        public async Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var tehsils = await db.tbl_tehsils.ToListAsync();
                    var tehsilStatusResponseDTOs = tehsils.Select(tehsil => new TehsilStatusResponseDTO
                    {
                        tehsilName = tehsil.tehsil_name,
                        totalCitizenCount = totalCitizens.Count(citizen => citizen.fk_tehsil == tehsil.tehsil_id),
                        registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null && citizen.fk_tehsil == tehsil.tehsil_id),
                        enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null && citizen.fk_tehsil == tehsil.tehsil_id)
                    }).ToList();

                    return new ResponseModel<List<TehsilStatusResponseDTO>>()
                    {
                        success = true,
                        remarks = "Citizen counts based on tehsil retrieved successfully.",
                        data = tehsilStatusResponseDTOs
                    };

                }
                else
                {
                    return new ResponseModel<List<TehsilStatusResponseDTO>>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<TehsilStatusResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        public async Task<ResponseModel<List<DistrictStatusResponseDTO>>> GetDistrictStatusResponses()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).Include(x => x.tbl_citizen_tehsil).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var districts = await db.tbl_districts.ToListAsync();
                    var districtStatusResponseDTOs = districts.Select(district => new DistrictStatusResponseDTO
                    {
                        districtName = district.district_name,
                        totalCitizenCount = totalCitizens.Count(citizen => citizen.tbl_citizen_tehsil.fk_district == district.district_id),
                        registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null && citizen.tbl_citizen_tehsil.fk_district == district.district_id),
                        enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null && citizen.tbl_citizen_tehsil.fk_district == district.district_id)
                    }).ToList();

                    return new ResponseModel<List<DistrictStatusResponseDTO>>()
                    {
                        success = true,
                        remarks = "Citizen counts based on District retrieved successfully.",
                        data = districtStatusResponseDTOs
                    };

                }
                else
                {
                    return new ResponseModel<List<DistrictStatusResponseDTO>>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<DistrictStatusResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        public async Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses()
        {
            try
            {
                var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).Include(x => x.tbl_citizen_tehsil).ThenInclude(x => x.tbl_district).ToListAsync();
                var totalCitizensCount = totalCitizens.Count();
                if (totalCitizensCount > 0)
                {
                    var provinces = await db.tbl_provinces.ToListAsync();
                    var provinceStatusResponseDTOs = provinces.Select(province => new ProvinceStatusResponseDTO
                    {
                        provinceName = province.province_name,
                        totalCitizenCount = totalCitizens.Count(citizen => citizen.tbl_citizen_tehsil.tbl_district.fk_province == province.province_id),
                        registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null && citizen.tbl_citizen_tehsil.tbl_district.fk_province == province.province_id),
                        enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null && citizen.tbl_citizen_tehsil.tbl_district.fk_province == province.province_id)
                    }).ToList();

                    return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                    {
                        success = true,
                        remarks = "Citizen counts based on Province retrieved successfully.",
                        data = provinceStatusResponseDTOs
                    };

                }
                else
                {
                    return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                    {
                        success = false,
                        remarks = "There is no citizen."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ProvinceStatusResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        public async Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolled()
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

        //public async Task<ResponseModel<DashboardDTO>> GetTotalCompliantApplicants()
        //{
        //    try
        //    {
        //        var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).ToListAsync();
        //        var totalCitizensCount = totalCitizens.Count();
        //        if (totalCitizensCount > 0)
        //        {

        //            var dashboardResponseDto = new DashboardDTO
        //            {
        //                totalCitizenCount = totalCitizensCount,
        //                registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null),
        //                enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null)
        //            };

        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = true,
        //                remarks = "Citizen counts retrieved successfully.",
        //                data = dashboardResponseDto
        //            };

        //        }
        //        else
        //        {
        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = false,
        //                remarks = "There is no citizen."
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseModel<DashboardDTO>()
        //        {
        //            success = false,
        //            remarks = $"There Was Fatal Error {ex.Message.ToString()}"
        //        };
        //    }
        //}

        //public async Task<ResponseModel<DashboardDTO>> GetTotalSavings()
        //{
        //    try
        //    {
        //        var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).ToListAsync();
        //        var totalCitizensCount = totalCitizens.Count();
        //        if (totalCitizensCount > 0)
        //        {

        //            var dashboardResponseDto = new DashboardDTO
        //            {
        //                totalCitizenCount = totalCitizensCount,
        //                registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null),
        //                enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null)
        //            };

        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = true,
        //                remarks = "Citizen counts retrieved successfully.",
        //                data = dashboardResponseDto
        //            };

        //        }
        //        else
        //        {
        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = false,
        //                remarks = "There is no citizen."
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseModel<DashboardDTO>()
        //        {
        //            success = false,
        //            remarks = $"There Was Fatal Error {ex.Message.ToString()}"
        //        };
        //    }
        //}

        //public async Task<ResponseModel<DashboardDTO>> GetTotalMatchingGrants()
        //{
        //    try
        //    {
        //        var totalCitizens = await db.tbl_citizens.Include(x => x.tbl_citizen_registration).Include(x => x.tbl_enrollment).ToListAsync();
        //        var totalCitizensCount = totalCitizens.Count();
        //        if (totalCitizensCount > 0)
        //        {

        //            var dashboardResponseDto = new DashboardDTO
        //            {
        //                totalCitizenCount = totalCitizensCount,
        //                registeredCount = totalCitizens.Count(citizen => citizen.tbl_citizen_registration != null && citizen.tbl_enrollment == null),
        //                enrolledCount = totalCitizens.Count(citizen => citizen.tbl_enrollment != null)
        //            };

        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = true,
        //                remarks = "Citizen counts retrieved successfully.",
        //                data = dashboardResponseDto
        //            };

        //        }
        //        else
        //        {
        //            return new ResponseModel<DashboardDTO>()
        //            {
        //                success = false,
        //                remarks = "There is no citizen."
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseModel<DashboardDTO>()
        //        {
        //            success = false,
        //            remarks = $"There Was Fatal Error {ex.Message.ToString()}"
        //        };
        //    }
        //}
    }
}
