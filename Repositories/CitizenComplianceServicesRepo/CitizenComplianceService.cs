﻿using AutoMapper;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Models.DTOS.CitizenComplianceDTO;

namespace BISPAPIORA.Repositories.CitizenComplianceServicesRepo
{
    public class CitizenComplianceService : ICitizenComplianceService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenComplianceService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        // Adds a new citizen compliance
        public async Task<ResponseModel<CitizenComplianceResponseDTO>> AddCitizenCompliance(AddCitizenComplianceDTO model)
        {
            try
            {
                // Check if the citizen compliance already exists
                var citizenCompliance = await db.tbl_citizen_compliances
                    .Where(x => x.fk_citizen.Equals(Guid.Parse(model.fkCitizen)))
                    .FirstOrDefaultAsync();

                if (citizenCompliance == null)
                {
                    // If not, create a new citizen compliance
                    var newCitizenCompliance = new tbl_citizen_compliance();
                    newCitizenCompliance = _mapper.Map<tbl_citizen_compliance>(model);
                    db.tbl_citizen_compliances.Add(newCitizenCompliance);
                    await db.SaveChangesAsync();

                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Compliance has been added successfully",
                        data = _mapper.Map<CitizenComplianceResponseDTO>(newCitizenCompliance),
                    };
                }
                else
                {
                    // If it exists, update the existing citizen compliance
                    var existingCitizenCompliance = _mapper.Map<UpdateCitizenComplianceDTO>(model);
                    existingCitizenCompliance.citizenComplianceId = citizenCompliance.citizen_compliance_id.ToString();
                    var response = await UpdateCitizenCompliance(existingCitizenCompliance);

                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        success = response.success,
                        remarks = response.remarks
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
                    .ToListAsync();

                if (citizenCompliances.Count() > 0)
                {
                    // Create a success response model with the list of citizen compliances
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenComplianceResponseDTO>>(citizenCompliances),
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
                    .Where(x => x.citizen_compliance_id == Guid.Parse(citizenComplianceId))
                    .FirstOrDefaultAsync();

                if (existingCitizenCompliance != null)
                {
                    // Return a success response model with the found citizen compliance
                    return new ResponseModel<CitizenComplianceResponseDTO>()
                    {
                        data = _mapper.Map<CitizenComplianceResponseDTO>(existingCitizenCompliance),
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
                    .Where(x => x.fk_citizen == Guid.Parse(citizenId))
                    .ToListAsync();

                if (existingDistricts.Count > 0)
                {
                    // Return a success response model with the list of citizen compliances
                    return new ResponseModel<List<CitizenComplianceResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenComplianceResponseDTO>>(existingDistricts),
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

    }
}
