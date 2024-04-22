using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.PaymentDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class PaymentProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public PaymentProfile()
        {
            CreateMap<AddPaymentDTO, tbl_payment>()
             .ForMember(d => d.paid_amount, opt => opt.MapFrom(src => src.paidAmount))
             .ForMember(d => d.due_amount, opt => opt.MapFrom(src => src.dueAmount))
             .ForMember(d => d.payment_quarter_code, opt => opt.MapFrom(src => src.quarterCode))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)))
             .ForMember(d => d.fk_compliance, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizenCompliance) ? Guid.Parse(src.fkCitizenCompliance) : dest.fk_compliance));
            CreateMap<UpdatePaymentDTO, tbl_payment>()
             .ForMember(d => d.payment_id, opt => opt.MapFrom((src, dest) => dest.payment_id))
             .ForMember(d => d.paid_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.paidAmount) ? decimal.Parse(src.paidAmount.ToString()) : dest.paid_amount))
             .ForMember(d => d.due_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.dueAmount) ? decimal.Parse(src.dueAmount.ToString()) : dest.due_amount))
             .ForMember(d => d.payment_quarter_code, opt => opt.MapFrom((src, dest) => otherServices.Check(src.quarterCode) ? src.quarterCode : dest.payment_quarter_code))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen))
             .ForMember(d => d.fk_compliance, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizenCompliance) ? Guid.Parse(src.fkCitizenCompliance) : default!));
            CreateMap<tbl_payment, PaymentResponseDTO>()
             .ForMember(d => d.paymentId, opt => opt.MapFrom(src => src.payment_id))
             .ForMember(d => d.paidAmount, opt => opt.MapFrom((src) => src.paid_amount))
             .ForMember(d => d.dueAmount, opt => opt.MapFrom((src) => src.due_amount))
             .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.payment_quarter_code))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen))
             .ForMember(d => d.fkCitizenCompliance, opt => opt.MapFrom(src => src.fk_compliance));
        }
    }
}
