using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.GroupPermissionDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class GroupPermissionProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public GroupPermissionProfile()
        {
            CreateMap<AddGroupPermissionDTO, tbl_group_permission>()
             .ForMember(d => d.fk_user_type, opt => opt.MapFrom(src => Guid.Parse(src.fkUserType)))
             .ForMember(d => d.fk_functionality, opt => opt.MapFrom(src => Guid.Parse(src.fkFunctionality)))
             .ForMember(d => d.can_create, opt => opt.MapFrom(src => src.canCreate))
             .ForMember(d => d.can_update, opt => opt.MapFrom(src => src.canUpdate))
             .ForMember(d => d.can_read, opt => opt.MapFrom(src => src.canRead))
             .ForMember(d => d.can_delete, opt => opt.MapFrom(src => src.canCreate));
            CreateMap<UpdateGroupPermissionDTO, tbl_group_permission>()
             .ForMember(d => d.group_permission_id, opt => opt.MapFrom((src, dest) => dest.group_permission_id))
             .ForMember(d => d.can_create, opt => opt.MapFrom((src, dest) => src.canCreate))
             .ForMember(d => d.can_update, opt => opt.MapFrom((src, dest) => src.canUpdate))
             .ForMember(d => d.can_read, opt => opt.MapFrom((src, dest) => src.canRead))
             .ForMember(d => d.can_delete, opt => opt.MapFrom((src, dest) => src.canDelete));
            CreateMap<tbl_group_permission, GroupPermissionResponseDTO>()
             .ForMember(d => d.groupPermissionId, opt => opt.MapFrom(src => src.group_permission_id))
             .ForMember(d => d.canCreate, opt => opt.MapFrom(src => src.can_create))
             .ForMember(d => d.canUpdate, opt => opt.MapFrom(src => src.can_update))
             .ForMember(d => d.canRead, opt => opt.MapFrom(src => src.can_read))
             .ForMember(d => d.canDelete, opt => opt.MapFrom(src => src.can_delete))
             .ForMember(d => d.userTypeName, opt => opt.MapFrom(src => src.tbl_user_type.user_type_name))
             .ForMember(d => d.fkUserType, opt => opt.MapFrom(src => src.fk_user_type))
             .ForMember(d => d.fkFunctionality, opt => opt.MapFrom(src => src.fk_functionality))
             .ForMember(d => d.functionalityName, opt => opt.MapFrom(src => src.tbl_functionality.functionality_name));
        }
    }
}
