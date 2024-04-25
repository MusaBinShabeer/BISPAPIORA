using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenSchemeDTO;
using BISPAPIORA.Repositories.PaymentServicesRepo;
using BISPAPIORA.Models.DTOS.PaymentDTO;
using BISPAPIORA.Repositories.InnerServicesRepo;

namespace BISPAPIORA.Repositories.CitizenSchemeServicesRepo
{
    public class CitizenSchemeService : ICitizenSchemeService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        private readonly IPaymentService paymentService;
        private readonly IInnerServices innerServices;
        public CitizenSchemeService(IMapper mapper, Dbcontext db, IPaymentService paymentService, IInnerServices innerServices)
        {
            _mapper = mapper;
            this.db = db;
            this.paymentService = paymentService;
            this.innerServices = innerServices;
        }

        // Adds or updates a citizen scheme based on the provided model
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> AddCitizenScheme(AddCitizenSchemeDTO model)
        {
            try
            {
                // Check if a citizen scheme already exists for the provided citizen
                var citizenScheme = await db.tbl_citizen_schemes
                    .Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)))
                    .FirstOrDefaultAsync();

                if (citizenScheme == null)
                {
                    // If no citizen scheme exists, create a new one
                    var newCitizenScheme = _mapper.Map<tbl_citizen_scheme>(model);
                    db.tbl_citizen_schemes.Add(newCitizenScheme);
                    await db.SaveChangesAsync();
                    var allQuarters = innerServices.GetAllQuarterCodes(newCitizenScheme.citizen_scheme_quarter_code.Value);
                    foreach (var quarterCode in allQuarters)
                    {
                        var addPaymentDTo = _mapper.Map<AddPaymentDTO>((newCitizenScheme, quarterCode.quarterCode));
                        var responsePayment = await paymentService.AddPayment(addPaymentDTo);
                    } 
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Scheme has been added successfully",
                        data = _mapper.Map<CitizenSchemeResponseDTO>(newCitizenScheme),
                    };
                }
                else
                {
                    // If a citizen scheme already exists, update it using the provided model
                    var existingCitizenScheme = _mapper.Map<UpdateCitizenSchemeDTO>(model);
                    existingCitizenScheme.citizenSchemeId = citizenScheme.citizen_scheme_id.ToString();
                    var response = await UpdateCitizenScheme(existingCitizenScheme);

                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Deletes a citizen scheme based on the provided citizen scheme ID
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> DeleteCitizenScheme(string citizenSchemeId)
        {
            try
            {
                // Find the existing citizen scheme in the database based on the provided ID
                var existingCitizenScheme = await db.tbl_citizen_schemes
                    .Where(x => x.citizen_scheme_id == Guid.Parse(citizenSchemeId))
                    .FirstOrDefaultAsync();

                if (existingCitizenScheme != null)
                {
                    // Remove the citizen scheme and save changes
                    db.tbl_citizen_schemes.Remove(existingCitizenScheme);
                    await db.SaveChangesAsync();

                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = "Citizen Scheme Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided ID
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all citizen schemes
        public async Task<ResponseModel<List<CitizenSchemeResponseDTO>>> GetCitizenSchemesList()
        {
            try
            {
                // Retrieve all citizen schemes from the database
                var citizenSchemes = await db.tbl_citizen_schemes.ToListAsync();

                if (citizenSchemes.Count() > 0)
                {
                    // Create a success response model with the list of citizen schemes
                    return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenSchemeResponseDTO>>(citizenSchemes),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no citizen schemes are found
                    return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<CitizenSchemeResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a citizen scheme based on the provided citizen scheme ID
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> GetCitizenScheme(string citizenSchemeId)
        {
            try
            {
                // Find the existing citizen scheme in the database based on the provided ID
                var existingCitizenScheme = await db.tbl_citizen_schemes
                    .Where(x => x.citizen_scheme_id == Guid.Parse(citizenSchemeId))
                    .FirstOrDefaultAsync();

                if (existingCitizenScheme != null)
                {
                    // Create a success response model with the found citizen scheme
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        data = _mapper.Map<CitizenSchemeResponseDTO>(existingCitizenScheme),
                        remarks = "Citizen Scheme found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided ID
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates a citizen scheme based on the provided model
        public async Task<ResponseModel<CitizenSchemeResponseDTO>> UpdateCitizenScheme(UpdateCitizenSchemeDTO model)
        {
            try
            {
                // Find the existing citizen scheme in the database based on the provided ID
                var existingCitizenScheme = await db.tbl_citizen_schemes
                    .Where(x => x.citizen_scheme_id == Guid.Parse(model.citizenSchemeId))
                    .FirstOrDefaultAsync();

                if (existingCitizenScheme != null)
                {
                    // Update the citizen scheme using the provided model and save changes
                    existingCitizenScheme = _mapper.Map(model, existingCitizenScheme);
                    await db.SaveChangesAsync();

                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        remarks = $"Citizen Scheme has been updated",
                        data = _mapper.Map<CitizenSchemeResponseDTO>(existingCitizenScheme),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no record is found for the provided ID
                    return new ResponseModel<CitizenSchemeResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<CitizenSchemeResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}
