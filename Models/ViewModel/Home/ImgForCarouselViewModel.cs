using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgForCarouselViewModel
    {
        public int? ImageId {get; set;}
        public string? ImagePath {get; set;}
        public string? ImageLabel {get; set;}
        public string? ImageContentTitle {get; set;}
        public int? ImageVisible {get; set;}
        public string? ImageEventCreator {get; set;}

        public static ImgForCarouselViewModel FromDataRow(DataRow imgForCarouselRow)
        {
            ImgForCarouselViewModel imgForCarouselViewModel = new ImgForCarouselViewModel {
                ImageId = Convert.ToInt32(imgForCarouselRow["Image_Id"]),
                ImagePath = (string)imgForCarouselRow["Image_Path"],
                ImageLabel = (string) imgForCarouselRow["Image_Label"],
                ImageContentTitle = (string) imgForCarouselRow["Image_Content_Title"],
                ImageVisible = Convert.ToInt16(imgForCarouselRow["Image_Visible"]),
                ImageEventCreator = (imgForCarouselRow["Image_Event_Creator"] ?? String.Empty).ToString()
            };
            return imgForCarouselViewModel;
        }
    }
}