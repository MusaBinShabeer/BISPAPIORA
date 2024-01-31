using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.RegistrationDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Extensions.AutomapperProfiles
{
    public class DbfCitizenProfile :Profile
    {

        private readonly OtherServices otherServices = new();
        public DbfCitizenProfile() 
        {
            CreateMap<AddRegistrationDTO, HiberProtectionAccount>()
               .ForMember(d => d.Address, opt => opt.MapFrom(src => src.citizenAddress))
               .ForMember(d => d.MobileNo, opt => opt.MapFrom(src => src.citizenPhoneNo))
               .ForMember(d => d.Cnic, opt => opt.MapFrom(src => src.citizenCnic))
               .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.citizenGender))
               .ForMember(d => d.Name, opt => opt.MapFrom(src => src.citizenName));
            CreateMap<UpdateRegistrationDTO, HiberProtectionAccount>()
               .ForMember(d => d.Address, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenAddress)?src.citizenAddress:dest.Address))
               .ForMember(d => d.MobileNo, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenPhoneNo)?decimal.Parse(src.citizenPhoneNo):dest.MobileNo))
               .ForMember(d => d.Cnic, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenCnic)? decimal.Parse(src.citizenCnic):dest.Cnic))
               .ForMember(d => d.Gender, opt => opt.MapFrom((src, dest) => otherServices.Check(src.citizenGender)? $"{(GenderEnum)src.citizenGender}" : dest.Gender))
               .ForMember(d => d.Name, opt => opt.MapFrom((src,dest) => otherServices.Check(src.citizenName)?src.citizenName:dest.Name));
            //CreateMap<HiberProtectionAccount, RegistrationResponseDTO>()
            //   .ForMember(d => d.citizenAddress, opt => opt.MapFrom(src => src.Address))
            //   .ForMember(d => d.citizenPhoneNo, opt => opt.MapFrom(src => src.MobileNo))
            //   .ForMember(d => d.citizenCnic, opt => opt.MapFrom(src => src.Cnic))
            //   .ForMember(d => d.genderName, opt => opt.MapFrom(src => src.Gender))
            //   .ForMember(d => d.citizenName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
