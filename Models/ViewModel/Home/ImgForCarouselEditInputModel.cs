using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgForCarouselEditInputModel
    {
        [Required]
        public int? ImageId {get; set;}
        [Display(Name = "Immagine rappresentativa")]
        public string? ImagePath {get; set;}
        
        [Required(ErrorMessage ="titolo obbligatorio"),
        MinLength(5, ErrorMessage = "Il titolo deve contenere almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il titolo deve essere al massimo di {1} caratteri!"),
        Remote(action: nameof(HomeController.IsTitleAvailable), controller: "Home", ErrorMessage = "Il titolo esiste già", AdditionalFields ="ImageId"),
        Display(Name = "Titolo dell'Attivitá")]
        public string? ImageContentTitle {get; set;}
        /*
        [Required(ErrorMessage ="Indicare se l'attivitá sará visualizzata oppure no"),
        RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Il campo Visibilitá accetta solamente 1 o 0"),
        MaxLength(1, ErrorMessage ="Il campo Visibilitá accetta solamente 1 o 0"),
        Display(Name ="Visibilitá")]
        public int? ImageVisible {get; set;}
        */
        [Required(ErrorMessage ="la descrizione é obbligatoria"),
        MinLength(10, ErrorMessage = "La descrizione deve contenere almeno {1} caratteri"),
        MaxLength(500, ErrorMessage = "la descrizione deve essere al massimo di {1} caratteri!"),
        Display(Name ="Descrizione dell'Attivitá")]
        public string? ImageContentDescription {get; set;}
        public string? ImageEventCreator {get; set;}
        [Required(ErrorMessage ="Sottotitolo obbligatorio"),
        MinLength(5, ErrorMessage = "Il Sottotitolo deve contenere almeno {1} caratteri"),
        MaxLength(50, ErrorMessage = "Il Sottotitolo deve essere al massimo di {1} caratteri!"),
        Display(Name = "Sottotitolo dell'Attivitá")]
        public string? ImageLabel{get;set;}

        public static ImgForCarouselEditInputModel FromDataRow(DataRow imgForCarouselRow)
        {
            ImgForCarouselEditInputModel imgForCarouselEditInputModel = new ImgForCarouselEditInputModel {
                ImageId = Convert.ToInt32(imgForCarouselRow["Image_Id"]),
                ImagePath = (string)imgForCarouselRow["Image_Path"],
                ImageLabel = (string) imgForCarouselRow["Image_Label"],
                ImageContentTitle = (string) imgForCarouselRow["Image_Content_Title"],
                //ImageVisible = Convert.ToInt16(imgForCarouselRow["Image_Visible"]),
                ImageContentDescription = (string) imgForCarouselRow["Image_Content_Description"],
                ImageEventCreator = (imgForCarouselRow["Image_Event_Creator"] ?? String.Empty).ToString(),
                RowVersion = (string)imgForCarouselRow["RowVersion"]
            };
            return imgForCarouselEditInputModel;
        }
        [Display(Name = "Nuova Immagine...")]
        public IFormFile? Image {get; set;}
        public string RowVersion { get; set; }
    }
}