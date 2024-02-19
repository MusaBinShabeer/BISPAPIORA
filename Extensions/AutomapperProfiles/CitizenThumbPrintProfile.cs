using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenThumbPrintProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenThumbPrintProfile()
        {
            CreateMap<AddCitizenThumbPrintDTO, tbl_citizen_thumb_print>()
             .ForMember(d => d.citizen_thumb_print_name, opt => opt.MapFrom(src => src.citizenThumbPrintName))
             .ForMember(d => d.citizen_thumb_print_path, opt => opt.MapFrom(src => src.citizenThumbPrintPath))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => src.fkCitizen));
            CreateMap<UpdateCitizenThumbPrintDTO, tbl_citizen_thumb_print>()
             .ForMember(d => d.citizen_thumb_print_id, opt => opt.MapFrom((src, dest) => dest.citizen_thumb_print_id))
             .ForMember(d => d.citizen_thumb_print_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenThumbPrintName) ? src.citizenThumbPrintName : dest.citizen_thumb_print_name))
             .ForMember(d => d.citizen_thumb_print_path, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenThumbPrintPath) ? src.citizenThumbPrintPath : dest.citizen_thumb_print_path))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => src.fkCitizen));
            CreateMap<tbl_citizen_thumb_print, CitizenThumbPrintResponseDTO>()
             .ForMember(d => d.citizenThumbPrintId, opt => opt.MapFrom(src => src.citizen_thumb_print_id))
             .ForMember(d => d.citizenThumbPrintCode, opt => opt.MapFrom(src => src.code))
             .ForMember(d => d.citizenThumbPrintName, opt => opt.MapFrom((src) => src.citizen_thumb_print_name))
             .ForMember(d => d.citizenThumbPrintPath, opt => opt.MapFrom((src) => src.citizen_thumb_print_path))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
