using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;
using BISPAPIORA.Extensions;
using BISPAPIORA.Repositories.InnerServicesRepo;
using BISPAPIORA.Repositories.TransactionServicesRepo;
using BISPAPIORA.Repositories.ComplexMappersRepo;
using BISPAPIORA.Models.DTOS.AuthDTO;
using BISPAPIORA.Models.ENUMS;

namespace BISPAPIORA.Repositories.CitizenComplianceServicesRepo
{
    public class CitizenComplianceService : ICitizenComplianceService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        private readonly IInnerServices innerServices;
        private readonly ITransactionService transactionService;
        private readonly IComplexMapperServices complexMapperServices;
        public CitizenComplianceService(IMapper mapper, Dbcontext db, IInnerServices innerServices, ITransactionService transactionService, IComplexMapperServices complexMapperServices)
        {
            _mapper = mapper;
            this.db = db;
            this.innerServices = innerServices;
            this.transactionService = transactionService;
            this.complexMapperServices = complexMapperServices;
        }
        // Adds a new citizen compliance
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> AddCitizenCompliance(AddCitizenComplianceDTO model)
        {
            try
            {
                // Check if the citizen compliance already exists
                var citizenCompliance = await db.tbl_citizen_compliances
                    .Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)) && x.citizen_compliance_quarter_code.Equals(model.quarterCode))
                    .FirstOrDefaultAsync();
                if (citizenCompliance == null)
                {
                    var citizenScheme = await db.tbl_citizen_schemes.Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen))).FirstOrDefaultAsync();
                    var betweenquarters = innerServices.GetQuarterCodesBetween(citizenScheme.citizen_scheme_quarter_code.Value, model.quarterCode);
                    var expectedSavingsPerQuarterDecimal = citizenScheme.citizen_scheme_saving_amount * 3;
                    var expectedSavingsPerQuarter = double.Parse(expectedSavingsPerQuarterDecimal.ToString());
                    var expectedSaving = await innerServices.GetTotalExpectedSavingAmount(betweenquarters, Guid.Parse(model.fkCitizen), expectedSavingsPerQuarter);
                    var actualSavings = model.transactionDTO.Sum(transaction =>
                    {
                        if (Enum.TryParse(transaction.transactionType, out TransactionTypeEnum transactionType))
                        {
                            
                                return transactionType == TransactionTypeEnum.Debit ? -transaction.transactionAmount : +transaction.transactionAmount;
                           
                        }
                        else
                        {
                            // Handle parsing error for transaction type
                            Console.WriteLine("Invalid transaction type: " + transaction.transactionType);
                            return 0; // or any default value
                        }
                    });
                    var newCitizenCompliance = new tbl_citizen_compliance();
                    newCitizenCompliance = _mapper.Map<tbl_citizen_compliance>(model);
                    await db.tbl_citizen_compliances.AddAsync(newCitizenCompliance);                    
                    await db.SaveChangesAsync();
                    if (actualSavings >= expectedSaving)
                    {
                        newCitizenCompliance.is_compliant = true;
                        var paymentToUpdate = await db.tbl_payments
                            .Where(x => x.fk_citizen == Guid.Parse(model.fkCitizen) && x.payment_quarter_code == newCitizenCompliance.citizen_compliance_quarter_code)
                            .FirstOrDefaultAsync();
                        paymentToUpdate.paid_amount = paymentToUpdate.due_amount;
                        paymentToUpdate.fk_compliance = newCitizenCompliance.citizen_compliance_id;
                        await db.SaveChangesAsync();
                    }
                    foreach (var transaction in model.transactionDTO)
                    {
                        transaction.fkCompliance = newCitizenCompliance.citizen_compliance_id.ToString();
                        var transactionResponse = await transactionService.AddTransaction(transaction);
                    }
                    var payment = await db.tbl_payments.Where(x => x.fk_citizen == newCitizenCompliance.fk_citizen && x.payment_quarter_code == newCitizenCompliance.citizen_compliance_quarter_code).FirstOrDefaultAsync();
                    if(payment != null ) 
                    {
                        payment.fk_compliance = newCitizenCompliance.citizen_compliance_id;
                        await db.SaveChangesAsync();
                    }
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Compliance has been added successfully",
                        data = _mapper.Map<CitizenComplianceResponseDTO>(newCitizenCompliance),
                    };
                }
                else
                {
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = false,
                        remarks = "Already Added"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a citizen compliance based on the provided citizen compliance ID
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> DeleteCitizenCompliance(string citizenComplianceId)
        {
            try
            {
                // Find the existing citizen compliance in the database based on the provided ID
                var existingCitizenCompliance = await db.tbl_citizen_compliances
                    .Where(x => x.citizen_compliance_id == Guid.Parse(citizenComplianceId))
                    .FirstOrDefaultAsync();

                if (existingCitizenCompliance != null)
                {
                    // Remove the citizen compliance from the database and save changes
                    db.tbl_citizen_compliances.Remove(existingCitizenCompliance);
                    await db.SaveChangesAsync();

                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = "Citizen Compliance Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided ID
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all citizen compliances
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenCompliancesList()
        {
            try
            {
                // Retrieve all citizen compliances from the database
                var citizenCompliances = await db.tbl_citizen_compliances
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_transactions)
                    .ToListAsync();
                if (citizenCompliances.Count() > 0)
                {
                    // Create a success response model with the list of citizen compliances
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = complexMapperServices.ComplexAutomapperForCompliance().Map<List<CitizenComplianceResponseDTO>>(citizenCompliances),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a specific citizen compliance by citizen compliance ID
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> GetCitizenCompliance(string citizenComplianceId)
        {
            try
            {
                // Find the existing citizen compliance in the database based on the provided ID
                var existingCitizenCompliance = await db.tbl_citizen_compliances
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_transactions)
                    .Where(x => x.citizen_compliance_id == Guid.Parse(citizenComplianceId))
                    .FirstOrDefaultAsync();

                if (existingCitizenCompliance != null)
                {
                    // Return a success response model with the found citizen compliance
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        data = complexMapperServices.ComplexAutomapperForCompliance().Map<CitizenComplianceResponseDTO>(existingCitizenCompliance),
                        remarks = "Citizen Compliance found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided ID
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a citizen compliance based on the provided model
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> UpdateCitizenCompliance(UpdateCitizenComplianceDTO model)
        {
            try
            {
                // Find the existing citizen compliance in the database based on the provided citizen compliance ID
                var existingCitizenCompliance = await db.tbl_citizen_compliances
                    .Include(x => x.tbl_citizen)
                    .Where(x => x.citizen_compliance_id == Guid.Parse(model.citizenComplianceId))
                    .FirstOrDefaultAsync();

                if (existingCitizenCompliance != null)
                {
                    // Update the existing citizen compliance with the provided model and save changes
                    existingCitizenCompliance = _mapper.Map(model, existingCitizenCompliance);
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated citizen compliance
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        remarks = $"Citizen Compliance has been updated",
                        data = _mapper.Map<CitizenComplianceResponseDTO>(existingCitizenCompliance),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided citizen compliance ID
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenComplianceResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of citizen compliances based on citizen ID
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenComplianceByCitizenId(string citizenId)
        {
            try
            {
                // Find all citizen compliances in the database based on the provided citizen ID
                var existingDistricts = await db.tbl_citizen_compliances
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_transactions)
                    .Where(x => x.fk_citizen == Guid.Parse(citizenId))
                    .ToListAsync();

                if (existingDistricts.Count > 0)
                {
                    // Return a success response model with the list of citizen compliances
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = complexMapperServices.ComplexAutomapperForCompliance().Map<List<CitizenComplianceResponseDTO>>(existingDistricts),
                        remarks = "Citizen Compliances found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found for the provided citizen ID
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of citizen compliances based on citizen CNIC
        public async Task<ResponseModel<List<CitizenComplianceResponseDTO>>> GetCitizenComplianceByCitizenCnic(string citizenCnic)
        {
            try
            {
                // Find all citizen compliances in the database based on the provided citizen CNIC
                var existingDistricts = await db.tbl_citizen_compliances
                    .Include(x => x.tbl_citizen)
                    .Include(x => x.tbl_transactions)
                    .Where(x => x.tbl_citizen.citizen_cnic == citizenCnic)
                    .ToListAsync();

                if (existingDistricts.Count > 0)
                {
                    // Return a success response model with the list of citizen compliances
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = complexMapperServices.ComplexAutomapperForCompliance().Map<List<CitizenComplianceResponseDTO>>(existingDistricts),
                        remarks = "Citizen Compliances found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found for the provided citizen CNIC
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }



    }
}
