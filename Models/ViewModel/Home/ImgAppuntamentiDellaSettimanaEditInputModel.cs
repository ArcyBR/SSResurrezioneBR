using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Controllers;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgAppuntamentiDellaSettimanaEditInputModel
    {
        [Required]
        public int? IdFoto {get; set;}
        [Display(Name = "Immagine rappresentativa")]
        public string? PathFoto {get; set;}
        public string? ReviewerFoto { get; set; }

        public static ImgAppuntamentiDellaSettimanaEditInputModel FromDataRow(DataRow imgAppuntamentiDellaSettimanaRow)
        {
            ImgAppuntamentiDellaSettimanaEditInputModel imgAppuntamentiDellaSettimanaEditInputModel = new ImgAppuntamentiDellaSettimanaEditInputModel {
                IdFoto = Convert.ToInt32(imgAppuntamentiDellaSettimanaRow["IdFoto"]),
                PathFoto = (string) imgAppuntamentiDellaSettimanaRow["PathFoto"],
                ReviewerFoto = (imgAppuntamentiDellaSettimanaRow["reviewerFoto"] ?? String.Empty).ToString(),
                RowVersion = (string) imgAppuntamentiDellaSettimanaRow["RowVersion"]
            };
            return imgAppuntamentiDellaSettimanaEditInputModel;
        }
        [Display(Name = "Nuova Immagine...")]
        public IFormFile? Image {get; set;}
        public string RowVersion { get; set; }
    }
}