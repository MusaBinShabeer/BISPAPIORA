using AutoMapper;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Models.DBModels.Dbtables;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class CitizenComplianceProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public CitizenComplianceProfile()
        {
            CreateMap<AddCitizenComplianceDTO, tbl_citizen_compliance>()
             .ForMember(d => d.starting_balance_on_quarterly_bank_statement, opt => opt.MapFrom(src => src.startingBalanceOnQuarterlyBankStatement))
             .ForMember(d => d.closing_balance_on_quarterly_bank_statement, opt => opt.MapFrom(src => src.closingBalanceOnQuarterlyBankStatement))
             .ForMember(d => d.citizen_compliance_actual_saving_amount, opt => opt.MapFrom(src => src.citizenComplianceActualSavingAmount))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)));
            CreateMap<UpdateCitizenComplianceDTO, tbl_citizen_compliance>()
             .ForMember(d => d.citizen_compliance_id, opt => opt.MapFrom((src, dest) => dest.citizen_compliance_id))
             .ForMember(d => d.starting_balance_on_quarterly_bank_statement, opt => opt.MapFrom((src, dest) => otherServices.Check(src.startingBalanceOnQuarterlyBankStatement) ? decimal.Parse(src.startingBalanceOnQuarterlyBankStatement.ToString()) : dest.starting_balance_on_quarterly_bank_statement))
             .ForMember(d => d.closing_balance_on_quarterly_bank_statement, opt => opt.MapFrom((src, dest) => otherServices.Check(src.closingBalanceOnQuarterlyBankStatement) ? decimal.Parse(src.closingBalanceOnQuarterlyBankStatement.ToString()) : dest.closing_balance_on_quarterly_bank_statement))
             .ForMember(d => d.citizen_compliance_actual_saving_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenComplianceActualSavingAmount) ? decimal.Parse(src.citizenComplianceActualSavingAmount.ToString()) : dest.citizen_compliance_actual_saving_amount))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_citizen_compliance, CitizenComplianceResponseDTO>()
            .ForMember(d => d.citizenComplianceId, opt => opt.MapFrom(src => src.citizen_compliance_id))
            .ForMember(d => d.startingBalanceOnQuarterlyBankStatement, opt => opt.MapFrom((src) => src.starting_balance_on_quarterly_bank_statement))
            .ForMember(d => d.closingBalanceOnQuarterlyBankStatement, opt => opt.MapFrom((src) => src.closing_balance_on_quarterly_bank_statement))
            .ForMember(d => d.citizenComplianceActualSavingAmount, opt => opt.MapFrom((src) => src.citizen_compliance_actual_saving_amount))
            .ForMember(d => d.citizenComplianceCode, opt => opt.MapFrom(src => src.code))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom((src) => src.fk_citizen));
        }
    }
}
