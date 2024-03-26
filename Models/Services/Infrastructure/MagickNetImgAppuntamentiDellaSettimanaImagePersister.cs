using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.Exceptions;
using System.IO;
using System.Runtime.InteropServices;
using ImageMagick;
using System.Drawing;
using SSResurrezioneBR.Models.Exceptions.Infrastructure;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public class MagickNetImgAppuntamentiDellaSettimanaImagePersister : IImgPersisterAS
    {
        const int ERROR_SHARING_VIOLATION = 32;
        const int ERROR_LOCK_VIOLATION = 33;
        private readonly IWebHostEnvironment environment;
        private readonly SemaphoreSlim semaphoreSlim;
        private readonly IConfiguration _configuration;

        public MagickNetImgAppuntamentiDellaSettimanaImagePersister(IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.environment = environment;
            ResourceLimits.Width = 4000;
            ResourceLimits.Height = 4000;
            semaphoreSlim = new SemaphoreSlim(2);
            _configuration = configuration;
        }
        public async Task<string> SaveImageAsync(IFormFile formFile)
        {
            await semaphoreSlim.WaitAsync();
            // salvare il file
            string fileName = formFile.FileName;
            var valid = MakeValidFileName(fileName);
            string path = string.Empty;
            try{
                if (fileName==valid){
                    path = $"img/AppuntamentiDellaSettimana/{fileName}";
                    string phisicalPath = Path.Combine(environment.WebRootPath,"img/AppuntamentiDellaSettimana", $"{fileName}");
                    // controlla che un'immagine con lo stesso nome non esista
                    if(File.Exists(phisicalPath))
                    {
                        File.Delete(phisicalPath);
                    }
                    using Stream inputStream = formFile.OpenReadStream();
                    using MagickImage image = new MagickImage(inputStream);

                    // manipolare l'immagine
                    int width = _configuration.GetValue<int>("ImgAppuntamentiDellaSettimanaImage:width");
                    int height = _configuration.GetValue<int>("ImgAppuntamentiDellaSettimanaImage:height");
                    MagickGeometry resizeGeometry = new MagickGeometry(width, height){
                        FillArea = true
                    };
                    image.Resize(resizeGeometry);
                    image.Crop(width, height, Gravity.Northwest);
                    image.Quality = 75;
                    image.Write(phisicalPath, MagickFormat.Jpeg);
                }else{
                    throw new ImgFileInsicureException(fileName);
                }
            } catch (IOException ex){
                if (IsFileLocked(ex))
                    throw new ImgFileExistInFolderException(fileName);
            }
            catch (Exception ex){
                throw new ImagePersistenceException(ex);
            }
            finally
            {
                semaphoreSlim.Release();
            }
            // restituisce il percorso al file
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