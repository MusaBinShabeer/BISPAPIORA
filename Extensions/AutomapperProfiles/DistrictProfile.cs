using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DistrictDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class DistrictProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public DistrictProfile()
        {
            CreateMap<AddDistrictDTO, tbl_district>()
             .ForMember(d => d.district_name, opt => opt.MapFrom(src => src.districtName))
             .ForMember(d => d.fk_province, opt => opt.MapFrom(src => Guid.Parse(src.fkProvince)))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateDistrictDTO, tbl_district>()
             .ForMember(d => d.district_id, opt => opt.MapFrom((src, dest) => dest.district_id))
             .ForMember(d => d.district_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.districtName) ? src.districtName : dest.district_name))
             .ForMember(d => d.fk_province, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkProvince) ? Guid.Parse(src.fkProvince) : dest.fk_province))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_district, DistrictResponseDTO>()
            .ForMember(d => d.districtId, opt => opt.MapFrom(src => src.district_id))
            .ForMember(d => d.districtName, opt => opt.MapFrom((src) => src.district_name))
            .ForMember(d => d.fkProvince, opt => opt.MapFrom(src => src.fk_province))
            .ForMember(d => d.provinceName, opt => opt.MapFrom((src) => src.tbl_province.province_name))
            .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
