using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //Get Folder Path
            //string folderPath = "E:\\Projects c#\\Demo Solution\\Demo.PL\\wwwroot\\" + FolerName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",folderName);
            //make folder unique
            string fileName =$"{Guid.NewGuid()}{file.FileName}";
            //get file path
            string filePath=Path.Combine(folderPath, fileName);

            //save file
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        }

        public static void Delete(string fileName,string folderName)
        
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files", folderName,fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
