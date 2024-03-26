using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class ImgNotFoundException: Exception
    {
        public ImgNotFoundException(string tipoEvento, int id) : base ($"{tipoEvento} con Id: {id} non esiste.")
        {
            
        }
    }
}