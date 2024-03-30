using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DBModels.OraDbContextClass;
using BISPAPIORA.Models.DTOS.FileManagerDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.FileManagerServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace BISPAPIORA.Controllers
{
    [AppVersion]
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {

        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly IFileManagerService fileManagerService;
        private readonly IImageCitizenAttachmentService imageCitizenAttachmentService;
        private readonly IImageCitizenFingerPrintService imageCitizenFingerPrintService;
        private readonly Dbcontext db;

        public FileManagerController(IFileManagerService fileManagerService, IImageCitizenAttachmentService imageCitizenAttachmentService, IImageCitizenFingerPrintService imageCitizenFingerPrintService, Dbcontext db) 
        {
            this.fileManagerService = fileManagerService;
            this.imageCitizenAttachmentService = imageCitizenAttachmentService;
            this.imageCitizenFingerPrintService = imageCitizenFingerPrintService;
            this.db = db;
        }

        // Handles file upload for citizen attachments, processing multipart form data,
        // associating files with citizens identified by CNIC.
        [HttpPost("UploadAttachmentFile"), DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<ResponseModel<FileManagerResponseDTO>>> UploadAttachmentFile(string citizenCnic)
        {
            // Initialize variables to store file information
            string fileNameWithoutExtension = "";
            string fileExtension = "";
            #region Multipart
            // Check if the request is of type "multipart"
            if (!MultipartMiddleware.IsMultipartContentType(Request.ContentType))
            {
                // Return an UnsupportedMediaTypeResult if the request is not multipart
                return new UnsupportedMediaTypeResult();
            }

            // Accumulate the form data key-value pairs in the request (formAccumulator).
            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            // Read the boundary from the request and set up a MultipartReader
            var boundary = MultipartMiddleware.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            // Read the first section of the multipart content
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                // Check if the section has a content disposition header
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // Process file content if the section represents a file
                    if (MultipartMiddleware
                        .HasFileContentDisposition(contentDisposition))
                    {
                        untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        fileNameWithoutExtension = Path.GetFileNameWithoutExtension(untrustedFileNameForStorage);
                        fileExtension = Path.GetExtension(untrustedFileNameForStorage);
                        var stringArray = new string[1];
                        stringArray[0] = fileExtension;
                        streamedFileContent =
                            await FileAuthentication.ProcessStreamedFile(section, contentDisposition,
                                ModelState, stringArray, int.MaxValue);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                    }
                    // Process form data content if the section represents form data
                    else if (MultipartMiddleware
                        .HasFormDataContentDisposition(contentDisposition))
                    {
                        // Don't limit the key name length because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities
                            .RemoveQuotes(contentDisposition.Name).Value;
                        var encoding = MultipartMiddleware.GetEncoding(section);

                        if (encoding == null)
                        {
                            ModelState.AddModelError("File",
                                $"The request couldn't be processed (Error 2).");
                            // Log error

                            return BadRequest(ModelState);
                        }

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by 
                            // MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();

                            if (string.Equals(value, "undefined",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }

                            formAccumulator.Append(key, value);

                            if (formAccumulator.ValueCount >
                                _defaultFormOptions.ValueCountLimit)
                            {
                                // Form key count limit of 
                                // _defaultFormOptions.ValueCountLimit 
                                // is exceeded.
                                ModelState.AddModelError("File",
                                    $"The request couldn't be processed (Error 3).");
                                // Log error

                                return BadRequest(ModelState);
                            }
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                var image = new AddImageCitizenAttachmentDTO()
                {
                    imageCitizenAttachmentName = trustedFileNameForDisplay,  // Use the trusted file name for display
                    imageCitizenAttachmentData = streamedFileContent,
                    imageCitizenAttachmentContentType = section.ContentType,
                    imageCitizenAttachmentCnic = citizenCnic
                };
                var imageResponse = imageCitizenAttachmentService.AddImageCitizenAttachment(image);
                section = await reader.ReadNextSectionAsync();

            }
            return Ok(new ResponseModel<FileManagerResponseDTO>() { remarks = "Success", success = true });
            #endregion

            //// Bind form data to the model
            //var formData = new MultipartFormDataContent();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);
            //var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "",
            //    valueProvider: formValueProvider);
            //if (!bindingSuccessful)
            //{
            //    ModelState.AddModelError("File",
            //        "The request couldn't be processed (Error 5).");
            //    // Log error

            //    return BadRequest(ModelState);
            //}
            ////var response = await fileManagerService.UploadFileAsync(streamedFileContent, fileNameWithoutExtension,fileExtension);
        }

        // Handles the upload of thumbprint images for a citizen identified by CNIC
        [HttpPost("UploadThumbPrint"), DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<ResponseModel<FileManagerResponseDTO>>> UploadThumbPrint(string citizenCnic)
        {
            // Initialize variables to store file information
            string fileNameWithoutExtension = "";
            string fileExtension = "";

            // List to store thumbprint images
            var images = new List<AddImageCitizenFingerPrintDTO>();
            #region Multipart
            // Check if the request is of type "multipart"
            if (!MultipartMiddleware.IsMultipartContentType(Request.ContentType))
            {
                // Return an UnsupportedMediaTypeResult if the request is not multipart
                return new UnsupportedMediaTypeResult();
            }

            // Accumulate the form data key-value pairs in the request (formAccumulator).
            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            // Read the boundary from the request and set up a MultipartReader
            var boundary = MultipartMiddleware.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            // Read the first section of the multipart content
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                // Check if the section has a content disposition header
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // Process file content if the section represents a file
                    if (MultipartMiddleware
                        .HasFileContentDisposition(contentDisposition))
                    {
                        untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        fileNameWithoutExtension = Path.GetFileNameWithoutExtension(untrustedFileNameForStorage);
                        fileExtension = Path.GetExtension(untrustedFileNameForStorage);
                        var stringArray = new string[1];
                        stringArray[0] = fileExtension;
                        streamedFileContent =
                            await FileAuthentication.ProcessStreamedFile(section, contentDisposition,
                                ModelState, stringArray, int.MaxValue);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                    }
                    // Process form data content if the section represents form data
                    else if (MultipartMiddleware
                        .HasFormDataContentDisposition(contentDisposition))
                    {
                        // Don't limit the key name length because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities
                            .RemoveQuotes(contentDisposition.Name).Value;
                        var encoding = MultipartMiddleware.GetEncoding(section);

                        if (encoding == null)
                        {
                            ModelState.AddModelError("File",
                                $"The request couldn't be processed (Error 2).");
                            // Log error

                            return BadRequest(ModelState);
                        }

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by 
                            // MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();

                            if (string.Equals(value, "undefined",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }

                            formAccumulator.Append(key, value);

                            if (formAccumulator.ValueCount >
                                _defaultFormOptions.ValueCountLimit)
                            {
                                // Form key count limit of 
                                // _defaultFormOptions.ValueCountLimit 
                                // is exceeded.
                                ModelState.AddModelError("File",
                                    $"The request couldn't be processed (Error 3).");
                                // Log error

                                return BadRequest(ModelState);
                            }
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.

                // Create an image object for the thumbprint and add it to the list
                images.Add(new AddImageCitizenFingerPrintDTO()
                {
                    imageCitizenThumbPrintName = trustedFileNameForDisplay,  // Use the trusted file name for display
                    imageCitizenThumbPrintData = streamedFileContent,
                    imageCitizenThumbPrintContentType = section.ContentType,
                });

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            // Check if there is an existing thumbprint image for the citizen
            var existingImage = db.tbl_image_citizen_finger_prints.Where(x => x.cnic == citizenCnic).FirstOrDefault();

            // Update the existing thumbprint image if found
            if (existingImage != null)
            {
                existingImage.thumb_print_data = images[0].imageCitizenThumbPrintData;
                existingImage.thumb_print_content_type = images[0].imageCitizenThumbPrintContentType;
                existingImage.thumb_print_name = images[0].imageCitizenThumbPrintName;
                db.SaveChanges();
            }
            // Add a new thumbprint image if no existing image is found
            else
            {
                var image = new AddImageCitizenFingerPrintDTO()
                {
                    imageCitizenThumbPrintName = images[0].imageCitizenThumbPrintName,  // Use the trusted file name for display
                    imageCitizenThumbPrintData = images[0].imageCitizenThumbPrintData,
                    imageCitizenThumbPrintContentType = images[0].imageCitizenThumbPrintContentType,
                    imageCitizenFingerPrintCnic = citizenCnic
                };
                var imageResponse = imageCitizenFingerPrintService.AddImageCitizenFingerPrint(image);
            }

            // Return a success response
            return Ok(new ResponseModel<FileManagerResponseDTO>() { remarks = "Success", success = true });
            #endregion

            //// Bind form data to the model
            //var formData = new MultipartFormDataContent();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);
            //var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "",
            //    valueProvider: formValueProvider);
            //if (!bindingSuccessful)
            //{
            //    ModelState.AddModelError("File",
            //        "The request couldn't be processed (Error 5).");
            //    // Log error

            //    return BadRequest(ModelState);
            //}
            ////var response = await fileManagerService.UploadFileAsync(streamedFileContent, fileNameWithoutExtension,fileExtension);
        }

        // Handles the upload of fingerprint images for a citizen identified by CNIC
        [HttpPost("UploadFingerPrint"), DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<ResponseModel<FileManagerResponseDTO>>> UploadFingerPrint(string citizenCnic)
        {
            // Initialize variables to store file information
            string fileNameWithoutExtension = "";
            string fileExtension = "";
            var images = new List<AddImageCitizenFingerPrintDTO>();
            #region Multipart
            // Check if the request is of type "multipart"
            if (!MultipartMiddleware.IsMultipartContentType(Request.ContentType))
            {
                // Return an UnsupportedMediaTypeResult if the request is not multipart
                return new UnsupportedMediaTypeResult();
            }

            // Accumulate the form data key-value pairs in the request (formAccumulator).
            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            // Read the boundary from the request and set up a MultipartReader
            var boundary = MultipartMiddleware.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            // Read the first section of the multipart content
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                // Check if the section has a content disposition header
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // Process file content if the section represents a file
                    if (MultipartMiddleware
                        .HasFileContentDisposition(contentDisposition))
                    {
                        untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        fileNameWithoutExtension = Path.GetFileNameWithoutExtension(untrustedFileNameForStorage);
                        fileExtension = Path.GetExtension(untrustedFileNameForStorage);
                        var stringArray = new string[1];
                        stringArray[0] = fileExtension;
                        streamedFileContent =
                            await FileAuthentication.ProcessStreamedFile(section, contentDisposition,
                                ModelState, stringArray, int.MaxValue);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }
                    }
                    // Process form data content if the section represents form data
                    else if (MultipartMiddleware
                        .HasFormDataContentDisposition(contentDisposition))
                    {
                        // Don't limit the key name length because the 
                        // multipart headers length limit is already in effect.
                        var key = HeaderUtilities
                            .RemoveQuotes(contentDisposition.Name).Value;
                        var encoding = MultipartMiddleware.GetEncoding(section);

                        if (encoding == null)
                        {
                            ModelState.AddModelError("File",
                                $"The request couldn't be processed (Error 2).");
                            // Log error

                            return BadRequest(ModelState);
                        }

                        using (var streamReader = new StreamReader(
                            section.Body,
                            encoding,
                            detectEncodingFromByteOrderMarks: true,
                            bufferSize: 1024,
                            leaveOpen: true))
                        {
                            // The value length limit is enforced by 
                            // MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();

                            if (string.Equals(value, "undefined",
                                StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }

                            formAccumulator.Append(key, value);

                            if (formAccumulator.ValueCount >
                                _defaultFormOptions.ValueCountLimit)
                            {
                                // Form key count limit of 
                                // _defaultFormOptions.ValueCountLimit 
                                // is exceeded.
                                ModelState.AddModelError("File",
                                    $"The request couldn't be processed (Error 3).");
                                // Log error

                                return BadRequest(ModelState);
                            }
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.

                // Create an image object for the fingerprint and add it to the list
                images.Add(new AddImageCitizenFingerPrintDTO()
                {
                    imageCitizenFingerPrintName = trustedFileNameForDisplay,  // Use the trusted file name for display
                    imageCitizenFingerPrintData = streamedFileContent,
                    imageCitizenFingerPrintContentType = section.ContentType,
                });

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            // Check if there is an existing fingerprint image for the citizen
            var existingImage = db.tbl_image_citizen_finger_prints.Where(x => x.cnic == citizenCnic).FirstOrDefault();

            // Update the existing fingerprint image if found
            if (existingImage != null)
            {
                existingImage.finger_print_data = images[0].imageCitizenFingerPrintData;
                existingImage.finger_print_content_type = images[0].imageCitizenFingerPrintContentType;
                existingImage.finger_print_name = images[0].imageCitizenFingerPrintName;
                db.SaveChanges();
            }
            // Add a new fingerprint image if no existing image is found
            else
            {
                var image = new AddImageCitizenFingerPrintDTO()
                {
                    imageCitizenFingerPrintName = images[0].imageCitizenFingerPrintName,  // Use the trusted file name for display
                    imageCitizenFingerPrintData = images[0].imageCitizenFingerPrintData,
                    imageCitizenFingerPrintContentType = images[0].imageCitizenFingerPrintContentType,
                    imageCitizenFingerPrintCnic = citizenCnic
                };
                var imageResponse = imageCitizenFingerPrintService.AddImageCitizenFingerPrint(image);
            }

            // Return a success response
            return Ok(new ResponseModel<FileManagerResponseDTO>() { remarks = "Success", success = true });
            #endregion

            //// Bind form data to the model
            //var formData = new MultipartFormDataContent();
            //var formValueProvider = new FormValueProvider(
            //    BindingSource.Form,
            //    new FormCollection(formAccumulator.GetResults()),
            //    CultureInfo.CurrentCulture);
            //var bindingSuccessful = await TryUpdateModelAsync(formData, prefix: "",
            //    valueProvider: formValueProvider);
            //if (!bindingSuccessful)
            //{
            //    ModelState.AddModelError("File",
            //        "The request couldn't be processed (Error 5).");
            //    // Log error

            //    return BadRequest(ModelState);
            //}
            ////var response = await fileManagerService.UploadFileAsync(streamedFileContent, fileNameWithoutExtension,fileExtension);
        }

        // Downloads the citizen attachment image file by CNIC
        [HttpGet("DownloadImageCitizenAttachmentByCNIC")]
        public async Task<IActionResult> DownloadImageCitizenAttachmentByCNIC(string cnic)
        {
            try
            {
                // Get the image information for the citizen attachment by CNIC
                var imageCitizenAttachment = await imageCitizenAttachmentService.GetImageCitizenAttachmentByCitizenCnic(cnic);

                // Return the image data as a file content result
                return new FileContentResult(imageCitizenAttachment.data.imageCitizenAttachmentData, imageCitizenAttachment.data.imageCitizenAttachmentContentType)
                {
                    FileDownloadName = imageCitizenAttachment.data.imageCitizenAttachmentName
                };
            }
            catch (FileNotFoundException ex)
            {
                // Return a not found response if the image file is not found
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Return a server error response for other exceptions
                return StatusCode(500, ex.Message);
            }
        }

        // Downloads the citizen thumbprint image file by CNIC
        [HttpGet("DownloadImageCitizenThumbPrintByCNIC")]
        public async Task<IActionResult> DownloadImageCitizenThumbPrintByCNIC(string cnic)
        {
            try
            {
                // Get the image information for the citizen thumbprint by CNIC
                var imageCitizenThumbPrint = await imageCitizenFingerPrintService.GetImageCitizenFingerPrintByCitizenCnic(cnic);

                // Return the image data as a file content result
                return new FileContentResult(imageCitizenThumbPrint.data.imageCitizenThumbPrintData, imageCitizenThumbPrint.data.imageCitizenThumbPrintContentType)
                {
                    FileDownloadName = imageCitizenThumbPrint.data.imageCitizenThumbPrintName
                };
            }
            catch (FileNotFoundException ex)
            {
                // Return a not found response if the image file is not found
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Return a server error response for other exceptions
                return StatusCode(500, ex.Message);
            }
        }

        // Downloads the citizen fingerprint image file by CNIC
        [HttpGet("DownloadImageCitizenFingerPrintByCNIC")]
        public async Task<IActionResult> DownloadImageCitizenFingerPrintByCNIC(string cnic)
        {
            try
            {
                // Get the image information for the citizen fingerprint by CNIC
                var imageCitizenFingerPrint = await imageCitizenFingerPrintService.GetImageCitizenFingerPrintByCitizenCnic(cnic);

                // Return the image data as a file content result
                return new FileContentResult(imageCitizenFingerPrint.data.imageCitizenFingerPrintData, imageCitizenFingerPrint.data.imageCitizenFingerPrintContentType)
                {
                    FileDownloadName = imageCitizenFingerPrint.data.imageCitizenFingerPrintName
                };
            }
            catch (FileNotFoundException ex)
            {
                // Return a not found response if the image file is not found
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Return a server error response for other exceptions
                return StatusCode(500, ex.Message);
            }
        }

    }
}
