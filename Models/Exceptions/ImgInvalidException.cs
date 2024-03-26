using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class ImgInvalidException : Exception
    {
        public ImgInvalidException(IFormFile image, Exception ex) : base($"{image.Name}: {ex}.")
        {

        }
    }
}