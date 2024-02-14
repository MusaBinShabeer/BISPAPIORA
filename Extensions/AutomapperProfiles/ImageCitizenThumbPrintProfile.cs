using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenThumbPrintDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class ImageCitizenThumbPrintProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public ImageCitizenThumbPrintProfile()
        {
            CreateMap<AddImageCitizenThumbPrintDTO, tbl_image_citizen_thumb_print>()
                .ForMember(d => d.name, opt => opt.MapFrom(src => src.imageCitizenThumbPrintName))
                .ForMember(d => d.data, opt => opt.MapFrom(src => src.imageCitizenThumbPrintData))
                .ForMember(d => d.content_type, opt => opt.MapFrom(src => src.imageCitizenThumbPrintContentType))
                .ForMember(d => d.cnic, opt => opt.MapFrom(src => src.imageCitizenThumbPrintCnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateImageCitizenThumbPrintDTO, tbl_image_citizen_thumb_print>()
                .ForMember(d => d.id, opt => opt.MapFrom((src, dest) => dest.id))
                .ForMember(d => d.name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintName) ? src.imageCitizenThumbPrintName : dest.name))
                .ForMember(d => d.data, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintData) ? src.imageCitizenThumbPrintData : dest.data))
                .ForMember(d => d.content_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintContentType) ? src.imageCitizenThumbPrintContentType : dest.content_type))
                .ForMember(d => d.cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintCnic) ? src.imageCitizenThumbPrintCnic : dest.cnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_image_citizen_thumb_print, ImageCitizenThumbPrintResponseDTO>()
                .ForMember(d => d.imageCitizenThumbPrintId, opt => opt.MapFrom(src => src.id))
                .ForMember(d => d.imageCitizenThumbPrintData, opt => opt.MapFrom((src) => src.name))
                .ForMember(d => d.imageCitizenThumbPrintData, opt => opt.MapFrom((src) => src.data))
                .ForMember(d => d.imageCitizenThumbPrintContentType, opt => opt.MapFrom((src) => src.content_type))
                .ForMember(d => d.imageCitizenThumbPrintCnic, opt => opt.MapFrom((src) => src.cnic))
                .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen));
                //.ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
                //.ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
