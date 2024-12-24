using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.Exceptions
{
    public class CoroPolifonicoMaterMisericordieEventoNotFoundException : Exception
    {
        public CoroPolifonicoMaterMisericordieEventoNotFoundException (string titoloEvento, long id) : base ($"{titoloEvento} con Id: {id} non esiste.")
        {
            
        }
    }
}