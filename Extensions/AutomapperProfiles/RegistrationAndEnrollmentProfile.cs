using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class RegistrationAndEnrollmentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public RegistrationAndEnrollmentProfile()
        {
            CreateMap<AddRegistrationDTO, tbl_registration>()
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<AddEnrollmentDTO, tbl_enrollment>()
           .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));

        }
    }
}
