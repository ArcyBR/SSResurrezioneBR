using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Incarichi
{
    public class IncaricoDetailViewModel
    {
        public string? Cognome {get; set;}
        public string? Titolo { get; set; }
        public string? Nome { get; set; }
        public string? Incarico { get; set; }
        public string? LinkDiocesi { get; set; }
        public string? Email { get; set; }
        public string? TelefonoFisso { get; set; }
        public string? TelefonoCellulare { get; set; }
        public string? Indirizzo { get; set; }
        public string? NumeroCivico { get; set; }
        public string? CAP { get; set; }
        public string? Citta { get; set; }

    }
}