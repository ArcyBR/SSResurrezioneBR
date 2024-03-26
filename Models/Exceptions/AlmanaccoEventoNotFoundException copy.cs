using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class AlmanaccoEventoNotFoundException : Exception
    {
        public AlmanaccoEventoNotFoundException (string titoloEvento, int id) : base ($"{titoloEvento} con Id: {id} non esiste.")
        {
            
        }
    }
}