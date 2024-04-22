using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.BankStatementDTO;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Repositories.BankStatementServicesRepo
{
    public class BankStatementService : IBankStatementService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public BankStatementService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }


        // Adds a new Bank Statement.
        public async Task<ResponseModel<BankStatementResponseDTO>> AddBankStatement(AddBankStatementDTO model)
        {
            try
            {
                // Check if attachment with the same CNIC exists
                var BankStatement = await db.tbl_bank_statements
                    .Where(x => x.cnic.ToLower().Equals(model.bankStatementCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (BankStatement == null)
                {
                    // Create a new attachment and add it to the database
                    var newBankStatement = _mapper.Map<tbl_bank_statement>(model);
                    db.tbl_bank_statements.Add(newBankStatement);
                    await db.SaveChangesAsync();

                    // Return success response with added attachment details
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = true,
                        remarks = $"Bank Statement {model.bankStatementName} added successfully",
                        data = _mapper.Map<BankStatementResponseDTO>(newBankStatement),
                    };
                }
                else
                {
                    // Return failure response if attachment with the same CNIC already exists
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = false,
                        remarks = $"Bank Statement with CNIC {model.bankStatementCnic} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Adds a foreign key to an existing Bank Statement.
        public async Task<ResponseModel<BankStatementResponseDTO>> AddFkCitizenComplianceToAttachment(AddBankStatementDTO model)
        {
            try
            {
                // Check if the attachment with the given CNIC exists
                var bankStatement = await db.tbl_bank_statements
                    .Where(x => x.cnic.ToLower().Equals(model.bankStatementCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (bankStatement != null)
                {
                    // Add foreign key (citizen Compliance ID) to the existing attachment
                    bankStatement.fk_citizen_compliance = Guid.Parse(model.fkCitizenCompliance);
                    await db.SaveChangesAsync();

                    // Return success response
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = true,
                        remarks = "Success",
                    };
                }
                else
                {
                    // Return failure response if the attachment does not exist
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = false,
                        remarks = $"Bank Statement with CNIC {model.bankStatementCnic} does not exist"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an Bank Statement.
        public async Task<ResponseModel<BankStatementResponseDTO>> DeleteBankStatement(string bankStatementId)
        {
            try
            {
                // Find existing Bank Statement based on provided ID
                var existingBankStatement = await db.tbl_bank_statements
                    .Where(x => x.id == Guid.Parse(bankStatementId))
                    .FirstOrDefaultAsync();

                if (existingBankStatement != null)
                {
                    // Remove the Bank Statement
                    db.tbl_bank_statements.Remove(existingBankStatement);
                    await db.SaveChangesAsync();

                    // Return success response
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        remarks = "Bank Statement Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all Bank Statements.
        public async Task<ResponseModel<List<BankStatementResponseDTO>>> GetBankStatementsList()
        {
            try
            {
                // Retrieve all Bank Statements
                var bankStatements = await db.tbl_bank_statements.Include(x => x.tbl_citizen_compliance).ThenInclude(x => x.tbl_citizen)
                    .ToListAsync();

                if (bankStatements.Count() > 0)
                {
                    // Return success response with Bank Statements
                    return new ResponseModel<List<BankStatementResponseDTO>>()
                    {
                        data = _mapper.Map<List<BankStatementResponseDTO>>(bankStatements),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no records are found
                    return new ResponseModel<List<BankStatementResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<List<BankStatementResponseDTO>>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of an Bank Statement by its ID.s>
        public async Task<ResponseModel<BankStatementResponseDTO>> GetBankStatement(string bankStatementId)
        {
            try
            {
                // Find the existing Bank Statement based on the provided ID
                var existingBankStatement = await db.tbl_bank_statements.Include(x => x.tbl_citizen_compliance).ThenInclude(x => x.tbl_citizen)
                    .Where(x => x.id == Guid.Parse(bankStatementId))
                    .FirstOrDefaultAsync();

                if (existingBankStatement != null)
                {
                    // Return success response with the Bank Statement
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        data = _mapper.Map<BankStatementResponseDTO>(existingBankStatement),
                        remarks = "Bank Statement found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of an Bank Statement by citizen's CNIC.
        public async Task<ResponseModel<BankStatementResponseDTO>> GetBankStatementByCitizenCnic(string citizenCnic)
        {
            try
            {
                // Find the existing Bank Statement based on the provided CNIC
                var existingBankStatement = await db.tbl_bank_statements.Include(x => x.tbl_citizen_compliance).ThenInclude(x => x.tbl_citizen)
                    .Where(x => x.cnic == citizenCnic)
                    .FirstOrDefaultAsync();

                if (existingBankStatement != null)
                {
                    // Return success response with the Bank Statement
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        data = _mapper.Map<BankStatementResponseDTO>(existingBankStatement),
                        remarks = "Bank Statement found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates details of an Bank Statement.
        public async Task<ResponseModel<BankStatementResponseDTO>> UpdateBankStatement(UpdateBankStatementDTO model)
        {
            try
            {
                // Find the existing Bank Statement based on the provided ID
                var existingBankStatement = await db.tbl_bank_statements
                    .Where(x => x.id == Guid.Parse(model.bankStatementId))
                    .FirstOrDefaultAsync();

                if (existingBankStatement != null)
                {
                    // Update the Bank Statement with new information
                    existingBankStatement = _mapper.Map(model, existingBankStatement);
                    await db.SaveChangesAsync();

                    // Return success response with the updated Bank Statement
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        remarks = $"Bank Statement: {model.bankStatementName} has been updated",
                        data = _mapper.Map<BankStatementResponseDTO>(existingBankStatement),
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<BankStatementResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<BankStatementResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of Bank Statements associated with a citizen ID.
        public async Task<ResponseModel<List<BankStatementResponseDTO>>> GetBankStatementByCitizenId(string citizenId)
        {
            try
            {
                // Find Bank Statements associated with the provided citizen ID
                var existingBankStatements = await db.tbl_bank_statements.Include(x => x.tbl_citizen_compliance).ThenInclude(x => x.tbl_citizen)
                    .Where(x => x.tbl_citizen_compliance.fk_citizen == Guid.Parse(citizenId))
                    .ToListAsync();

                if (existingBankStatements != null && existingBankStatements.Any())
                {
                    // Return success response with the list of Bank Statements
                    return new ResponseModel<List<BankStatementResponseDTO>>()
                    {
                        data = _mapper.Map<List<BankStatementResponseDTO>>(existingBankStatements),
                        remarks = "Bank Statements found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<List<BankStatementResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<List<BankStatementResponseDTO>>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

    }
}
