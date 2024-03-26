﻿using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DashboardDTO;
using AutoMapper;
using LinqKit;

namespace BISPAPIORA.Repositories.DashboardServicesRepo
{
    public interface IDashboardServices
    {
        public Task<ResponseModel<DashboardUserPerformanceResponseDTO>> GetUserPerformanceStats(string userIdstring, string dateStart, string dateEnd);
        public Task<ResponseModel<DashboardCitizenCountPercentageDTO>> GetWebDesktopApplicantDistribution();
        public Task<ResponseModel<List<DashboardProvinceCitizenCountPercentageDTO>, List<DashboardDistrictCitizenCountPercentageDTO>, List<DashboardTehsilCitizenCountPercentageDTO>, List<DashboardCitizenEducationalPercentageStatDTO>, List<DashboardCitizenGenderPercentageDTO>, List<DashboardCitizenMaritalStatusPercentageDTO>, List<DashboardCitizenEmploymentPercentageStatDTO>, List<DashboardCitizenCountSavingAmountDTO>, List<DashboardCitizenTrendDTO>>> GetWebDesktopApplicantDistributionLocationBased(string dateStart, string dateEnd, string provinceId, string districtId, string tehsilId, bool registration, bool enrollment);
        public Task<ResponseModel<List<TehsilStatusResponseDTO>>> GetTehsilStatusResponses();
        public Task<ResponseModel<List<DistrictStatusResponseDTO>>> GetDistrictStatusResponses();
        public Task<ResponseModel<List<ProvinceStatusResponseDTO>>> GetProvinceStatusResponses();
        public Task<ResponseModel<DashboardDTO>> GetTotalCitizenAndEnrolled();
        //public Task<ResponseModel<DashboardDTO>> GetTotalCompliantApplicants();
        //public Task<ResponseModel<DashboardDTO>> GetTotalSavings();
        //public Task<ResponseModel<DashboardDTO>> GetTotalMatchingGrants();
    }
}
