using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class ProvinceProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public ProvinceProfile()
        {
            CreateMap<AddProvinceDTO, tbl_province>()
             .ForMember(d => d.province_name, opt => opt.MapFrom(src => src.provinceName))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateProvinceDTO, tbl_province>()
             .ForMember(d => d.province_id, opt => opt.MapFrom((src, dest) => dest.province_id))
             .ForMember(d => d.province_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.provinceName) ? src.provinceName : dest.province_name))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_province, ProvinceResponseDTO>()
            .ForMember(d => d.provinceId, opt => opt.MapFrom(src => src.province_id))
            .ForMember(d => d.provinceName, opt => opt.MapFrom((src) => src.province_name))
            .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
