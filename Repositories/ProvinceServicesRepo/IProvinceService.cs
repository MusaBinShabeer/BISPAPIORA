using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.ProvinceServicesRepo
{
    public interface IProvinceService
    {
        public Task<ResponseModel<ProvinceResponseDTO>> AddProvince(AddProvinceDTO model);
        public Task<ResponseModel<ProvinceResponseDTO>> DeleteProvince(string provinceId);
        public Task<ResponseModel<List<ProvinceResponseDTO>>> GetProvincesList();
        public Task<ResponseModel<ProvinceResponseDTO>> GetProvince(string provinceId);
        public Task<ResponseModel<ProvinceResponseDTO>> UpdateProvince(UpdateProvinceDTO model);
    }
}
