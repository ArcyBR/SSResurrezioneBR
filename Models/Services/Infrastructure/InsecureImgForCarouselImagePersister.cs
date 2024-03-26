using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.Exceptions;
using System.IO;
using System.Runtime.InteropServices;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public class InsecureImgForCarouselImagePersister : IImgPersister
    {
        const int ERROR_SHARING_VIOLATION = 32;
        const int ERROR_LOCK_VIOLATION = 33;

        private readonly IWebHostEnvironment environment;
        public InsecureImgForCarouselImagePersister(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public async Task<string> SaveImageAsync (IFormFile formFile)
        {
            string fileName = formFile.FileName;
            var valid = MakeValidFileName(fileName);
            string path = string.Empty;
            try{
                if (fileName==valid){
                    path = $"img/IniziativeInCorso/{fileName}";
                    string phisicalPath = Path.Combine(environment.WebRootPath,"img/IniziativeInCorso", $"{fileName}");
                    // controlla che un'immagine con lo stesso nome non esista
                    if(File.Exists(phisicalPath))
                    {
                        File.Delete(phisicalPath);
                    }
                    using FileStream fileStream = File.OpenWrite(phisicalPath);
                    await formFile.CopyToAsync(fileStream);
                }else{
                    throw new ImgFileInsicureException(fileName);
                }
            } catch (IOException ex){
                if (IsFileLocked(ex))
                    throw new ImgFileExistInFolderException(fileName);
            }
            
            return path;
        }

        private static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION;
        }

        private static string MakeValidFileName(string name)
	    {
            var invalidFileNameChars = new string(Path.GetInvalidFileNameChars());
            string invalidChars = Regex.Escape(invalidFileNameChars);
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
	    }
    }
}