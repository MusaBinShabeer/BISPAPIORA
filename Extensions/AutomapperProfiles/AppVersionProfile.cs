using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.AppVersionDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class AppVersionProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public AppVersionProfile()
        {
            CreateMap<AddAppVersionDTO, tbl_app_version>()
             .ForMember(d => d.app_version, opt => opt.MapFrom(src => src.appVersion))
             .ForMember(d => d.app_update_url, opt => opt.MapFrom(src => src.appVersionURL));
            CreateMap<UpdateAppVersionDTO, tbl_app_version>()
             .ForMember(d => d.app_version_id, opt => opt.MapFrom((src, dest) => dest.app_version_id))
             .ForMember(d => d.app_version, opt => opt.MapFrom((src, dest) => otherServices.Check(src.appVersion) ? src.appVersion : dest.app_version))
             .ForMember(d => d.app_update_url, opt => opt.MapFrom((src, dest) => otherServices.Check(src.appVersionURL) ? src.appVersionURL : dest.app_update_url));
            CreateMap<tbl_app_version, AppVersionResponseDTO>()
             .ForMember(d => d.appVersionId, opt => opt.MapFrom(src => src.app_version_id))
             .ForMember(d => d.appVersion, opt => opt.MapFrom((src) => src.app_version))
             .ForMember(d => d.appVersionURL, opt => opt.MapFrom((src) => src.app_update_url));
        }
    }
}
