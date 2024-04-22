using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankStatementDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class BankStatementProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public BankStatementProfile()
        {
            CreateMap<AddBankStatementDTO, tbl_bank_statement>()
                .ForMember(d => d.name, opt => opt.MapFrom(src => src.bankStatementName))
                .ForMember(d => d.data, opt => opt.MapFrom(src => src.bankStatementData))
                .ForMember(d => d.content_type, opt => opt.MapFrom(src => src.bankStatementContentType))
                .ForMember(d => d.cnic, opt => opt.MapFrom(src => src.bankStatementCnic))
                .ForMember(d => d.fk_citizen_compliance, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizenCompliance) ? Guid.Parse(src.fkCitizenCompliance) : dest.fk_citizen_compliance))
                .ForMember(d => d.insertion_date, opt => opt.MapFrom((src => DateTime.Now)));
            CreateMap<UpdateBankStatementDTO, tbl_bank_statement>()
                .ForMember(d => d.id, opt => opt.MapFrom((src, dest) => dest.id))
                .ForMember(d => d.name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankStatementName) ? src.bankStatementName : dest.name))
                .ForMember(d => d.data, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankStatementData) ? src.bankStatementData : dest.data))
                .ForMember(d => d.content_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankStatementContentType) ? src.bankStatementContentType : dest.content_type))
                .ForMember(d => d.cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.bankStatementCnic) ? src.bankStatementCnic : dest.cnic))
                .ForMember(d => d.fk_citizen_compliance, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizenCompliance) ? Guid.Parse(src.fkCitizenCompliance) : dest.fk_citizen_compliance));
            CreateMap<tbl_bank_statement, BankStatementResponseDTO>()
                .ForMember(d => d.bankStatementId, opt => opt.MapFrom(src => src.id))
                .ForMember(d => d.bankStatementName, opt => opt.MapFrom((src) => src.name))
                .ForMember(d => d.bankStatementData, opt => opt.MapFrom((src) => src.data))
                .ForMember(d => d.bankStatementContentType, opt => opt.MapFrom((src) => src.content_type))
                .ForMember(d => d.bankStatementCnic, opt => opt.MapFrom((src) => src.cnic))
                .ForMember(d => d.fkCitizenCompliance, opt => opt.MapFrom(src => src.fk_citizen_compliance));
                //.ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.tbl_citizen.citizen_name))
                //.ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.tbl_citizen.citizen_cnic));
        }
    }
}
