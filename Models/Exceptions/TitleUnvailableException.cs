using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class TitleUnavailableException: Exception
    {
        public TitleUnavailableException(string tipoEvento, string title, Exception innerException) : base ($"{tipoEvento} Titolo: '{title}' esiste giá", innerException)
        {
            
        }
    }
}