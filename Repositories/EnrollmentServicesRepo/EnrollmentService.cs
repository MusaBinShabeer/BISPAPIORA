using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.EnrollmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenServicesRepo;
using Microsoft.EntityFrameworkCore;

namespace BISPAPIORA.Repositories.EnrollmentServicesRepo
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly ICitizenService citizenService;
        private readonly OraDbContext db;
        public EnrollmentService(IMapper mapper, OraDbContext db, ICitizenService citizenService)
        {
            _mapper = mapper;
            this.db = db;
            this.citizenService = citizenService;
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> AddEnrolledCitizen(AddEnrollmentDTO model)
        {
            try
            {
                var Citizen = await db.tbl_citizens.Where(x => x.citizen_cnic.ToLower().Equals(model.citizenCnic.ToLower())).FirstOrDefaultAsync();
                if (Citizen == null)
                {
                    var newCitizen = await citizenService.AddEnrolledCitizen(model);
                    if (newCitizen.success)
                    {
                        if (newCitizen.data != null)
                        {
                            model.fkCitizen = newCitizen.data.citizenId;
                            var newEnrollment = new tbl_enrollment();
                            newEnrollment = _mapper.Map<tbl_enrollment>(model);
                            await db.tbl_enrollment.AddAsync(newEnrollment);
                            await db.SaveChangesAsync();
                            var response = _mapper.Map<EnrollmentResponseDTO>(newCitizen.data);
                            response.enrollmentId = newEnrollment.enrollment_id.ToString();
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been registered successfully",
                                data = response,
                            };
                        }
                        else
                        {
                            return new ResponseModel<EnrollmentResponseDTO>()
                            {
                                success = true,
                                remarks = $"Citizen {model.citizenName} has been not been registered successfully",
                            };
                        }
                    }
                    else
                    {
                        return new ResponseModel<EnrollmentResponseDTO>()
                        {
                            success = true,
                            remarks = newCitizen.remarks
                        };
                    }
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen with name {model.citizenName} already exists",
                        data = _mapper.Map<EnrollmentResponseDTO>(Citizen),
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> DeleteEnrollment(string registrationId)
        {
            try
            {
                var registration = await db.tbl_registration.Where(x => x.registration_id == Guid.Parse(registrationId)).FirstOrDefaultAsync();
                if (registration != null)
                {
                    db.tbl_registration.Remove(registration);
                    await db.SaveChangesAsync();
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = "Enrollment Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<EnrollmentResponseDTO>> GetEnrollment(string registrationId)
        {
            try
            {
                var existingCitizen = await db.tbl_registration.Where(x => x.registration_id == Guid.Parse(registrationId)).Include(x => x.tbl_citizen).ThenInclude(x => x.citizen_tehsil).ThenInclude(x => x.tbl_district).ThenInclude(x => x.tbl_province).Include(x => x.tbl_citizen).ThenInclude(x => x.citizen_employement).Include(x => x.tbl_citizen).ThenInclude(x => x.citizen_education).FirstOrDefaultAsync();
                if (existingCitizen != null)
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        data = _mapper.Map<EnrollmentResponseDTO>(existingCitizen),
                        remarks = "Citizen found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<EnrollmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<EnrollmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
