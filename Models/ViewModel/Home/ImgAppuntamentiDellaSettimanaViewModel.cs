using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgAppuntamentiDellaSettimanaViewModel
    {
        public int? IdFoto {get; set;}
        public string? PathFoto {get; set;}
        public string? ReviewerFoto {get; set;}

        public static ImgAppuntamentiDellaSettimanaViewModel FromDataRow(DataRow imgAppuntamentiDellaSettimanaRow)
        {
            ImgAppuntamentiDellaSettimanaViewModel imgAppuntamentiDellaSettimanaViewModel = new ImgAppuntamentiDellaSettimanaViewModel {
                IdFoto = Convert.ToInt32(imgAppuntamentiDellaSettimanaRow["IdFoto"]),
                PathFoto = (string)imgAppuntamentiDellaSettimanaRow["PathFoto"],
                ReviewerFoto = (imgAppuntamentiDellaSettimanaRow["ReviewerFoto"] ?? String.Empty).ToString()
            };
            return imgAppuntamentiDellaSettimanaViewModel;
        }
    }
}