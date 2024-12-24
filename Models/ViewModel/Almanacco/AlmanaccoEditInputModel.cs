using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;
using SSResurrezioneBR.Models.Entities;

namespace SSResurrezioneBR.Models.ViewModel.Almanacco
{
    public class AlmanaccoEditInputModel
    {
        [Required]
        public int AlmanaccoId {get; set;}
        [Required(ErrorMessage ="la descrizione é obbligatoria"),
        MinLength(10, ErrorMessage = "La descrizione deve contenere almeno {1} caratteri"),
        MaxLength(500, ErrorMessage = "la descrizione deve essere al massimo di {1} caratteri!"),
        Display(Name ="Descrizione dell'Evento da inserire nell'Almanacco")]
        public string? AlmanaccoDescrizione {get; set;}
        [Required(ErrorMessage ="titolo obbligatorio"),
        MinLength(5, ErrorMessage = "Il titolo deve contenere almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(AlmanaccoController.IsTitleAvailable), controller: "Almanacco", ErrorMessage = "Il titolo esiste già", AdditionalFields ="AlmanaccoId"),
        Display(Name = "Titolo dell'Evento da inserire nell'Almanacco")]
        public string? AlmanaccoTitolo {get; set;}
        [BindProperty, DataType(DataType.Date),
        Display(Name = "Data dell'Evento")]
        public DateTime AlmanaccoDataEvento {get;set;}
        public string? AlmanaccoEventCreator {get;set;}
        public string? AlmanaccoRowVersion { get; set; }

        /*
        public static AlmanaccoEditInputModel FromDataRow (DataRow almanaccoDetailRow)
        {
            AlmanaccoEditInputModel almanaccoEditInputModel = new AlmanaccoEditInputModel {
                AlmanaccoId = Convert.ToInt32(almanaccoDetailRow["Id_Almanacco"]),
                AlmanaccoDescrizione = (string) almanaccoDetailRow["Descrizione_Almanacco"],
                AlmanaccoTitolo = (string) almanaccoDetailRow["Titolo_Almanacco"],
                AlmanaccoDataEvento = DateOnly.Parse((string) almanaccoDetailRow["Data_Evento_Almanacco"]),
                AlmanaccoEventCreator = (string) almanaccoDetailRow["Creatore_Evento_Almanacco"]
            };
            return almanaccoEditInputModel;
        }
        */
        public static AlmanaccoEditInputModel FromEntity(Entities.Almanacco almanacco)
        {
            return new AlmanaccoEditInputModel
            {
                AlmanaccoId = Convert.ToInt32(almanacco.IdAlmanacco),
                AlmanaccoDescrizione = almanacco.DescrizioneAlmanacco,
                AlmanaccoTitolo = almanacco.TitoloAlmanacco,
                AlmanaccoDataEvento = almanacco.DataEventoAlmanacco,
                AlmanaccoEventCreator = almanacco.CreatoreEventoAlmanacco,
                AlmanaccoRowVersion = almanacco.RowVersion,
            };
        }
    }
}