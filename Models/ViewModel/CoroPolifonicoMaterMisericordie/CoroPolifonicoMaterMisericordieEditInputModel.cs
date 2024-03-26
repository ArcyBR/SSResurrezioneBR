using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;
using SSResurrezioneBR.Models.Entities;

namespace SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie
{
    public class CoroPolifonicoMaterMisericordieEditInputModel
    {
        [Required]
        public int? CoroPolifonicoMaterMisericordieId {get; set;}
        [Required(ErrorMessage ="la descrizione é obbligatoria"),
        MinLength(10, ErrorMessage = "La descrizione deve contenere almeno {1} caratteri"),
        MaxLength(500, ErrorMessage = "la descrizione deve essere al massimo di {1} caratteri!"),
        Display(Name ="Descrizione dell'Evento del Coro Polifonico Mater Misericordie")]
        public string? CoroPolifonicoMaterMisericordieDescrizione {get; set;}
        [Required(ErrorMessage ="titolo obbligatorio"),
        MinLength(5, ErrorMessage = "Il titolo deve contenere almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(CoroPolifonicoMaterMisericordieController.IsTitleAvailable), controller: "CoroPolifonicoMaterMisericordie", ErrorMessage = "Il titolo esiste già", AdditionalFields ="CoroPolifonicoMaterMisericordieId"),
        Display(Name = "Titolo dell'Evento da inserire nell'Almanacco")]
        public string? CoroPolifonicoMaterMisericordieTitolo {get; set;}
        [BindProperty, DataType(DataType.Date),
        Display(Name = "Data dell'Evento")]
        public DateTime CoroPolifonicoMaterMisericordieDataEvento {get;set;}
        public string? CoroPolifonicoMaterMisericordieCreatoreEvento {get;set;}
        public string? CoroPolifonicoMaterMisericordieRowVersion { get; set; }
        public int? CoroPolifonicoMaterMisericordieIsVisible { get; set; }

        public static CoroPolifonicoMaterMisericordieEditInputModel FromEntity(Entities.CoroPolifonicoMaterMisericordie coroPolifonicoMaterMisericordie)
        {
            return new CoroPolifonicoMaterMisericordieEditInputModel
            {
                CoroPolifonicoMaterMisericordieId = Convert.ToInt32(coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie),
                CoroPolifonicoMaterMisericordieDescrizione = coroPolifonicoMaterMisericordie.DescrizioneEventoCoroPolifonicoMaterMisericordie,
                CoroPolifonicoMaterMisericordieTitolo = coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie,
                CoroPolifonicoMaterMisericordieDataEvento = coroPolifonicoMaterMisericordie.DataEventoCoroPolifonicoMaterMisericordie,
                CoroPolifonicoMaterMisericordieCreatoreEvento = coroPolifonicoMaterMisericordie.CreatoreEventoCoroPolifonicoMaterMisericordie,
                CoroPolifonicoMaterMisericordieRowVersion = coroPolifonicoMaterMisericordie.RowVersion,
                CoroPolifonicoMaterMisericordieIsVisible = coroPolifonicoMaterMisericordie.ImageVisibileCoroPolifonicoMaterMisericordie
            };
        }
    }
}