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
        public async Task<ResponseModel<TransactionResponseDTO>> AddTransaction(AddTransactionDTO model)
        {
            try
            {
                var newTransaction = new tbl_transaction();
                newTransaction = _mapper.Map<tbl_transaction>(model);
                db.tbl_transactions.Add(newTransaction);
                await db.SaveChangesAsync();
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = true,
                    remarks = $"Transaction has been added successfully",
                    data = _mapper.Map<TransactionResponseDTO>(newTransaction),
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TransactionResponseDTO>> DeleteTransaction(string bankId)
        {
            try
            {
                var existingTransaction = await db.tbl_transactions.Where(x => x.transaction_id == Guid.Parse(bankId)).FirstOrDefaultAsync();
                if (existingTransaction != null)
                {
                    db.tbl_transactions.Remove(existingTransaction);
                    await db.SaveChangesAsync();
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = "Existing Transaction Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionsList()
        {
            try
            {
                var transactions = await db.tbl_transactions.Include(x => x.tbl_citizen).ToListAsync();
                if (transactions.Count() > 0)
                {
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        data = _mapper.Map<List<TransactionResponseDTO>>(transactions),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<TransactionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TransactionResponseDTO>> GetTransaction(string transactionsId)
        {
            try
            {
                var existingTransaction = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.transaction_id == Guid.Parse(transactionsId)).FirstOrDefaultAsync();
                if (existingTransaction != null)
                {
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        data = _mapper.Map<TransactionResponseDTO>(existingTransaction),
                        remarks = "Transaction found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<TransactionResponseDTO>> UpdateTransaction(UpdateTransactionDTO model)
        {
            try
            {
                var existingTransaction = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.transaction_id == Guid.Parse(model.transactionId)).FirstOrDefaultAsync();
                if (existingTransaction != null)
                {
                    existingTransaction = _mapper.Map(model, existingTransaction);
                    await db.SaveChangesAsync();
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        remarks = $"Transaction: {model.transactionId} has been updated",
                        data = _mapper.Map<TransactionResponseDTO>(existingTransaction),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<TransactionResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<TransactionResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<TransactionResponseDTO>>> GetTransactionByCitizenId(string citizenId)
        {
            try
            {
                var existingTransactions = await db.tbl_transactions.Include(x => x.tbl_citizen).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingTransactions != null)
                {
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        data = _mapper.Map<List<TransactionResponseDTO>>(existingTransactions),
                        remarks = "Transactions found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<TransactionResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<TransactionResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
