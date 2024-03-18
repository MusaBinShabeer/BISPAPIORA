using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class ImageCitizenFingerPrintProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public ImageCitizenFingerPrintProfile()
        {
            CreateMap<AddImageCitizenFingerPrintDTO, tbl_image_citizen_finger_print>()
                .ForMember(d => d.finger_print_name, opt => opt.MapFrom(src => src.imageCitizenFingerPrintName))
                .ForMember(d => d.finger_print_data, opt => opt.MapFrom(src => src.imageCitizenFingerPrintData))
                .ForMember(d => d.finger_print_content_type, opt => opt.MapFrom(src => src.imageCitizenFingerPrintContentType))
                .ForMember(d => d.thumb_print_name, opt => opt.MapFrom(src => src.imageCitizenThumbPrintName))
                .ForMember(d => d.thumb_print_data, opt => opt.MapFrom(src => src.imageCitizenThumbPrintData))
                .ForMember(d => d.thumb_print_content_type, opt => opt.MapFrom(src => src.imageCitizenThumbPrintContentType))
                .ForMember(d => d.cnic, opt => opt.MapFrom(src => src.imageCitizenFingerPrintCnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen))
                .ForMember(d => d.insertion_date, opt => opt.MapFrom((src => DateTime.Now)));
            CreateMap<UpdateImageCitizenFingerPrintDTO, tbl_image_citizen_finger_print>()
                .ForMember(d => d.id, opt => opt.MapFrom((src, dest) => dest.id))
                .ForMember(d => d.finger_print_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenFingerPrintName) ? src.imageCitizenFingerPrintName : dest.finger_print_name))
                .ForMember(d => d.finger_print_data, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenFingerPrintData) ? src.imageCitizenFingerPrintData : dest.finger_print_data))
                .ForMember(d => d.finger_print_content_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenFingerPrintContentType) ? src.imageCitizenFingerPrintContentType : dest.finger_print_content_type))
                .ForMember(d => d.thumb_print_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintName) ? src.imageCitizenThumbPrintName : dest.thumb_print_name))
                .ForMember(d => d.thumb_print_data, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintData) ? src.imageCitizenThumbPrintData : dest.thumb_print_data))
                .ForMember(d => d.thumb_print_content_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenThumbPrintContentType) ? src.imageCitizenThumbPrintContentType : dest.thumb_print_content_type))
                .ForMember(d => d.cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenFingerPrintCnic) ? src.imageCitizenFingerPrintCnic : dest.cnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_image_citizen_finger_print, ImageCitizenFingerPrintResponseDTO>()
                .ForMember(d => d.imageCitizenFingerPrintId, opt => opt.MapFrom(src => src.id))
                .ForMember(d => d.imageCitizenFingerPrintName, opt => opt.MapFrom((src) => src.finger_print_name))
                .ForMember(d => d.imageCitizenFingerPrintData, opt => opt.MapFrom((src) => src.finger_print_data))
                .ForMember(d => d.imageCitizenFingerPrintContentType, opt => opt.MapFrom((src) => src.finger_print_content_type))
                .ForMember(d => d.imageCitizenThumbPrintName, opt => opt.MapFrom((src) => src.thumb_print_content_type))
                .ForMember(d => d.imageCitizenThumbPrintData, opt => opt.MapFrom((src) => src.thumb_print_data))
                .ForMember(d => d.imageCitizenThumbPrintContentType, opt => opt.MapFrom((src) => src.thumb_print_content_type))
                .ForMember(d => d.imageCitizenFingerPrintCnic, opt => opt.MapFrom((src) => src.cnic))
                .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen));
                //.ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
                //.ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
