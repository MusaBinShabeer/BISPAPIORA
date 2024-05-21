using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.TehsilDTO;

namespace BISPAPIORA.Repositories.TehsilServicesRepo
{
    public interface ITehsilService
    {
        public Task<ResponseModel<TehsilResponseDTO>> AddTehsil(AddTehsilDTO model);
        public Task<ResponseModel<TehsilResponseDTO>> DeleteTehsil(string tehsilId);
        public Task<ResponseModel<List<TehsilResponseDTO>>> GetTehsilsList();
        public Task<ResponseModel<List<TehsilResponseDTO>>> GetActiveTehsilsList();
        public Task<ResponseModel<TehsilResponseDTO>> GetTehsil(string tehsilId);
        public Task<ResponseModel<TehsilResponseDTO>> UpdateTehsil(UpdateTehsilDTO model);
        public Task<ResponseModel<List<TehsilResponseDTO>>> GetTehsilByDistrictId(string districtId);
    }
}
