using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class ImageCitizenAttachmentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public ImageCitizenAttachmentProfile()
        {
            CreateMap<AddImageCitizenAttachmentDTO, tbl_image_citizen_attachment>()
                .ForMember(d => d.name, opt => opt.MapFrom(src => src.imageCitizenAttachmentName))
                .ForMember(d => d.data, opt => opt.MapFrom(src => src.imageCitizenAttachmentData))
                .ForMember(d => d.content_type, opt => opt.MapFrom(src => src.imageCitizenAttachmentContentType))
                .ForMember(d => d.cnic, opt => opt.MapFrom(src => src.imageCitizenAttachmentCnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateImageCitizenAttachmentDTO, tbl_image_citizen_attachment>()
                .ForMember(d => d.id, opt => opt.MapFrom((src, dest) => dest.id))
                .ForMember(d => d.name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenAttachmentName) ? src.imageCitizenAttachmentName : dest.name))
                .ForMember(d => d.data, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenAttachmentData) ? src.imageCitizenAttachmentData : dest.data))
                .ForMember(d => d.content_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenAttachmentContentType) ? src.imageCitizenAttachmentContentType : dest.content_type))
                .ForMember(d => d.cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.imageCitizenAttachmentCnic) ? src.imageCitizenAttachmentCnic : dest.cnic))
                .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_image_citizen_attachment, ImageCitizenAttachmentResponseDTO>()
                .ForMember(d => d.imageCitizenAttachmentId, opt => opt.MapFrom(src => src.id))
                .ForMember(d => d.imageCitizenAttachmentData, opt => opt.MapFrom((src) => src.name))
                .ForMember(d => d.imageCitizenAttachmentData, opt => opt.MapFrom((src) => src.data))
                .ForMember(d => d.imageCitizenAttachmentContentType, opt => opt.MapFrom((src) => src.content_type))
                .ForMember(d => d.imageCitizenAttachmentCnic, opt => opt.MapFrom((src) => src.cnic))
                .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen));
                //.ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
                //.ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
