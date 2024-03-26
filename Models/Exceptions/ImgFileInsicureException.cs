using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class ImgFileInsicureException: Exception
    {
        public ImgFileInsicureException(string fileName) : base ($"{fileName} contiene informazioni potenzialmente dannose.")
        {
            
        }
    }
}