using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;

namespace SSResurrezioneBR.Models.InputModels
{
    public class CreateInitiativeInputModel
    { 
        [Required(ErrorMessage = "Il titolo dell'iniziativa é obbligatorio!"),
        MinLength(5, ErrorMessage="Il titolo deve essere di almeno {1} caratteri!"), 
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(HomeController.CreateInitiativeIsTitleAvailable), controller: "Home", ErrorMessage = "Il titolo esiste già")]
        public string? Image_Content_Title {get;set;}
    }
}