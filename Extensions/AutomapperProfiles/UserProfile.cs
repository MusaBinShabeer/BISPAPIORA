﻿using AutoMapper;
using BISPAPIORA.Extensions;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.DTOS.UserDTOs;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        private readonly OtherServices otherServices = new();
        public UserProfile()
        {
            CreateMap<AddUserDTO, tbl_user>()
             .ForMember(d => d.user_name, opt => opt.MapFrom(src => src.userName))
             .ForMember(d => d.user_email, opt => opt.MapFrom(src => src.userEmail))
             .ForMember(d => d.user_password, opt => opt.MapFrom(src => otherServices.encodePassword(src.userPassword)))
             .ForMember(d => d.fk_user_type, opt => opt.MapFrom(src => src.fkUserType))
             .ForMember(d => d.is_active, opt => opt.MapFrom(src => src.isActive))
             .ForMember(d => d.remarks, opt => opt.MapFrom(src => src.remarks))
             .ForMember(d => d.insertion_date, opt => opt.MapFrom((src => DateTime.Now)));
            CreateMap<UpdateUserDTO, tbl_user>()
             .ForMember(d => d.user_id, opt => opt.MapFrom((src, dest) => dest.user_id))
             .ForMember(d => d.user_name, opt => opt.MapFrom((src, dest) => otherServices.Check(src.userName) ? src.userName : dest.user_name))
             .ForMember(d => d.remarks, opt => opt.MapFrom((src, dest) => otherServices.Check(src.remarks) ? src.remarks : dest.remarks))
             .ForMember(d => d.user_email, opt => opt.MapFrom((src, dest) => otherServices.Check(src.userEmail) ? src.userEmail : dest.user_email))
             .ForMember(d => d.user_password, opt => opt.MapFrom((src, dest) =>dest.user_password))
             .ForMember(d => d.fk_user_type, opt => opt.MapFrom((src, dest) => otherServices.Check(src.fkUserType) ? Guid.Parse(src.fkUserType) : dest.fk_user_type))
             .ForMember(d => d.is_active, opt => opt.MapFrom((src, dest) => src.isActive));
            CreateMap<tbl_user, UserResponseDTO>()
             .ForMember(d => d.userId, opt => opt.MapFrom(src => src.user_id))
             .ForMember(d => d.userName, opt => opt.MapFrom((src) => src.user_name))
             .ForMember(d => d.remarks, opt => opt.MapFrom((src) => src.remarks))
             .ForMember(d => d.userEmail, opt => opt.MapFrom((src) => src.user_email))
             .ForMember(d => d.userPassword, opt => opt.MapFrom(src => src.user_password))
             .ForMember(d => d.fkUserType, opt => opt.MapFrom(src => src.fk_user_type))
             .ForMember(d => d.userTypeName, opt => opt.MapFrom(src => src.tbl_user_type.user_type_name))
             .ForMember(d => d.isFtpSet, opt => opt.MapFrom(src => src.is_ftp_set))
             .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
            CreateMap<tbl_user, LoginResponseDTO>()
               .ForMember(d => d.userId, opt => opt.MapFrom(src => src.user_id))
               .ForMember(d => d.userName, opt => opt.MapFrom(src => src.user_name))
               .ForMember(d => d.userEmail, opt => opt.MapFrom(src => src.user_email))
               .ForMember(d => d.fkUserType, opt => opt.MapFrom(src => src.tbl_user_type.user_type_id))
               .ForMember(d => d.userTypeName, opt => opt.MapFrom(src => src.tbl_user_type.user_type_name))
               .ForMember(d => d.isFtpSet, opt => opt.MapFrom(src => src.is_ftp_set))
               .ForMember(d => d.isActive, opt => opt.MapFrom(src => src.is_active));
        }
    }
}
