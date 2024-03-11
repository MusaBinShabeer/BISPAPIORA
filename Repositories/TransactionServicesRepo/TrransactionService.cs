using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.TransactionDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.DistrictDTO;

namespace BISPAPIORA.Repositories.TransactionServicesRepo
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public TransactionService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new transaction based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<TransactionResponseDTO>> AddTransaction(AddTransactionDTO model)
        {
            try
            {
                // Create a new transaction entity and map properties from the provided model
                var newTransaction = new tbl_transaction();
                newTransaction = _mapper.Map<tbl_transaction>(model);

                // Add the new transaction to the database and save changes
                db.tbl_transactions.Add(newTransaction);
                await db.SaveChangesAsync();

                // Return a success response model with the added transaction details
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = true,
                    remarks = $"Transaction has been added successfully",
                    data = _mapper.Map<TransactionResponseDTO>(newTransaction),
                };
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a transaction based on the provided transactionId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<TransactionResponseDTO>> DeleteTransaction(string transactionId)
        {
            try
            {
                // Retrieve the existing transaction from the database based on the transactionId
                var existingTransaction = await db.tbl_transactions.Where(x => x.transaction_id == Guid.Parse(transactionId)).FirstOrDefaultAsync();

                // If the transaction exists, remove it and save changes to the database
                if (existingTransaction != null)
                {
                    db.tbl_transactions.Remove(existingTransaction);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the transaction has been deleted
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = "Existing Transaction Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all transactions along with associated citizen details
        // Returns a response model containing the list of transactions or an error message
        public async Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionsList()
        {
            try
            {
                // Retrieve all transactions from the database, including associated citizen details
                var transactions = await db.tbl_transactions.Include(x => x.tbl_citizen).ToListAsync();

                // Check if there are transactions in the list
                if (transactions.Count() > 0)
                {
                    // Return a success response model with the list of transactions
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        data = _mapper.Map<List<TransactionResponseDTO>>(transactions),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no transactions are found
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<TransactionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a transaction based on the provided transactionId,
        // including associated citizen details.
        // Returns a response model containing the transaction details or an error message.
        public async Task<ResponseModel<TransactionResponseDTO>> GetTransaction(string transactionsId)
        {
            try
            {
                // Retrieve the transaction with the specified ID, including associated citizen details
                var existingTransaction = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.transaction_id == Guid.Parse(transactionsId)).FirstOrDefaultAsync();

                if (existingTransaction != null)
                {
                    // Return a success response model with the mapped transaction details
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        data = _mapper.Map<TransactionResponseDTO>(existingTransaction),
                        remarks = "Transaction found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a transaction based on the provided model.
        // Returns a response model indicating the success or failure of the operation.
        public async Task<ResponseModel<TransactionResponseDTO>> UpdateTransaction(UpdateTransactionDTO model)
        {
            try
            {
                // Retrieve the existing transaction based on the provided transactionId
                var existingTransaction = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.transaction_id == Guid.Parse(model.transactionId)).FirstOrDefaultAsync();

                if (existingTransaction != null)
                {
                    // Map the properties from the provided model to the existing transaction
                    existingTransaction = _mapper.Map(model, existingTransaction);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated transaction details
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = $"Transaction: {model.transactionId} has been updated",
                        data = _mapper.Map<TransactionResponseDTO>(existingTransaction),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of transactions associated with the provided citizenId,
        // including details about each transaction and the associated citizen.
        // Returns a response model containing the list of transactions or an error message.
        public async Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionByCitizenId(string citizenId)
        {
            try
            {
                // Retrieve transactions associated with the specified citizen ID, including citizen details
                var existingTransactions = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();

                if (existingTransactions != null)
                {
                    // Return a success response model with the mapped list of transactions
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        data = _mapper.Map<List<TransactionResponseDTO>>(existingTransactions),
                        remarks = "Transactions found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching records are found
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<TransactionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
