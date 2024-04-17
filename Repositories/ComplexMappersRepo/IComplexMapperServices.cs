using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Models.DTOS.TransactionDTO;

namespace BISPAPIORA.Repositories.ComplexMappersRepo
{
    public interface IComplexMapperServices
    {
        public Mapper ComplexAutomapperForLogin();

        public Mapper ComplexAutomapperForCompliance();
    }
}