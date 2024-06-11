using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.BankOtherSpecificationDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Models.DTOS.CitizenBankInfoDTO;

namespace BISPAPIORA.Repositories.BankOtherSpecificationServicesRepo
{
    public class BankOtherSpecificationService : IBankOtherSpecificationService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public BankOtherSpecificationService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds or updates registered bank other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddRegisteredBankOtherSpecification(AddRegisteredBankOtherSpecificationDTO model)
        {
            try
            {
                // Retrieve the foreign key (fkCitizenFamilyBankInfo) from the model
                var fkBankinfo = Guid.Parse(model.fkCitizenFamilyBankInfo);

                // Check if a registered bank other specification already exists for the provided citizen family bank info
                var bankOtherSpecification = await db.tbl_bank_other_specifications
                    .Where(x => x.fk_citizen_family_bank_info == fkBankinfo)
                    .FirstOrDefaultAsync();

                if (bankOtherSpecification == null)
                {
                    // If no existing record found, add a new bank other specification
                    var newBankOtherSpecification = _mapper.Map<tbl_bank_other_specification>(model);
                    db.tbl_bank_other_specifications.Add(newBankOtherSpecification);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added bank other specification
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Other Specification {model.bankOtherSpecification} has been added successfully",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(newBankOtherSpecification),
                    };
                }
                else
                {
                    // If an existing record is found, update the bank other specification using the provided model
                    var existingBankOtherSpecification = _mapper.Map<UpdateRegisteredBankOtherSpecificationDTO>(model);
                    existingBankOtherSpecification.bankOtherSpecificationId = bankOtherSpecification.bank_other_specification_id.ToString();

                    // Call the update method and return its response
                    var response = await UpdateRegisteredBankOtherSpecification(existingBankOtherSpecification);
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a registered bank other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateRegisteredBankOtherSpecification(UpdateRegisteredBankOtherSpecificationDTO model)
        {
            try
            {
                // Retrieve the existing bank other specification from the database based on the provided ID
                var existingBankOtherSpecification = await db.tbl_bank_other_specifications
                    .Where(x => x.bank_other_specification_id == Guid.Parse(model.bankOtherSpecificationId))
                    .FirstOrDefaultAsync();

                if (existingBankOtherSpecification != null)
                {
                    // Update the existing bank other specification with the properties from the provided model
                    existingBankOtherSpecification = _mapper.Map(model, existingBankOtherSpecification);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated bank other specification
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Bank Other Specification has been updated",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(existingBankOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    // If no matching record is found, return a failure response
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Adds or updates enrolled bank other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> AddEnrolledBankOtherSpecification(AddEnrolledBankOtherSpecificationDTO model)
        {
            try
            {
                // Retrieve the foreign key (fkCitizenBankInfo) from the model
                var fkBankinfo = Guid.Parse(model.fkCitizenBankInfo);

                // Check if an enrolled bank other specification already exists for the provided citizen bank info
                var bankOtherSpecification = await db.tbl_bank_other_specifications
                    .Where(x => x.fk_citizen_bank_info == fkBankinfo)
                    .FirstOrDefaultAsync();

                if (bankOtherSpecification == null)
                {
                    // If no existing record found, add a new bank other specification
                    var newBankOtherSpecification = _mapper.Map<tbl_bank_other_specification>(model);
                    db.tbl_bank_other_specifications.Add(newBankOtherSpecification);
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the added bank other specification
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Other Specification {model.bankOtherSpecification} has been added successfully",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(newBankOtherSpecification),
                    };
                }
                else
                {
                    // If an existing record is found, update the bank other specification using the provided model
                    var existingBankOtherSpecification = _mapper.Map<UpdateRegisteredBankOtherSpecificationDTO>(model);
                    existingBankOtherSpecification.bankOtherSpecificationId = bankOtherSpecification.bank_other_specification_id.ToString();

                    // Call the update method and return its response
                    var response = await UpdateRegisteredBankOtherSpecification(existingBankOtherSpecification);
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an enrolled bank other specification based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<BankRegisteredOtherSpecificationResponseDTO>> UpdateEnrolledBankOtherSpecification(UpdateEnrolledCitizenBankInfoDTO model)
        {
            try
            {
                // Retrieve the existing bank other specification from the database based on the provided ID
                var existingBankOtherSpecification = await db.tbl_bank_other_specifications
                    .Where(x => x.tbl_citizen_bank_info.fk_citizen == Guid.Parse(model.fkCitizen))
                    .FirstOrDefaultAsync();

                if (existingBankOtherSpecification != null)
                {
                    // Update the existing bank other specification with the properties from the provided model
                    existingBankOtherSpecification = _mapper.Map(model, existingBankOtherSpecification);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with details of the updated bank other specification
                    return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                    {
                        remarks = $"Bank Other Specification has been updated",
                        data = _mapper.Map<BankRegisteredOtherSpecificationResponseDTO>(existingBankOtherSpecification),
                        success = true,
                    };
                }
                else
                {
                    var newAddRequest = new AddEnrolledBankOtherSpecificationDTO();
                    newAddRequest= _mapper.Map(model, newAddRequest);
                    var response= await AddEnrolledBankOtherSpecification(newAddRequest);
                    // If no matching record is found, return a failure response
                    return response;
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<BankRegisteredOtherSpecificationResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
