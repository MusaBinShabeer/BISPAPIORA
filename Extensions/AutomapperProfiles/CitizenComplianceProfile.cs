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
             .ForMember(d => d.citizen_compliance_quarter_code, opt => opt.MapFrom(src => src.quarterCode))
             .ForMember(d => d.is_compliant, opt => opt.MapFrom(src => src.isCompliant))
             .ForMember(d => d.fk_compliant_by, opt => opt.MapFrom(src => Guid.Parse(src.fkCompliantBy)))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)))
             .ForMember(d => d.insertion_date, opt => opt.MapFrom((src => DateTime.Now)));
            CreateMap<UpdateCitizenComplianceDTO, tbl_citizen_compliance>()
             .ForMember(d => d.citizen_compliance_id, opt => opt.MapFrom((src, dest) => dest.citizen_compliance_id))
             .ForMember(d => d.starting_balance_on_quarterly_bank_statement, opt => opt.MapFrom((src, dest) => otherServices.Check(src.startingBalanceOnQuarterlyBankStatement) ? decimal.Parse(src.startingBalanceOnQuarterlyBankStatement.ToString()) : dest.starting_balance_on_quarterly_bank_statement))
             .ForMember(d => d.closing_balance_on_quarterly_bank_statement, opt => opt.MapFrom((src, dest) => otherServices.Check(src.closingBalanceOnQuarterlyBankStatement) ? decimal.Parse(src.closingBalanceOnQuarterlyBankStatement.ToString()) : dest.closing_balance_on_quarterly_bank_statement))
             .ForMember(d => d.citizen_compliance_actual_saving_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenComplianceActualSavingAmount) ? decimal.Parse(src.citizenComplianceActualSavingAmount.ToString()) : dest.citizen_compliance_actual_saving_amount))
             .ForMember(d => d.citizen_compliance_quarter_code, opt => opt.MapFrom((src, dest) => otherServices.Check(src.quarterCode) ? src.quarterCode : dest.citizen_compliance_quarter_code))
             .ForMember(d => d.is_compliant, opt => opt.MapFrom((src, dest) => src.isCompliant))
            .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen));
            CreateMap<tbl_citizen_compliance, CitizenComplianceResponseDTO>()
            .ForMember(d => d.citizenComplianceId, opt => opt.MapFrom(src => src.citizen_compliance_id))
            .ForMember(d => d.startingBalanceOnQuarterlyBankStatement, opt => opt.MapFrom((src) => src.starting_balance_on_quarterly_bank_statement))
            .ForMember(d => d.closingBalanceOnQuarterlyBankStatement, opt => opt.MapFrom((src) => src.closing_balance_on_quarterly_bank_statement))
            .ForMember(d => d.citizenComplianceActualSavingAmount, opt => opt.MapFrom((src) => src.citizen_compliance_actual_saving_amount))
            .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.citizen_compliance_quarter_code))
            .ForMember(d => d.isCompliant, opt => opt.MapFrom(src => src.is_compliant))
            .ForMember(d => d.fkCompliantBy, opt => opt.MapFrom(src => (src.fk_compliant_by)))
            .ForMember(d => d.compliantByUser, opt => opt.MapFrom(src => (src.compliant_by.user_name)))
            .ForMember(d => d.citizenCnic, opt => opt.MapFrom((src) => src.tbl_citizen.citizen_cnic))
            .ForMember(d => d.fkCitizen, opt => opt.MapFrom((src) => src.fk_citizen));
        }
    }
}
