using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using Microsoft.AspNetCore.Mvc;

namespace BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo
{
    public class ImageCitizenAttachmentService : IImageCitizenAttachmentService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ImageCitizenAttachmentService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> AddImageCitizenAttachment(AddImageCitizenAttachmentDTO model)
        {
            try
            {
                var imageCitizenAttachment = await db.tbl_image_citizen_attachments.Where(x => x.name.ToLower().Equals(model.imageCitizenAttachmentName.ToLower())).FirstOrDefaultAsync();
                if (imageCitizenAttachment == null)
                {
                    var newImageCitizenAttachment = new tbl_image_citizen_attachment();
                    newImageCitizenAttachment = _mapper.Map<tbl_image_citizen_attachment>(model);
                    db.tbl_image_citizen_attachments.Add(newImageCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Image Citizen Attachment {model.imageCitizenAttachmentName} has been added successfully",
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(newImageCitizenAttachment),
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Attachment with name {model.imageCitizenAttachmentName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> DeleteImageCitizenAttachment(string imageCitizenAttachmentId)
        {
            try
            {
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments.Where(x => x.id == Guid.Parse(imageCitizenAttachmentId)).FirstOrDefaultAsync();
                if (existingImageCitizenAttachment != null)
                {
                    db.tbl_image_citizen_attachments.Remove(existingImageCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = "Image Citizen Attachment Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentsList()
        {
            try
            {
                var imageCitizenAttachments = await db.tbl_image_citizen_attachments./*Include(x => x.tbl_citizen).*/ToListAsync();
                if (imageCitizenAttachments.Count() > 0)
                {
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenAttachmentResponseDTO>>(imageCitizenAttachments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachment(string imageCitizenAttachmentId)
        {
            try
            {
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(imageCitizenAttachmentId)).FirstOrDefaultAsync();
                if (existingImageCitizenAttachment != null)
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        remarks = "Image Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachmentByCitizenCnic(string citizenCnic)
        {
            try
            {
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments/*.Include(x => x.tbl_citizen)*/.Where(x => x.cnic == citizenCnic).FirstOrDefaultAsync();
                if (existingImageCitizenAttachment != null)
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        remarks = "Image Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> UpdateImageCitizenAttachment(UpdateImageCitizenAttachmentDTO model)
        {
            try
            {
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(model.imageCitizenAttachmentId)).FirstOrDefaultAsync();
                if (existingImageCitizenAttachment != null)
                {
                    existingImageCitizenAttachment = _mapper.Map(model, existingImageCitizenAttachment);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = $"Image Citizen Attachment: {model.imageCitizenAttachmentName} has been updated",
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentByCitizenId(string citizenId)
        {
            try
            {
                var existingImageCitizenAttachments = await db.tbl_image_citizen_attachments/*.Include(x => x.tbl_citizen)*/.Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingImageCitizenAttachments != null)
                {
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenAttachmentResponseDTO>>(existingImageCitizenAttachments),
                        remarks = "Image Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
