using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class RegistrationAndEnrollmentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public RegistrationAndEnrollmentProfile()
        {
            CreateMap<AddRegistrationDTO, tbl_registration>()
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)))
            .ForMember(d => d.citizen_code, opt => opt.MapFrom((src, dest) => dest.tbl_citizen.code));
            CreateMap<AddEnrollmentDTO, tbl_enrollment>()
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)))
            .ForMember(d => d.citizen_code, opt => opt.MapFrom((src, dest) => dest.tbl_citizen.code));
            CreateMap<(AddEnrollmentDTO dto, int code), tbl_enrollment>()
           .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.dto.fkCitizen)))
           .ForMember(d => d.citizen_code, opt => opt.MapFrom((src, dest) => src.code));
            CreateMap<(AddEnrollmentDTO dto, decimal code), tbl_enrollment>()
           .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.dto.fkCitizen)))
           .ForMember(d => d.citizen_code, opt => opt.MapFrom((src, dest) => src.code));
            CreateMap<(AddRegistrationDTO dto, decimal code), tbl_registration>()
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.dto.fkCitizen)))
            .ForMember(d => d.citizen_code, opt => opt.MapFrom((src, dest) => src.code));
            CreateMap<AddEnrollmentDTO, UpdateEnrollmentDTO>()
            .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.citizenName))
            .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.citizenPhoneNo))
            .ForMember(d => d.citizenGender, opt => opt.MapFrom(src => src.citizenGender))
            .ForMember(d => d.maritalStatus, opt => opt.MapFrom(src =>src.maritalStatus))
            .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.citizenAddress))
            .ForMember(d => d.fatherSpouseName, opt => opt.MapFrom(src => src.fatherSpouseName))
            .ForMember(d => d.fkTehsil, opt => opt.MapFrom(src => Guid.Parse(src.fkTehsil)))
            .ForMember(d => d.fkEmployment, opt => opt.MapFrom(src => Guid.Parse(src.fkEmployment)))
            .ForMember(d => d.fkEducation, opt => opt.MapFrom(src => Guid.Parse(src.fkEducation)))
            .ForMember(d => d.dateOfBirth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dateOfBirth) ? src.dateOfBirth : dest.dateOfBirth))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.citizenCnic))
            .ForMember(d => d.quarterCode, opt => opt.MapFrom(src => src.quarterCode));

        }
    }
}
