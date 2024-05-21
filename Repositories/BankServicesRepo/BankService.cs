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

        // Adds a new bank record to the database based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankResponseDTO>> AddBank(AddBankDTO model)
        {
            try
            {
                // Check if a bank with the same name already exists in the database
                var bank = await db.tbl_banks.Where(x => x.bank_name.ToLower().Equals(model.bankName.ToLower())).FirstOrDefaultAsync();

                if (bank == null)
                {
                    // If no existing record is found, create a new bank record
                    var newBank = new tbl_bank();

                    // Map properties from the provided DTO to the entity using AutoMapper
                    newBank = _mapper.Map<tbl_bank>(model);
                    db.tbl_banks.Add(newBank);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added bank record
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank {model.bankName} has been added successfully",
                        data = _mapper.Map<BankResponseDTO>(newBank),
                    };
                }
                else
                {
                    // If a bank with the same name already exists, return a failure response
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = $"Bank with name {model.bankName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an existing bank record from the database based on the provided bank ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankResponseDTO>> DeleteBank(string bankId)
        {
            try
            {
                // Retrieve the existing bank record from the database based on the provided ID
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(bankId)).FirstOrDefaultAsync();

                if (existingBank != null)
                {
                    // If the bank record is found, remove it from the database
                    existingBank.is_active=false;
                    await db.SaveChangesAsync();

                    // Return a success response model indicating the deletion
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = "Bank Deleted",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all bank records from the database
        // Returns a response model containing the list of banks or indicating the absence of records
        public async Task<ResponseModel<List<BankResponseDTO>>> GetBanksList()
        {
            try
            {
                // Retrieve all bank records from the database
                var banks = await db.tbl_banks.ToListAsync();

                if (banks.Count() > 0)
                {
                    // If there are records, return a success response model with the list of banks
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        data = _mapper.Map<List<BankResponseDTO>>(banks),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<BankResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        // Retrieves a list of all active bank records from the database
        // Returns a response model containing the list of banks or indicating the absence of records
        public async Task<ResponseModel<List<BankResponseDTO>>> GetActiveBanksList()
        {
            try
            {
                var isActive= true;
                // Retrieve all bank records from the database
                var banks = await db.tbl_banks
                    .Where(x=>x.is_active==isActive)
                    .ToListAsync();

                if (banks.Count() > 0)
                {
                    // If there are records, return a success response model with the list of banks
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        data = _mapper.Map<List<BankResponseDTO>>(banks),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // If no records are found, return a failure response
                    return new ResponseModel<List<BankResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<BankResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a specific bank record from the database based on the provided bank ID
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankResponseDTO>> GetBank(string bankId)
        {
            try
            {
                // Retrieve the existing bank record from the database based on the provided ID
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(bankId)).FirstOrDefaultAsync();

                if (existingBank != null)
                {
                    // If the bank record is found, return a success response model with details
                    return new ResponseModel<BankResponseDTO>()
                    {
                        data = _mapper.Map<BankResponseDTO>(existingBank),
                        remarks = "Bank found successfully",
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing bank record based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankResponseDTO>> UpdateBank(UpdateBankDTO model)
        {
            try
            {
                // Retrieve the existing bank record from the database based on the provided ID
                var existingBank = await db.tbl_banks.Where(x => x.bank_id == Guid.Parse(model.bankId)).FirstOrDefaultAsync();

                if (existingBank != null)
                {
                    // If the bank record is found, update it with the properties from the provided model
                    existingBank = _mapper.Map(model, existingBank);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated bank record
                    return new ResponseModel<BankResponseDTO>()
                    {
                        remarks = $"Bank: {model.bankName} has been updated",
                        data = _mapper.Map<BankResponseDTO>(existingBank),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<BankResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}