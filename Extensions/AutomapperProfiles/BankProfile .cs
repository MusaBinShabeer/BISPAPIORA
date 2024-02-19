using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class BankProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public BankProfile()
        {
            CreateMap<AddBankDTO, tbl_bank>()
             .ForMember(d => d.bank_name, opt => opt.MapFrom(src => src.bankName))
             .ForMember(d => d.bank_prefix_iban, opt => opt.MapFrom(src => src.bankPrefixIban))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive));
            CreateMap<UpdateBankDTO, tbl_bank>()
             .ForMember(d => d.bank_id, opt => opt.MapFrom((src, dest) => dest.bank_id))
             .ForMember(d => d.bank_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankName) ? src.bankName : dest.bank_name))
             .ForMember(d => d.bank_prefix_iban, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankPrefixIban) ? src.bankPrefixIban : dest.bank_prefix_iban))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_bank, BankResponseDTO>()
             .ForMember(d => d.bankId, opt => opt.MapFrom(src => src.bank_id))
             .ForMember(d => d.bankName, opt => opt.MapFrom((src) => src.bank_name))
             .ForMember(d => d.bankPrefixIban, opt => opt.MapFrom((src) => src.bank_prefix_iban))
             .ForMember(d => d.bankCode, opt => opt.MapFrom(src => src.code))
             .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
