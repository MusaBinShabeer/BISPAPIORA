﻿using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ImageCitizenThumbPrintDTO;

namespace BISPAPIORA.Repositories.ImageCitizenThumbPrintServicesRepo
{
    public class ImageCitizenThumbPrintService : IImageCitizenThumbPrintService
    {
        private readonly IMapper _mapper;
        private readonly Dbcontext db;
        public ImageCitizenThumbPrintService(IMapper mapper, Dbcontext db)
        {
            _mapper = mapper;
            this.db = db;
        }
        public async Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> AddImageCitizenThumbPrint(AddImageCitizenThumbPrintDTO model)
        {
            try
            {
                var imageCitizenThumbPrint = await db.tbl_image_citizen_thumb_prints.Where(x => x.name.ToLower().Equals(model.imageCitizenThumbPrintName.ToLower())).FirstOrDefaultAsync();
                if (imageCitizenThumbPrint == null)
                {
                    var newImageCitizenThumbPrint = new tbl_image_citizen_thumb_print();
                    newImageCitizenThumbPrint = _mapper.Map<tbl_image_citizen_thumb_print>(model);
                    db.tbl_image_citizen_thumb_prints.Add(newImageCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        success = true,
                        remarks = $"Image Citizen Thumb Print {model.imageCitizenThumbPrintName} has been added successfully",
                        data = _mapper.Map<ImageCitizenThumbPrintResponseDTO>(newImageCitizenThumbPrint),
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Thumb Print with name {model.imageCitizenThumbPrintName} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> DeleteImageCitizenThumbPrint(string imageCitizenThumbPrintId)
        {
            try
            {
                var existingImageCitizenThumbPrint = await db.tbl_image_citizen_thumb_prints.Where(x => x.id == Guid.Parse(imageCitizenThumbPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenThumbPrint != null)
                {
                    db.tbl_image_citizen_thumb_prints.Remove(existingImageCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        remarks = "Image Citizen Thumb Print Deleted",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>> GetImageCitizenThumbPrintsList()
        {
            try
            {
                var imageCitizenThumbPrints = await db.tbl_image_citizen_thumb_prints./*Include(x => x.tbl_citizen).*/ToListAsync();
                if (imageCitizenThumbPrints.Count() > 0)
                {
                    return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenThumbPrintResponseDTO>>(imageCitizenThumbPrints),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> GetImageCitizenThumbPrint(string imageCitizenThumbPrintId)
        {
            try
            {
                var existingImageCitizenThumbPrint = await db.tbl_image_citizen_thumb_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(imageCitizenThumbPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenThumbPrint != null)
                {
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenThumbPrintResponseDTO>(existingImageCitizenThumbPrint),
                        remarks = "Image Citizen Thumb Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<ImageCitizenThumbPrintResponseDTO>> UpdateImageCitizenThumbPrint(UpdateImageCitizenThumbPrintDTO model)
        {
            try
            {
                var existingImageCitizenThumbPrint = await db.tbl_image_citizen_thumb_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.id == Guid.Parse(model.imageCitizenThumbPrintId)).FirstOrDefaultAsync();
                if (existingImageCitizenThumbPrint != null)
                {
                    existingImageCitizenThumbPrint = _mapper.Map(model, existingImageCitizenThumbPrint);
                    await db.SaveChangesAsync();
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        remarks = $"Image Citizen Thumb Print: {model.imageCitizenThumbPrintName} has been updated",
                        data = _mapper.Map<ImageCitizenThumbPrintResponseDTO>(existingImageCitizenThumbPrint),
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<ImageCitizenThumbPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
        public async Task<ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>> GetImageCitizenThumbPrintByCitizenId(string citizenId)
        {
            try
            {
                var existingImageCitizenThumbPrints = await db.tbl_image_citizen_thumb_prints/*.Include(x => x.tbl_citizen)*/.Where(x => x.fk_citizen == Guid.Parse(citizenId)).ToListAsync();
                if (existingImageCitizenThumbPrints != null)
                {
                    return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenThumbPrintResponseDTO>>(existingImageCitizenThumbPrints),
                        remarks = "Image Citizen Thumb Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<ImageCitizenThumbPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }
    }
}