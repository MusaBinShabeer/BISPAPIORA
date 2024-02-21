using BISPAPIORA.Extensions.Middleware;
using BISPAPIORA.Models.DTOS.CitizenThumbPrintDTO;
using BISPAPIORA.Models.DTOS.FileManagerDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenAttachmentDTO;
using BISPAPIORA.Models.DTOS.ImageCitizenFingerPrintDTO;
using BISPAPIORA.Models.DTOS.ResponseDTO;
using BISPAPIORA.Repositories.CitizenThumbPrintServicesRepo;
using BISPAPIORA.Repositories.FileManagerServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenAttachmentServicesRepo;
using BISPAPIORA.Repositories.ImageCitizenFingePrintServicesRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Net;

namespace BISPAPIORA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileManagerController : ControllerBase
    {

        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly IFileManagerService fileManagerService;
        private readonly IImageCitizenAttachmentService imageCitizenAttachmentService;
        private readonly IImageCitizenFingerPrintService imageCitizenThumbPrintService;

        public FileManagerController(IFileManagerService fileManagerService, IImageCitizenAttachmentService imageCitizenAttachmentService, IImageCitizenFingerPrintService citizenThumbPrintService) 
        {
            this.fileManagerService = fileManagerService;
            this.imageCitizenAttachmentService = imageCitizenAttachmentService;
            this.imageCitizenThumbPrintService = citizenThumbPrintService;
        }
        [HttpPost("UploadAttachmentFile"), DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<ResponseModel<FileManagerResponseDTO>>> UploadAttachmentFile(string citizenCnic)
        {
            string fileNameWithoutExtension = "";
            string fileExtension = "";
            #region Multipart
            if (!MultipartMiddleware.IsMultipartContentType(Request.ContentType))
            {
                return new UnsupportedMediaTypeResult();
            }

            // Accumulate the form data key-value pairs in the request (formAccumulator).
            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            var boundary = MultipartMiddleware.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
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
        [HttpPost("UploadFingerPrint"), DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<ActionResult<ResponseModel<FileManagerResponseDTO>>> UploadFingerPrint(string citizenCnic)
        {
            string fileNameWithoutExtension = "";
            string fileExtension = "";
            var images = new List<AddImageCitizenFingerPrintDTO>();
            #region Multipart
            if (!MultipartMiddleware.IsMultipartContentType(Request.ContentType))
            {
                return new UnsupportedMediaTypeResult();
            }

            // Accumulate the form data key-value pairs in the request (formAccumulator).
            var formAccumulator = new KeyValueAccumulator();
            var trustedFileNameForDisplay = string.Empty;
            var untrustedFileNameForStorage = string.Empty;
            var streamedFileContent = Array.Empty<byte>();

            var boundary = MultipartMiddleware.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
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
            var image = new AddImageCitizenFingerPrintDTO()
            {
                imageCitizenThumbPrintName = images[0].imageCitizenThumbPrintName,  // Use the trusted file name for display
                imageCitizenThumbPrintData = images[0].imageCitizenThumbPrintData,
                imageCitizenThumbPrintContentType = images[0].imageCitizenThumbPrintContentType,
                imageCitizenFingerPrintData = images[1].imageCitizenThumbPrintData,
                imageCitizenFingerPrintName = images[1].imageCitizenThumbPrintName,
                imageCitizenFingerPrintContentType = images[1].imageCitizenThumbPrintContentType,
                imageCitizenFingerPrintCnic = citizenCnic
            };           
            var imageResponse = imageCitizenThumbPrintService.AddImageCitizenFingerPrint(image);
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
        [HttpGet("DownloadImageCitizenAttachmentByCNIC")]
        public async Task<IActionResult> DownloadImageCitizenAttachmentByCNIC(string cnic)
        {
            try
            {
                var imageCitizenAttachment = await imageCitizenAttachmentService.GetImageCitizenAttachmentByCitizenCnic(cnic);
                return new FileContentResult(imageCitizenAttachment.data.imageCitizenAttachmentData, imageCitizenAttachment.data.imageCitizenAttachmentContentType)
                {
                    FileDownloadName = imageCitizenAttachment.data.imageCitizenAttachmentName
                };
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("DownloadImageCitizenThumbPrintByCNIC")]
        public async Task<IActionResult> DownloadImageCitizenThumbPrintByCNIC(string cnic)
        {
            try
            {
                var imageCitizenThumbPrint = await imageCitizenThumbPrintService.GetImageCitizenFingerPrintByCitizenCnic(cnic);
                return new FileContentResult(imageCitizenThumbPrint.data.imageCitizenThumbPrintData, imageCitizenThumbPrint.data.imageCitizenThumbPrintContentType)
                {
                    FileDownloadName = imageCitizenThumbPrint.data.imageCitizenThumbPrintName
                };
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
