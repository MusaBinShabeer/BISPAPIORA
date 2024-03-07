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


        // Adds a new image citizen attachment.
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> AddImageCitizenAttachment(AddImageCitizenAttachmentDTO model)
        {
            try
            {
                // Check if attachment with the same CNIC exists
                var imageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.cnic.ToLower().Equals(model.imageCitizenAttachmentCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (imageCitizenAttachment == null)
                {
                    // Create a new attachment and add it to the database
                    var newImageCitizenAttachment = _mapper.Map<tbl_image_citizen_attachment>(model);
                    db.tbl_image_citizen_attachments.Add(newImageCitizenAttachment);
                    await db.SaveChangesAsync();

                    // Return success response with added attachment details
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = true,
                        remarks = $"Image Citizen Attachment {model.imageCitizenAttachmentName} added successfully",
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(newImageCitizenAttachment),
                    };
                }
                else
                {
                    // Return failure response if attachment with the same CNIC already exists
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Attachment with CNIC {model.imageCitizenAttachmentCnic} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Adds a foreign key to an existing image citizen attachment.
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> AddFkCitizenToAttachment(AddImageCitizenAttachmentDTO model)
        {
            try
            {
                // Check if the attachment with the given CNIC exists
                var imageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.cnic.ToLower().Equals(model.imageCitizenAttachmentCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (imageCitizenAttachment != null)
                {
                    // Add foreign key (citizen ID) to the existing attachment
                    imageCitizenAttachment.fk_citizen = Guid.Parse(model.fkCitizen);
                    await db.SaveChangesAsync();

                    // Return success response
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = true,
                        remarks = "Success",
                    };
                }
                else
                {
                    // Return failure response if the attachment does not exist
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Attachment with CNIC {model.imageCitizenAttachmentCnic} does not exist"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Deletes an image citizen attachment.
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> DeleteImageCitizenAttachment(string imageCitizenAttachmentId)
        {
            try
            {
                // Find existing image citizen attachment based on provided ID
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.id == Guid.Parse(imageCitizenAttachmentId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenAttachment != null)
                {
                    // Remove the image citizen attachment
                    db.tbl_image_citizen_attachments.Remove(existingImageCitizenAttachment);
                    await db.SaveChangesAsync();

                    // Return success response
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = "Image Citizen Attachment Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of all image citizen attachments.
        public async Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentsList()
        {
            try
            {
                // Retrieve all image citizen attachments
                var imageCitizenAttachments = await db.tbl_image_citizen_attachments
                    .ToListAsync();

                if (imageCitizenAttachments.Count() > 0)
                {
                    // Return success response with image citizen attachments
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenAttachmentResponseDTO>>(imageCitizenAttachments),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no records are found
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of an image citizen attachment by its ID.s>
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachment(string imageCitizenAttachmentId)
        {
            try
            {
                // Find the existing image citizen attachment based on the provided ID
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.id == Guid.Parse(imageCitizenAttachmentId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenAttachment != null)
                {
                    // Return success response with the image citizen attachment
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        remarks = "Image Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves details of an image citizen attachment by citizen's CNIC.
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> GetImageCitizenAttachmentByCitizenCnic(string citizenCnic)
        {
            try
            {
                // Find the existing image citizen attachment based on the provided CNIC
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.cnic == citizenCnic)
                    .FirstOrDefaultAsync();

                if (existingImageCitizenAttachment != null)
                {
                    // Return success response with the image citizen attachment
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        remarks = "Image Citizen Attachment found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Updates details of an image citizen attachment.
        public async Task<ResponseModel<ImageCitizenAttachmentResponseDTO>> UpdateImageCitizenAttachment(UpdateImageCitizenAttachmentDTO model)
        {
            try
            {
                // Find the existing image citizen attachment based on the provided ID
                var existingImageCitizenAttachment = await db.tbl_image_citizen_attachments
                    .Where(x => x.id == Guid.Parse(model.imageCitizenAttachmentId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenAttachment != null)
                {
                    // Update the image citizen attachment with new information
                    existingImageCitizenAttachment = _mapper.Map(model, existingImageCitizenAttachment);
                    await db.SaveChangesAsync();

                    // Return success response with the updated image citizen attachment
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        remarks = $"Image Citizen Attachment: {model.imageCitizenAttachmentName} has been updated",
                        data = _mapper.Map<ImageCitizenAttachmentResponseDTO>(existingImageCitizenAttachment),
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<ImageCitizenAttachmentResponseDTO>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of image citizen attachments associated with a citizen ID.
        public async Task<ResponseModel<List<ImageCitizenAttachmentResponseDTO>>> GetImageCitizenAttachmentByCitizenId(string citizenId)
        {
            try
            {
                // Find image citizen attachments associated with the provided citizen ID
                var existingImageCitizenAttachments = await db.tbl_image_citizen_attachments
                    .Where(x => x.fk_citizen == Guid.Parse(citizenId))
                    .ToListAsync();

                if (existingImageCitizenAttachments != null && existingImageCitizenAttachments.Any())
                {
                    // Return success response with the list of image citizen attachments
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenAttachmentResponseDTO>>(existingImageCitizenAttachments),
                        remarks = "Image Citizen Attachments found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return failure response if no record is found
                    return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return failure response if an exception occurs
                return new ResponseModel<List<ImageCitizenAttachmentResponseDTO>>()
                {
                    success = false,
                    remarks = $"Fatal Error: {ex.Message.ToString()}"
                };
            }
        }

    }
}
