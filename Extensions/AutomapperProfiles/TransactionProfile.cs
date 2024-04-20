﻿using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.TransactionDTO;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class TransactionProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public TransactionProfile()
        {
            CreateMap<AddTransactionDTO, tbl_transaction>()
             .ForMember(d => d.transaction_date, opt => opt.MapFrom(src => DateTime.Parse(src.transactionDate)))
             .ForMember(d => d.transaction_amount, opt => opt.MapFrom(src => src.transactionAmount))
             .ForMember(d => d.transaction_type, opt => opt.MapFrom(src => src.transactionType))
             .ForMember(d => d.transaction_quarter_code, opt => opt.MapFrom(src => src.quarterCode))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom(src => Guid.Parse(src.fkCitizen)))
             .ForMember(d => d.fk_compliance, opt => opt.MapFrom(src => Guid.Parse(src.fkCompliance)));
            CreateMap<UpdateTransactionDTO, tbl_transaction>()
             .ForMember(d => d.transaction_id, opt => opt.MapFrom((src, dest) => dest.transaction_id))
             .ForMember(d => d.transaction_date, opt => opt.MapFrom((src, dest) => otherServices.Check(src.transactionDate) ? DateTime.Parse(src.transactionDate) : dest.transaction_date))
             .ForMember(d => d.transaction_amount, opt => opt.MapFrom((src, dest) => otherServices.Check(src.transactionAmount) ? decimal.Parse(src.transactionAmount.ToString()) : dest.transaction_amount))
             .ForMember(d => d.transaction_quarter_code, opt => opt.MapFrom((src, dest) => otherServices.Check(src.quarterCode) ? src.quarterCode : dest.transaction_quarter_code))
             .ForMember(d => d.transaction_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.transactionType) ? src.transactionType : dest.transaction_type))
             .ForMember(d => d.fk_citizen, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCitizen) ? Guid.Parse(src.fkCitizen) : dest.fk_citizen))
             .ForMember(d => d.fk_compliance, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkCompliance) ? Guid.Parse(src.fkCompliance) : dest.fk_compliance));
            CreateMap<tbl_transaction, TransactionResponseDTO>()
             .ForMember(d => d.transactionId, opt => opt.MapFrom(src => src.transaction_id))
             .ForMember(d => d.transactionDate, opt => opt.MapFrom((src) => src.transaction_date))
             .ForMember(d => d.transactionAmount, opt => opt.MapFrom((src) => src.transaction_amount))
             .ForMember(d => d.quarterCode, opt => opt.MapFrom((src) => src.transaction_quarter_code))
             .ForMember(d => d.transactionType, opt => opt.MapFrom((src) => src.transaction_type))
             .ForMember(d => d.fkCitizen, opt => opt.MapFrom(src => src.fk_citizen))
             .ForMember(d => d.fkCompliance, opt => opt.MapFrom(src => src.fk_compliance));
        }
    }
}
