using BISPAPIORA.Models.DTOS.ResponseDTO;

namespace BISPAPIORA.Repositories.FileManagerServicesRepo
{
    public class FileManagerService
    {
        public async Task<ResponseModel> UploadFileAsync(byte[] file)
        {
           

            // Define the folder where you want to save the uploaded files
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Generate a unique filename for the uploaded file
            string uniqueFileName = Path.GetRandomFileName();
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Save the uploaded file to the specified path
            File.WriteAllBytes(filePath, file);
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, int.MaxValue, FileOptions.DeleteOnClose))
            {
                  
            }
            return new ResponseModel()
            {
                remarks = "Success",
                success = true,
            };
        }
    }
}
