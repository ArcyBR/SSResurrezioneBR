using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;

namespace SSResurrezioneBR.Models.InputModels
{
    public class CreateEventCoroPolifonicoMaterMisericordieInputModel
    {
        public string? Chiamante {get;set;}
        [Required(ErrorMessage = "Il titolo dell'evento Coro é obbligatorio!"),
        MinLength(5, ErrorMessage="Il titolo deve essere di almeno {1} caratteri!"), 
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(CoroPolifonicoMaterMisericordieController.CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailable), controller: "CoroPolifonicoMaterMisericordie", ErrorMessage = "Il titolo esiste già")]
        public string? Titolo_CoroPolifonicoMaterMisericordie {get;set;}
    }
}