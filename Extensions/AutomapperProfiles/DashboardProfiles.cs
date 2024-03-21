using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.DashboardDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class DashboardProfiles : Profile
    {
        private readonly OtherServices otherServices = new();
        public DashboardProfiles()
        {
            CreateMap<tbl_citizen, DashboardCitizenBaseModel>()
                .ForMember(d => d.registered_date, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_date : default!))
                .ForMember(d => d.enrolled_date, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_date : default!))
                .ForMember(d => d.enrolled_by, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_id : default!))
                .ForMember(d => d.registered_by, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_id : default!))
                .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizen_name))
                .ForMember(d => d.user_name, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_name : src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_name : ""))
                .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id));
            CreateMap<tbl_citizen, DashboardCitizenLocationModel>()
                .ForMember(d => d.registered_date, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_date : default!))
                .ForMember(d => d.enrolled_date, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_date : default!))
                .ForMember(d => d.enrolled_by, opt => opt.MapFrom(src => src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_id : default!))
                .ForMember(d => d.registered_by, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_id : default!))
                .ForMember(d => d.citizen_name, opt => opt.MapFrom(src => src.citizen_name))
                .ForMember(d => d.user_name, opt => opt.MapFrom(src => src.tbl_citizen_registration != null ? src.tbl_citizen_registration.registered_by.user_name : src.tbl_enrollment != null ? src.tbl_enrollment.enrolled_by.user_name : ""))
                .ForMember(d => d.citizen_id, opt => opt.MapFrom(src => src.citizen_id))
                .ForMember(d => d.province_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.tbl_province.province_name : ""))
                .ForMember(d => d.province_id, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.tbl_district.fk_province))
                .ForMember(d => d.district_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tbl_district.district_name : ""))
                .ForMember(d => d.district_id, opt => opt.MapFrom(src => src.tbl_citizen_tehsil.fk_district))
                .ForMember(d => d.tehsil_name, opt => opt.MapFrom(src => src.tbl_citizen_tehsil != null ? src.tbl_citizen_tehsil.tehsil_name : ""))
                .ForMember(d => d.tehsil_id, opt => opt.MapFrom(src => src.fk_tehsil));
            CreateMap<(List<DashboardCitizenBaseModel>, List<DashboardCitizenBaseModel>), DashboardUserPerformanceResponseDTO>()
                .ForMember(dest => dest.registeredCount, opt => opt.MapFrom(src => src.Item1.Count))
                .ForMember(dest => dest.enrolledCount, opt => opt.MapFrom(src => src.Item2.Count));
            CreateMap<(double registeredPerc, double enrolledPerc), DashboardCitizenCountPercentageDTO>()
                .ForMember(dest => dest.registeredPercentage, opt => opt.MapFrom(src => src.registeredPerc))
                .ForMember(dest => dest.enrolledPercentage, opt => opt.MapFrom(src => src.enrolledPerc));
            CreateMap<DashboardDistrictCitizenCountPercentageDTO, DashboardTehsilCitizenCountPercentageDTO>()
                .ForMember(dest => dest.tehsilName, opt => opt.Ignore())
                .ForMember(dest => dest.districtName, opt => opt.MapFrom(src => src.districtName))
                .ForMember(dest => dest.provinceName, opt => opt.MapFrom(src => src.provinceName))
                .ForMember(dest => dest.citizenPercentage, opt => opt.MapFrom(src => src.citizenPercentage));
            CreateMap<DashboardProvinceCitizenCountPercentageDTO, DashboardTehsilCitizenCountPercentageDTO>()
                .ForMember(dest => dest.tehsilName, opt => opt.Ignore())
                .ForMember(dest => dest.districtName, opt => opt.Ignore())
                .ForMember(dest => dest.provinceName, opt => opt.MapFrom(src => src.provinceName))
                .ForMember(dest => dest.citizenPercentage, opt => opt.MapFrom(src => src.citizenPercentage));

        }
    }
}
