using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DistrictDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.DistrictServicesRepo
{
    public interface IDistrictService
    {
        public Task<ResponseModel<DistrictResponseDTO>> AddDistrict(AddDistrictDTO model);
        public Task<ResponseModel<DistrictResponseDTO>> DeleteDistrict(string districtId);
        public Task<ResponseModel<List<DistrictResponseDTO>>> GetDistrictsList();
        public Task<ResponseModel<DistrictResponseDTO>> GetDistrict(string districtId);
        public Task<ResponseModel<DistrictResponseDTO>> UpdateDistrict(UpdateDistrictDTO model);
        public Task<ResponseModel<List<DistrictResponseDTO>>> GetDistrictByProviceId(string provinceId);
    }
}
