using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class TehsilProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public TehsilProfile()
        {
            CreateMap<AddTehsilDTO, tbl_tehsil>()
             .ForMember(d => d.tehsil_name, opt => opt.MapFrom(src => src.tehsilName))
             .ForMember(d => d.id, opt => opt.MapFrom(src => src.id))
             .ForMember(d => d.district_id, opt => opt.MapFrom(src => src.districtId))
             .ForMember(d => d.fk_district, opt => opt.MapFrom(src => Guid.Parse(src.fkDistrict)))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateTehsilDTO, tbl_tehsil>()
             .ForMember(d => d.tehsil_id, opt => opt.MapFrom((src, dest) => dest.tehsil_id))
             .ForMember(d => d.tehsil_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.tehsilName) ? src.tehsilName : dest.tehsil_name))
             .ForMember(d => d.district_id, opt => opt.MapFrom((src, dest) => otherServices.Check(src.districtId) ? src.districtId : dest.district_id))
             .ForMember(d => d.id, opt => opt.MapFrom((src, dest) => otherServices.Check(src.id) ? src.id : dest.id))
             .ForMember(d => d.fk_district, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkDistrict) ? Guid.Parse(src.fkDistrict) : dest.fk_district))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_tehsil, TehsilResponseDTO>()
            .ForMember(d => d.tehsilId, opt => opt.MapFrom(src => src.tehsil_id))
            .ForMember(d => d.tehsilName, opt => opt.MapFrom((src) => src.tehsil_name))
            .ForMember(d => d.provinceId, opt => opt.MapFrom(src => src.tbl_district.tbl_province.province_id))
            .ForMember(d => d.provinceName, opt => opt.MapFrom((src) => src.tbl_district.tbl_province.province_name))
            .ForMember(d => d.id, opt => opt.MapFrom((src) => src.id))
            .ForMember(d => d.districtId, opt => opt.MapFrom((src) => src.district_id))
            .ForMember(d => d.fkDistrict, opt => opt.MapFrom(src => src.fk_district))
            .ForMember(d => d.districtName, opt => opt.MapFrom((src) => src.tbl_district.district_name))
            .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
