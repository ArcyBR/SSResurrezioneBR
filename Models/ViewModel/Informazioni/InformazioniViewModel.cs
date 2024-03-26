using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Informazioni
{
    public class InformazioniViewModel
    {
        public string? DenominazioneUfficiale { get; set; }    
        public string? DenominazioneAltra { get; set; }    
        public string? Tipo { get; set; }    
        public string? Telefono { get; set; }    
        public string? Email { get; set; }    
        public string? Indirizzo { get; set; }    
        public string? Facebook { get; set; }    
        public string? Instagram { get; set; }    

        public InformazioniViewModel()
        {        
            DenominazioneUfficiale = "SS. Resurrezione";
            DenominazioneAltra = "Cappuccini";
            Tipo="Parrocchia";
            Telefono ="0831586093";
            Email ="resurrezionebrindisi@libero.it";
            Indirizzo="via Monte Nero, Brindisi, Puglia - Italia";
            Facebook="https://www.facebook.com/parrocchiassresurrezionebrindisi/";
            Instagram="https://www.instagram.com/Parrocchia_resurrezione/";
        }
    }
}