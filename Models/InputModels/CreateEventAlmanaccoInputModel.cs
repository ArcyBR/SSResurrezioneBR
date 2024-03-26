using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;

namespace SSResurrezioneBR.Models.InputModels
{
    public class CreateEventAlmanaccoInputModel
    {
        [Required(ErrorMessage = "Il titolo dell'evento Almanacco é obbligatorio!"),
        MinLength(5, ErrorMessage="Il titolo deve essere di almeno {1} caratteri!"), 
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(AlmanaccoController.CreateEventoAlmanaccoIsTitleAvailable), controller: "Almanacco", ErrorMessage = "Il titolo esiste già")]
        public string? Titolo_Almanacco {get;set;}
        public string? Chiamante {get;set;}
    }
}