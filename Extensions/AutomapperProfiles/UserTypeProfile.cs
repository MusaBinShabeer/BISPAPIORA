using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.UserTypeDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class UserTypeProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public UserTypeProfile()
        {
            CreateMap<AddUserTypeDTO, tbl_user_type>()
             .ForMember(d => d.user_type_name, opt => opt.MapFrom(src => src.userTypeName))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive))
             .ForMember(d => d.enum_index, opt => opt.MapFrom(src => src.enumIndex));
            CreateMap<UpdateUserTypeDTO, tbl_user_type>()
             .ForMember(d => d.user_type_id, opt => opt.MapFrom((src, dest) => dest.user_type_id))
             .ForMember(d => d.user_type_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.userTypeName) ? src.userTypeName : dest.user_type_name))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive))
             .ForMember(d => d.enum_index, opt => opt.MapFrom((src, dest) => src.enumIndex!=null? src.enumIndex: dest.enum_index));
            CreateMap<tbl_user_type, UserTypeResponseDTO>()
             .ForMember(d => d.userTypeId, opt => opt.MapFrom(src => src.user_type_id))
             .ForMember(d => d.userTypeName, opt => opt.MapFrom((src) => src.user_type_name))
             .ForMember(d => d.enumIndex, opt => opt.MapFrom((src) => src.enum_index))
             .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
