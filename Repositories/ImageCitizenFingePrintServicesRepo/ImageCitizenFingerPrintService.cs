using AutoMapper;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DBModels.Dbtables;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using System.Reflection;
using System;
using System.Collections.Generic;

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

        //Add a new image citizen finger print:
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddImageCitizenFingerPrint(AddImageCitizenFingerPrintDTO model)
        {
            try
            {
                // Check if an image citizen finger print with the same CNIC already exists
                var imageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    .Where(x => x.cnic.ToLower().Equals(model.imageCitizenFingerPrintCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (imageCitizenFingerPrint == null)
                {
                    // Create a new image citizen finger print and save it to the database
                    var newImageCitizenFingerPrint = _mapper.Map<tbl_image_citizen_finger_print>(model);
                    db.tbl_image_citizen_finger_prints.Add(newImageCitizenFingerPrint);
                    await db.SaveChangesAsync();

                    // Return a success response model with the added image citizen finger print
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = true,
                        remarks = $"Image Citizen Finger Print {model.imageCitizenFingerPrintName} has been added successfully",
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(newImageCitizenFingerPrint),
                    };
                }
                else
                {
                    // Return a failure response model if an image citizen finger print with the same CNIC already exists
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Finger Print with CNIC {model.imageCitizenFingerPrintCnic} already exists"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        //Add a foreign key(citizen ID) to an existing image citizen finger print:
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> AddFkCitizentoImage(AddImageCitizenFingerPrintDTO model)
        {
            try
            {
                // Find the existing image citizen finger print based on the provided CNIC
                var imageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    .Where(x => x.cnic.ToLower().Equals(model.imageCitizenFingerPrintCnic.ToLower()))
                    .FirstOrDefaultAsync();

                if (imageCitizenFingerPrint != null)
                {
                    // Add the foreign key (citizen ID) to the existing image citizen finger print and save changes
                    imageCitizenFingerPrint.fk_citizen = Guid.Parse(model.fkCitizen);
                    await db.SaveChangesAsync();

                    // Return a success response model
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = true,
                        remarks = "Success",
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger print is found for the provided CNIC
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = $"Image Citizen Finger Print with CNIC {model.imageCitizenFingerPrintCnic} does not exist"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        //Delete an image citizen finger print:
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> DeleteImageCitizenFingerPrint(string imageCitizenFingerPrintId)
        {
            try
            {
                // Find the existing image citizen finger print based on the provided ID
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    .Where(x => x.id == Guid.Parse(imageCitizenFingerPrintId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenFingerPrint != null)
                {
                    // Remove the image citizen finger print from the database and save changes
                    db.tbl_image_citizen_finger_prints.Remove(existingImageCitizenFingerPrint);
                    await db.SaveChangesAsync();

                    // Return a success response model
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = "Image Citizen Finger Print Deleted",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger print is found for the provided ID
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = "No Record",
                        success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        //Get a list of image citizen finger prints:
        public async Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintsList()
        {
            try
            {
                // Retrieve all image citizen finger prints from the database
                var imageCitizenFingerPrints = await db.tbl_image_citizen_finger_prints.ToListAsync();

                if (imageCitizenFingerPrints.Count() > 0)
                {
                    // Return a success response model with the list of image citizen finger prints
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenFingerPrintResponseDTO>>(imageCitizenFingerPrints),
                        remarks = "Success",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no records are found
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        //Get an image citizen finger print by its ID:
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrint(string imageCitizenFingerPrintId)
        {
            try
            {
                // Find the existing image citizen finger print based on the provided ID
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    //.Include(x => x.tbl_citizen)
                    .Where(x => x.id == Guid.Parse(imageCitizenFingerPrintId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenFingerPrint != null)
                {
                    // Return a success response model with the found image citizen finger print
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        remarks = "Image Citizen Finger Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger print is found for the provided ID
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        //Get an image citizen finger print by citizen CNIC:
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> GetImageCitizenFingerPrintByCitizenCnic(string citizenCnic)
        {
            try
            {
                // Find the existing image citizen finger print based on the provided citizen CNIC
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    //.Include(x => x.tbl_citizen)
                    .Where(x => x.cnic == citizenCnic)
                    .FirstOrDefaultAsync();

                if (existingImageCitizenFingerPrint != null)
                {
                    // Return a success response model with the found image citizen finger print
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        remarks = "Image Citizen Finger Print found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger print is found for the provided citizen CNIC
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Updates an existing image citizen finger print record based on the provided model.
        public async Task<ResponseModel<ImageCitizenFingerPrintResponseDTO>> UpdateImageCitizenFingerPrint(UpdateImageCitizenFingerPrintDTO model)
        {
            try
            {
                // Find the existing image citizen finger print based on the provided ID
                var existingImageCitizenFingerPrint = await db.tbl_image_citizen_finger_prints
                    //.Include(x => x.tbl_citizen) // Uncomment this line if you want to include related citizen information
                    .Where(x => x.id == Guid.Parse(model.imageCitizenFingerPrintId))
                    .FirstOrDefaultAsync();

                if (existingImageCitizenFingerPrint != null)
                {
                    // Update the existing image citizen finger print with the new data
                    existingImageCitizenFingerPrint = _mapper.Map(model, existingImageCitizenFingerPrint);
                    await db.SaveChangesAsync();

                    // Return a success response model with the updated image citizen finger print
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        remarks = $"Image Citizen Finger Print: {model.imageCitizenFingerPrintName} has been updated",
                        data = _mapper.Map<ImageCitizenFingerPrintResponseDTO>(existingImageCitizenFingerPrint),
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger print is found for the provided ID
                    return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<ImageCitizenFingerPrintResponseDTO>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

        // Retrieves a list of image citizen finger prints based on the provided citizen ID.
        public async Task<ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>> GetImageCitizenFingerPrintByCitizenId(string citizenId)
        {
            try
            {
                // Retrieve a list of image citizen finger prints based on the provided citizen ID
                var existingImageCitizenFingerPrints = await db.tbl_image_citizen_finger_prints
                    //.Include(x => x.tbl_citizen) // Uncomment this line if you want to include related citizen information
                    .Where(x => x.fk_citizen == Guid.Parse(citizenId))
                    .ToListAsync();

                if (existingImageCitizenFingerPrints != null)
                {
                    // Return a success response model with the list of image citizen finger prints
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        data = _mapper.Map<List<ImageCitizenFingerPrintResponseDTO>>(existingImageCitizenFingerPrints),
                        remarks = "Image Citizen Finger Prints found successfully",
                        success = true,
                    };
                }
                else
                {
                    // Return a failure response model if no image citizen finger prints are found for the provided citizen ID
                    return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                    {
                        success = false,
                        remarks = "No Record"
                    };
                }
            }
            catch (Exception ex)
            {
                // Return a failure response model if an exception occurs during the process
                return new ResponseModel<List<ImageCitizenFingerPrintResponseDTO>>()
                {
                    success = false,
                    remarks = $"There Was Fatal Error {ex.Message.ToString()}"
                };
            }
        }

    }
}

