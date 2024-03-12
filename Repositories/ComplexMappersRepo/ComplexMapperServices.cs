using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.GroupPermissionDTO;

namespace BISPAPIORA.Repositories.ComplexMappersRepo
{
    public class ComplexMapperServices: IComplexMapperServices
    {
        public Mapper ComplexAutomapperForLogin()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tbl_user, LoginResponseDTO>()
                .ForMember(d => d.userId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(d => d.userName, opt => opt.MapFrom(src => src.user_name))
                .ForMember(d => d.userEmail, opt => opt.MapFrom(src => src.user_email))
                .ForMember(d => d.userPassword, opt => opt.MapFrom(src => src.fk_user_type))
                .ForMember(d => d.fkUserType, opt => opt.MapFrom(src => src.tbl_user_type.user_type_name))
                .ForMember(d => d.userTypeName, opt => opt.MapFrom(src => src.tbl_user_type.user_type_name))
                .ForMember(d => d.isFtpSet, opt => opt.MapFrom(src => src.is_ftp_set))
                .ForMember(d => d.permissions, opt => opt.MapFrom(src => src.tbl_user_type.tbl_group_permissions))
                .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));            
                cfg.CreateMap<tbl_group_permission, GroupPermissionResponseDTO>()
                .ForMember(d => d.groupPermissionId, opt => opt.MapFrom(src => src.group_permission_id))
                .ForMember(d => d.fkUserType, opt => opt.MapFrom((src) => src.fk_user_type))
                .ForMember(d => d.fkFunctionality, opt => opt.MapFrom((src) => src.fk_functionality))
                .ForMember(d => d.canCreate, opt => opt.MapFrom(src => src.can_create))
                .ForMember(d => d.canUpdate, opt => opt.MapFrom(src => src.can_update))
                .ForMember(d => d.canRead, opt => opt.MapFrom(src => src.can_read))
                .ForMember(d => d.canDelete, opt => opt.MapFrom(src => src.can_delete))
                .ForMember(d => d.functionalityName, opt => opt.MapFrom(src => src.tbl_functionality.functionality_name));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
