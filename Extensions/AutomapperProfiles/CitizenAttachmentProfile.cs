using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenAttachmentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenAttachmentProfile()
        {
            CreateMap<AddCitizenAttachmentDTO, tbl_citizen_attachment>()
             .ForMember(d => d.attachment_name, opt => opt.MapFrom(src => src.citizenAttachmentName))
             .ForMember(d => d.attachment_path, opt => opt.MapFrom(src => src.citizenAttachmentPath))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateCitizenAttachmentDTO, tbl_citizen_attachment>()
             .ForMember(d => d.citizen_attachment_id, opt => opt.MapFrom((src, dest) => dest.citizen_attachment_id))
             .ForMember(d => d.attachment_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAttachmentName) ? src.citizenAttachmentName : dest.attachment_name))
             .ForMember(d => d.attachment_path, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAttachmentPath) ? src.citizenAttachmentPath : dest.attachment_path))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_citizen_attachment, CitizenAttachmentResponseDTO>()
             .ForMember(d => d.citizenAttachmentId, opt => opt.MapFrom(src => src.citizen_attachment_id))
             .ForMember(d => d.citizenAttachmentName, opt => opt.MapFrom((src) => src.attachment_name))
             .ForMember(d => d.citizenAttachmentPath, opt => opt.MapFrom((src) => src.attachment_path))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen))
             .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
             .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
