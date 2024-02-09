using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class BankOtherSpecificationProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public BankOtherSpecificationProfile()
        {
            CreateMap<AddBankOtherSpecificationDTO, tbl_bank_other_specification>()
             .ForMember(d => d.bank_other_specification, opt => opt.MapFrom(src => src.bankOtherSpecification))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateBankOtherSpecificationDTO, tbl_bank_other_specification>()
             .ForMember(d => d.bank_other_specification_id, opt => opt.MapFrom((src, dest) => dest.bank_other_specification_id))
             .ForMember(d => d.bank_other_specification, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankOtherSpecification) ? src.bankOtherSpecification : dest.bank_other_specification))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_bank_other_specification, BankOtherSpecificationResponseDTO>()
             .ForMember(d => d.bankOtherSpecificationId, opt => opt.MapFrom(src => src.bank_other_specification_id))
             .ForMember(d => d.bankOtherSpecification, opt => opt.MapFrom((src) => src.bank_other_specification))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen));
        }
    }
}
