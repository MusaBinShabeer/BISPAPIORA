using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.DistrictDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.DistrictServicesRepo
{
    public class DistrictService : IDistrictService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public DistrictService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<DistrictResponseDTO>> AddDistrict(AddDistrictDTO model)
        {
            try
            {
                var district = await db.tbl_districts.Where(x => x.district_name.ToLower().Equals(model.districtName.ToLower())).FirstOrDefaultAsync();
                if (district == null)
                {
                    var newDistrict = new tbl_district();
                    newDistrict = _mapper.Map<tbl_district>(model);
                    db.tbl_districts.Add(newDistrict);
                    await db.SaveChangesAsync();
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = true,
                        remarks = $"District {model.districtName} has been added successfully",
                        data = _mapper.Map<DistrictResponseDTO>(newDistrict),
                    };
                }
                else
                {
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = $"District with name {model.districtName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<DistrictResponseDTO>> DeleteDistrict(string districtId)
        {
            try
            {
                var existingDistrict = await db.tbl_districts.Where(x => x.district_id == Guid.Parse(districtId)).FirstOrDefaultAsync();
                if (existingDistrict != null)
                {
                    db.tbl_districts.Remove(existingDistrict);
                    await db.SaveChangesAsync();
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = "District Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<DistrictResponseDTO>>> GetDistrictsList()
        {
            try
            {
                var districts = await db.tbl_districts.Include(x => x.tbl_province).ToListAsync();
                if (districts.Count() > 0)
                {
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        data = _mapper.Map<List<DistrictResponseDTO>>(districts),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<DistrictResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<DistrictResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<DistrictResponseDTO>> GetDistrict(string districtId)
        {
            try
            {
                var existingDistrict = await db.tbl_districts.Include(x => x.tbl_province).Where(x => x.district_id == Guid.Parse(districtId)).FirstOrDefaultAsync();
                if (existingDistrict != null)
                {
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        data = _mapper.Map<DistrictResponseDTO>(existingDistrict),
                        remarks = "District found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<DistrictResponseDTO>> UpdateDistrict(UpdateDistrictDTO model)
        {
            try
            {
                var existingDistrict = await db.tbl_districts.Where(x => x.district_id == Guid.Parse(model.districtId)).FirstOrDefaultAsync();
                if (existingDistrict != null)
                {
                    existingDistrict = _mapper.Map(model, existingDistrict);
                    await db.SaveChangesAsync();
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        remarks = $"District: {model.districtName} has been updated",
                        data = _mapper.Map<DistrictResponseDTO>(existingDistrict),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<DistrictResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<DistrictResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
