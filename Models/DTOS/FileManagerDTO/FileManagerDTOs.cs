﻿namespace BISPAPIORA.Models.DTOS.FileManagerDTO
{
    public class FileManagerDTOs
    {
        public string filePath { get; set; }= string.Empty;
        public string fileName { get; set; } = string.Empty;
    }
    public class FileManagerResponseDTO: FileManagerDTOs { }
   
}
