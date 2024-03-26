using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgAppuntamentiDellaSettimanaDetailViewModel
    {
        public int? ImageId {get; set;}
        public string? ImagePath {get; set;}
        

        public static ImgAppuntamentiDellaSettimanaDetailViewModel FromDataRow (DataRow imgAppuntamentiDellaSettimanaDetailRow)
        {
            ImgAppuntamentiDellaSettimanaDetailViewModel imgForCarouselDetailViewModel = new ImgAppuntamentiDellaSettimanaDetailViewModel {
                ImageId = Convert.ToInt32(imgAppuntamentiDellaSettimanaDetailRow["IdFoto"]),
                ImagePath = string.Concat("/", imgAppuntamentiDellaSettimanaDetailRow["PathFoto"])
            };
            return imgForCarouselDetailViewModel;
        }
    }    
}