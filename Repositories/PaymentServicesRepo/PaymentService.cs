using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.PaymentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.DistrictDTO;

namespace BISPAPIORA.Repositories.PaymentServicesRepo
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public PaymentService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }

        // Adds a new Payment based on the provided model
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<PaymentResponseDTO>> AddPayment(AddPaymentDTO model)
        {
            try
            {
                // Create a new Payment entity and map properties from the provided model
                var newPayment = new tbl_payment();
                newPayment = _mapper.Map<tbl_payment>(model);               
                // Add the new Payment to the database and save changes
                db.tbl_payments.Add(newPayment);
                await db.SaveChangesAsync();

                // Return a success response model with the added Payment details
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = true,
                    remarks = $"Payment has been added successfully",
                    data = _mapper.Map<PaymentResponseDTO>(newPayment),
                };
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        public async Task<ResponseModel<PaymentResponseDTO>> AddPayment(string citizenCnic,int quarterCode, double paidAmount)
        {
            try
            {
                // Create a new Payment entity and map properties from the provided model
                var payment = await db.tbl_payments.Where(x=>x.tbl_citizen.citizen_cnic== citizenCnic&& x.payment_quarter_code== quarterCode).FirstOrDefaultAsync();
                if (payment != null)
                {
                    payment.paid_amount = decimal.Parse(paidAmount.ToString());
                    payment.actual_due_amount= payment.actual_due_amount- payment.paid_amount;
                    // Update Payment to the database and save changes
                     await db.SaveChangesAsync();
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Payment has been added successfully",
                        data = _mapper.Map<PaymentResponseDTO>(payment),
                    };
                }
                else
                {
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        success = false,
                        remarks = $"No Record",                       
                    };

                }

                // Return a success response model with the added Payment details
                
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a Payment based on the provided PaymentId
        // Returns a response model indicating the success or failure of the operation
        public async Task<ResponseModel<PaymentResponseDTO>> DeletePayment(string paymentId)
        {
            try
            {
                // Retrieve the existing Payment from the database based on the PaymentId
                var existingPayment = await db.tbl_payments.Where(x => x.payment_id == Guid.Parse(paymentId)).FirstOrDefaultAsync();

                // If the Payment exists, remove it and save changes to the database
                if (existingPayment != null)
                {
                    db.tbl_payments.Remove(existingPayment);
                    await db.SaveChangesAsync();

                    // Return a success response model indicating that the Payment has been deleted
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        remarks = "Existing Payment Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all Payments along with associated citizen details
        // Returns a response model containing the list of Payments or an error message
        public async Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentsList()
        {
            try
            {
                // Retrieve all Payments from the database, including associated citizen details
                var payments = await db.tbl_payments.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_compliance).ToListAsync();

                // Check if there are Payments in the list
                if (payments.Count() > 0)
                {
                    // Return a success response model with the list of Payments
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<PaymentResponseDTO>>(payments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no Payments are found
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<PaymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was a Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a Payment based on the provided PaymentId,
        // including associated citizen details.
        // Returns a response model containing the Payment details or an error message.
        public async Task<ResponseModel<PaymentResponseDTO>> GetPayment(string paymentsId)
        {
            try
            {
                // Retrieve the Payment with the specified ID, including associated citizen details
                var existingPayment = await db.tbl_payments.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_compliance).Where(x => x.payment_id == Guid.Parse(paymentsId)).FirstOrDefaultAsync();

                if (existingPayment != null)
                {
                    // Return a success response model with the mapped Payment details
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        data = _mapper.Map<PaymentResponseDTO>(existingPayment),
                        remarks = "Payment found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a Payment based on the provided model.
        // Returns a response model indicating the success or failure of the operation.
        public async Task<ResponseModel<PaymentResponseDTO>> UpdatePayment(UpdatePaymentDTO model)
        {
            try
            {
                // Retrieve the existing Payment based on the provided PaymentId
                var existingPayment = await db.tbl_payments.Include(x => x.tbl_citizen).Where(x => x.payment_id == Guid.Parse(model.paymentId)).FirstOrDefaultAsync();

                if (existingPayment != null)
                {
                    // Map the properties from the provided model to the existing Payment
                    existingPayment = _mapper.Map(model, existingPayment);

                    // Save changes to the database
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated Payment details
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        remarks = $"Payment: {model.paymentId} has been updated",
                        data = _mapper.Map<PaymentResponseDTO>(existingPayment),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching record is found
                    return new ResponseModel<PaymentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<PaymentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of Payments associated with the provided citizenId,
        // including details about each Payment and the associated citizen.
        // Returns a response model containing the list of Payments or an error message.
        public async Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenId(string citizenId)
        {
            try
            {
                // Retrieve Payments associated with the specified citizen ID, including citizen details
                var existingPayments = await db.tbl_payments.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_compliance).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();

                if (existingPayments != null)
                {
                    // Return a success response model with the mapped list of Payments
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<PaymentResponseDTO>>(existingPayments),
                        remarks = "Payments found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching records are found
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<PaymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of Payments associated with the provided citizen CNIC,
        // including details about each Payment and the associated citizen.
        // Returns a response model containing the list of Payments or an error message.
        public async Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenCnic(string citizenCnic)
        {
            try
            {
                // Retrieve Payments associated with the specified citizen ID, including citizen details
                var existingPayments = await db.tbl_payments.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_compliance).Where(x => x.tbl_citizen.citizen_cnic == citizenCnic).ToListAsync();

                if (existingPayments != null)
                {
                    // Return a success response model with the mapped list of Payments
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<PaymentResponseDTO>>(existingPayments),
                        remarks = "Payments found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching records are found
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<PaymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of Payments associated with the provided citizen Compliance Id,
        // including details about each Payment and the associated citizen Compliance.
        // Returns a response model containing the list of Payments or an error message.
        public async Task<ResponseModel<List<PaymentResponseDTO>>> GetPaymentByCitizenComplianceId(string citizenComplainceId)
        {
            try
            {
                // Retrieve Payments associated with the specified citizen Compliance ID, including citizen details
                var existingPayments = await db.tbl_payments.Include(x => x.tbl_citizen).Include(x => x.tbl_citizen_compliance).Where(x => x.fk_compliance == Guid.Parse(citizenComplainceId)).ToListAsync();

                if (existingPayments != null)
                {
                    // Return a success response model with the mapped list of Payments
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        data = _mapper.Map<List<PaymentResponseDTO>>(existingPayments),
                        remarks = "Payments found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no matching records are found
                    return new ResponseModel<List<PaymentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model with details about the exception if an error occurs
                return new ResponseModel<List<PaymentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
