using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.CitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;

namespace BISPAPIORA.Repositories.CitizenAttachmentServicesRepo
{
    public class CitizenAttachmentService : ICitizenAttachmentService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public CitizenAttachmentService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<CitizenAttachmentResponseDTO>> AddCitizenAttachment(AddCitizenAttachmentDTO model)
        {
            try
            {
                var citizenAttachment = await db.tbl_citizen_attachments.Where(x => x.attachment_name.ToLower().Equals(model.citizenAttachmentName.ToLower())).FirstOrDefaultAsync();
                if (citizenAttachment == null)
                {
                    var newCitizenAttachment = new tbl_citizen_attachment();
                    newCitizenAttachment = _mapper.Map<tbl_citizen_attachment>(model);
                    db.tbl_citizen_attachments.Add(newCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Citizen Attachment {model.citizenAttachmentName} has been added successfully",
                        data = _mapper.Map<CitizenAttachmentResponseDTO>(newCitizenAttachment),
                    };
                }
                else
                {
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Citizen Attachment with name {model.citizenAttachmentName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenAttachmentResponseDTO>> DeleteCitizenAttachment(string citizenAttachmentId)
        {
            try
            {
                var existingCitizenAttachment = await db.tbl_citizen_attachments.Where(x => x.citizen_attachment_id == Guid.Parse(citizenAttachmentId)).FirstOrDefaultAsync();
                if (existingCitizenAttachment != null)
                {
                    db.tbl_citizen_attachments.Remove(existingCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        remarks = "Citizen Attachment Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenAttachmentResponseDTO>>> GetCitizenAttachmentsList()
        {
            try
            {
                var citizenAttachments = await db.tbl_citizen_attachments.Include(x => x.tbl_citizen).ToListAsync();
                if (citizenAttachments.Count() > 0)
                {
                    return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenAttachmentResponseDTO>>(citizenAttachments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenAttachmentResponseDTO>> GetCitizenAttachment(string citizenAttachmentId)
        {
            try
            {
                var existingCitizenAttachment = await db.tbl_citizen_attachments.Include(x => x.tbl_citizen).Where(x => x.citizen_attachment_id == Guid.Parse(citizenAttachmentId)).FirstOrDefaultAsync();
                if (existingCitizenAttachment != null)
                {
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        data = _mapper.Map<CitizenAttachmentResponseDTO>(existingCitizenAttachment),
                        remarks = "CitizenAttachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<CitizenAttachmentResponseDTO>> UpdateCitizenAttachment(UpdateCitizenAttachmentDTO model)
        {
            try
            {
                var existingCitizenAttachment = await db.tbl_citizen_attachments.Include(x => x.tbl_citizen).Where(x => x.citizen_attachment_id == Guid.Parse(model.citizenAttachmentId)).FirstOrDefaultAsync();
                if (existingCitizenAttachment != null)
                {
                    existingCitizenAttachment = _mapper.Map(model, existingCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        remarks = $"CitizenAttachment: {model.citizenAttachmentName} has been updated",
                        data = _mapper.Map<CitizenAttachmentResponseDTO>(existingCitizenAttachment),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<CitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<CitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<CitizenAttachmentResponseDTO>>> GetCitizenAttachmentByCitizenId(string citizenId)
        {
            try
            {
                var existingDistricts = await db.tbl_citizen_thumb_prints.Include(x => x.tbl_citizen).Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingDistricts != null)
                {
                    return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<CitizenAttachmentResponseDTO>>(existingDistricts),
                        remarks = "Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
