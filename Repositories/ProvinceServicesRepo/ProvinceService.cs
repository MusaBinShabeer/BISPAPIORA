using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ProvinceDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.ProvinceServicesRepo
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;
        private readonly OraDbContext db;
        public ProvinceService(IMapper mapper, OraDbContext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<ProvinceResponseDTO>> AddProvince(AddProvinceDTO model)
        {
            try
            {
                var province = await db.tbl_provinces.Where(x => x.province_name.ToLower().Equals(model.provinceName.ToLower())).FirstOrDefaultAsync();
                if (province == null)
                {
                    var newProvince = new tbl_province();
                    newProvince = _mapper.Map<tbl_province>(model);
                    db.tbl_provinces.Add(newProvince);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = true,
                        remarks = $"Province {model.provinceName} has been added successfully",
                        data = _mapper.Map<ProvinceResponseDTO>(newProvince),
                    };
                }
                else
                {
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = $"Province with name {model.provinceName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ProvinceResponseDTO>> DeleteProvince(string provinceId)
        {
            try
            {
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(provinceId)).FirstOrDefaultAsync();
                if (existingProvince != null)
                {
                    db.tbl_provinces.Remove(existingProvince);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = "Province Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ProvinceResponseDTO>>> GetProvincesList()
        {
            try
            {
                var provinces = await db.tbl_provinces.ToListAsync();
                if (provinces.Count() > 0)
                {
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        data = _mapper.Map<List<ProvinceResponseDTO>>(provinces),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ProvinceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ProvinceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ProvinceResponseDTO>> GetProvince(string provinceId)
        {
            try
            {
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(provinceId)).FirstOrDefaultAsync();
                if (existingProvince != null)
                {
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        data = _mapper.Map<ProvinceResponseDTO>(existingProvince),
                        remarks = "Province found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ProvinceResponseDTO>> UpdateProvince(UpdateProvinceDTO model)
        {
            try
            {
                var existingProvince = await db.tbl_provinces.Where(x => x.province_id == Guid.Parse(model.provinceId)).FirstOrDefaultAsync();
                if (existingProvince != null)
                {
                    existingProvince = _mapper.Map(model, existingProvince);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        remarks = $"Province: {model.provinceName} has been updated",
                        data = _mapper.Map<ProvinceResponseDTO>(existingProvince),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ProvinceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ProvinceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
