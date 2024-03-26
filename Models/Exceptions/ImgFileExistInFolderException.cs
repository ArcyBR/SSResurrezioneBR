using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class ImgFileExistInFolderException: Exception
    {
        public ImgFileExistInFolderException(string fileName) : base ($"Errore durante il caricamento del file: {fileName}. Riprovare da un'altra cartella.")
        {
            
        }
    }
}