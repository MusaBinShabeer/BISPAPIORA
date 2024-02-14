using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BISPAPIORA.Repositories.BankServicesRepo
{
    public class BankService : IBankService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public BankService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<BankResponseDTO>> AddBank(AddBankDTO model)
        {
            try
            {
                var bank = await db.tbl_banks.Where(x => x.bank_name.ToLower().Equals(model.bankName.ToLower())).FirstOrDefaultAsync();
                if (bank == null)
                {
                    var newBank = new tbl_bank();
                    newBank = _mapper.Map<tbl_bank>(model);
                    db.tbl_banks.Add(newBank);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank {model.bankName} has been added successfully",
                        data = _mapper.Map<BankResponseDTO>(newBank),
                    };
                }
                else
                {
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = $"Bank with name {model.bankName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankResponseDTO>> DeleteBank(string bankId)
        {
            try
            {
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(bankId)).FirstOrDefaultAsync();
                if (existingBank != null)
                {
                    db.tbl_banks.Remove(existingBank);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = "Bank Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<BankResponseDTO>>> GetBanksList()
        {
            try
            {
                var banks = await db.tbl_banks.ToListAsync();
                if (banks.Count() > 0)
                {
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        data = _mapper.Map<List<BankResponseDTO>>(banks),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<BankResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankResponseDTO>> GetBank(string bankId)
        {
            try
            {
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(bankId)).FirstOrDefaultAsync();
                if (existingBank != null)
                {
                    return new ResponseModel<BankResponseDTO>()
                    {
                        data = _mapper.Map<BankResponseDTO>(existingBank),
                        remarks = "Bank found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<BankResponseDTO>> UpdateBank(UpdateBankDTO model)
        {
            try
            {
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(model.bankId)).FirstOrDefaultAsync();
                if (existingBank != null)
                {
                    existingBank = _mapper.Map(model, existingBank);
                    await db.SaveChangesAsync();
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = $"Bank: {model.bankName} has been updated",
                        data = _mapper.Map<BankResponseDTO>(existingBank),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}