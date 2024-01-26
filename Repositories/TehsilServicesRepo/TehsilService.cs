using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.TehsilDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.TehsilServicesRepo
{
    public class TehsilService : ITehsilService
    {
        private readonly IMapper _mapper;
        private readonly OraDbContext db;
        public TehsilService(IMapper mapper, OraDbContext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<TehsilResponseDTO>> AddTehsil(AddTehsilDTO model)
        {
            try
            {
                var tehsil = await db.tbl_tehsils.Where(x => x.tehsil_name.ToLower().Equals(model.tehsilName.ToLower())).FirstOrDefaultAsync();
                if (tehsil == null)
                {
                    var newTehsil = new tbl_tehsil();
                    newTehsil = _mapper.Map<tbl_tehsil>(model);
                    db.tbl_tehsils.Add(newTehsil);
                    await db.SaveChangesAsync();
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = true,
                        remarks = $"Tehsil {model.tehsilName} has been added successfully",
                        data = _mapper.Map<TehsilResponseDTO>(newTehsil),
                    };
                }
                else
                {
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = $"Tehsil with name {model.tehsilName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TehsilResponseDTO>> DeleteTehsil(string tehsilId)
        {
            try
            {
                var existingTehsil = await db.tbl_tehsils.Where(x => x.tehsil_id == Guid.Parse(tehsilId)).FirstOrDefaultAsync();
                if (existingTehsil != null)
                {
                    db.tbl_tehsils.Remove(existingTehsil);
                    await db.SaveChangesAsync();
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = "Tehsil Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<TehsilResponseDTO>>> GetTehsilsList()
        {
            try
            {
                var tehsils = await db.tbl_tehsils.Include(x => x.tbl_district).Include(x => x.tbl_district.tbl_province).ToListAsync();
                if (tehsils.Count() > 0)
                {
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        data = _mapper.Map<List<TehsilResponseDTO>>(tehsils),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<TehsilResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<TehsilResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TehsilResponseDTO>> GetTehsil(string tehsilId)
        {
            try
            {
                var existingTehsil = await db.tbl_tehsils.Include(x => x.tbl_district).Include(x => x.tbl_district.tbl_province).Where(x => x.tehsil_id == Guid.Parse(tehsilId)).FirstOrDefaultAsync();
                if (existingTehsil != null)
                {
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        data = _mapper.Map<TehsilResponseDTO>(existingTehsil),
                        remarks = "Tehsil found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TehsilResponseDTO>> UpdateTehsil(UpdateTehsilDTO model)
        {
            try
            {
                var existingTehsil = await db.tbl_tehsils.Where(x => x.tehsil_id == Guid.Parse(model.tehsilId)).FirstOrDefaultAsync();
                if (existingTehsil != null)
                {
                    existingTehsil = _mapper.Map(model, existingTehsil);
                    await db.SaveChangesAsync();
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        remarks = $"Tehsil: {model.tehsilName} has been updated",
                        data = _mapper.Map<TehsilResponseDTO>(existingTehsil),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TehsilResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TehsilResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
