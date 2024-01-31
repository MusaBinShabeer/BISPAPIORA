﻿using BISPAPIORA.Models.DTOS.ResponseDTO;
using System.IO.Pipelines;

namespace BISPAPIORA.Repositories.FileManagerServicesRepo
{
    public class FileManagerService : IFileManagerService
    {
        public async Task<ResponseModel> UploadFileAsync(byte[] file,string fileName, string extension)
        {
           

            // Define the folder where you want to save the uploaded files
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Generate a unique filename for the uploaded file
            string uniqueFileName = fileName+ extension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the uploaded file to the specified path
            File.WriteAllBytes(filePath, file);
            //using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, int.MaxValue, FileOptions.DeleteOnClose))
            //{
                
            //}
            return new ResponseModel()
            {
                remarks = "Success",
                success = true,
            };
        }
    }
}
