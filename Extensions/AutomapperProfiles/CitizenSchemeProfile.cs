using AutoMapper;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenSchemeProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenSchemeProfile()
        {
            CreateMap<AddEnrollmentDTO, AddCitizenSchemeDTO>()
            .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom(src => src.citizenSchemeYear))
            .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom(src => src.citizenSchemeQuarter))
            .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom(src => src.citizenSchemeStartingMonth))
            .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom(src => src.citizenSchemeSavingAmount))
            .ForMember(d => d.quarterCode, opt => opt.MapFrom(src => src.quarterCode))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<AddCitizenSchemeDTO, tbl_citizen_scheme>()
             .ForMember(d => d.citizen_scheme_year, opt => opt.MapFrom(src => src.citizenSchemeYear))
             .ForMember(d => d.citizen_scheme_quarter, opt => opt.MapFrom(src => src.citizenSchemeQuarter))
             .ForMember(d => d.citizen_scheme_starting_month, opt => opt.MapFrom(src => src.citizenSchemeStartingMonth))
             .ForMember(d => d.citizen_scheme_saving_amount, opt => opt.MapFrom(src => src.citizenSchemeSavingAmount))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateCitizenSchemeDTO, tbl_citizen_scheme>()
             .ForMember(d => d.citizen_scheme_id, opt => opt.MapFrom((src, dest) => dest.citizen_scheme_id))
             .ForMember(d => d.citizen_scheme_year, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeYear) ? src.citizenSchemeYear : dest.citizen_scheme_year))
             .ForMember(d => d.citizen_scheme_quarter, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeQuarter) ? src.citizenSchemeQuarter : dest.citizen_scheme_quarter))
             //.ForMember(d => d.quarter_code, opt => opt.MapFrom((src, dest) => otherServices.Check(src.quarterCode) ? src.quarterCode : dest.quarter_code))
             .ForMember(d => d.citizen_scheme_starting_month, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeStartingMonth) ? src.citizenSchemeStartingMonth : dest.citizen_scheme_starting_month))
             .ForMember(d => d.citizen_scheme_saving_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeSavingAmount) ? decimal.Parse(src.citizenSchemeSavingAmount.ToString()) : dest.citizen_scheme_saving_amount))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_citizen_scheme, CitizenSchemeResponseDTO>()
            .ForMember(d => d.citizenSchemeId, opt => opt.MapFrom(src => src.citizen_scheme_id))
            .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src) => src.citizen_scheme_year))
            .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src) => src.citizen_scheme_quarter))
            //.ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.quarter_code))
            .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src) => src.citizen_scheme_starting_month))
            .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src) => src.citizen_scheme_saving_amount))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom((src) => src.fk_citizen));
            CreateMap<AddCitizenSchemeDTO, UpdateCitizenSchemeDTO>()
            .ForMember(d => d.citizenSchemeId, opt => opt.MapFrom((src, dest) => dest.citizenSchemeId))
            .ForMember(d => d.citizenSchemeYear, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeYear) ? src.citizenSchemeYear : dest.citizenSchemeYear))
            .ForMember(d => d.citizenSchemeQuarter, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeQuarter) ? src.citizenSchemeQuarter : dest.citizenSchemeQuarter))
            .ForMember(d => d.quarterCode, opt => opt.MapFrom((src, dest) => otherServices.Check(src.quarterCode) ? src.quarterCode : dest.quarterCode))
            .ForMember(d => d.citizenSchemeStartingMonth, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeStartingMonth) ? src.citizenSchemeStartingMonth : dest.citizenSchemeStartingMonth))
            .ForMember(d => d.citizenSchemeSavingAmount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenSchemeSavingAmount) ? src.citizenSchemeSavingAmount : dest.citizenSchemeSavingAmount))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? src.fkCitizen : dest.fkCitizen));
        }
    }
}
