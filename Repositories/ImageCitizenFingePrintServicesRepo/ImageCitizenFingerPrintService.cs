using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;

namespace BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo
{
    public class ImageCitizenFingerPrintService : IImageCitizenFingerPrintService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ImageCitizenFingerPrintService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddImageCitizenFingerPrint(AddImageCitizenFingerPrintDTO model)
        {
            try
            {
                var imageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints.Where(x => x.cnic.ToLower().Equals(model.imageCitizenFingerPrintCnic.ToLower())).FirstOrDefaultAsync();
                if (imageCitizenFingerPrint == null)
                {
                    var newImageCitizenFingerPrint = new tbl_image_citizen_finger_print();
                    newImageCitizenFingerPrint = _mapper.Map<tbl_image_citizen_finger_print>(model);
                    db.tbl_image_citizen_finger_prints.Add(newImageCitizenFingerPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = true,
                        remarks = $"Image Citizen Finger Print {model.imageCitizenFingerPrintName} has been added successfully",
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(newImageCitizenFingerPrint),
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Finger Print with cnic {model.imageCitizenFingerPrintCnic} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddFkCitizentoImage(AddImageCitizenFingerPrintDTO model)
        {
            try
            {
                var imageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints.Where(x => x.cnic.ToLower().Equals(model.imageCitizenFingerPrintCnic.ToLower())).FirstOrDefaultAsync();
                if (imageCitizenFingerPrint != null)
                {
                    imageCitizenFingerPrint.fk_citizen = Guid.Parse(model.fkCitizen);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = true,
                        remarks = $"Succuss",
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Finger Print with cnic {model.imageCitizenFingerPrintCnic} does not exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> DeleteImageCitizenFingerPrint(string imageCitizenFingerPrintId)
        {
            try
            {
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints.Where(x => x.id == Guid.Parse(imageCitizenFingerPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenFingerPrint != null)
                {
                    db.tbl_image_citizen_finger_prints.Remove(existingImageCitizenFingerPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = "Image Citizen Finger Print Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintsList()
        {
            try
            {
                var imageCitizenFingerPrints = await db.tbl_image_citizen_finger_prints./*Include(x => x.tbl_citizen).*/ToListAsync();
                if (imageCitizenFingerPrints.Count() > 0)
                {
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenFingerPrintResponseDTO>>(imageCitizenFingerPrints),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrint(string imageCitizenFingerPrintId)
        {
            try
            {
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(imageCitizenFingerPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenFingerPrint != null)
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        remarks = "Image Citizen Finger Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrintByCitizenCnic(string citizenCnic)
        {
            try
            {
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.cnic == citizenCnic).FirstOrDefaultAsync();
                if (existingImageCitizenFingerPrint != null)
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        remarks = "Image Citizen Finger Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> UpdateImageCitizenFingerPrint(UpdateImageCitizenFingerPrintDTO model)
        {
            try
            {
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(model.imageCitizenFingerPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenFingerPrint != null)
                {
                    existingImageCitizenFingerPrint = _mapper.Map(model, existingImageCitizenFingerPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = $"Image Citizen Finger Print: {model.imageCitizenFingerPrintName} has been updated",
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintByCitizenId(string citizenId)
        {
            try
            {
                var existingImageCitizenFingerPrints = await db.tbl_image_citizen_finger_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingImageCitizenFingerPrints != null)
                {
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenFingerPrintResponseDTO>>(existingImageCitizenFingerPrints),
                        remarks = "Image Citizen Finger Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}
