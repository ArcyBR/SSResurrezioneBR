using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Home
{
    public class ImgForCarouselDetailViewModel
    {
        public int? ImageId {get; set;}
        public string? ImagePath {get; set;}
        public string? ImageContentTitle {get; set;}
        public int? ImageVisible {get; set;}
        public string? ImageContentDescription {get; set;}
        public string? ImageEventCreator {get; set;}
        public string? ImageLabel {get;set;}

        public static ImgForCarouselDetailViewModel FromDataRow (DataRow imgForCarouselDetailRow)
        {
            ImgForCarouselDetailViewModel imgForCarouselDetailViewModel = new ImgForCarouselDetailViewModel {
                ImageId = Convert.ToInt32(imgForCarouselDetailRow["Image_Id"]),
                ImagePath = string.Concat("/",imgForCarouselDetailRow["Image_Path"]),
                ImageContentTitle = (string) imgForCarouselDetailRow["Image_Content_Title"],
                ImageVisible = Convert.ToInt32(imgForCarouselDetailRow["Image_Visible"]),
                ImageContentDescription = (string) imgForCarouselDetailRow["Image_Content_Description"],
                ImageEventCreator = (imgForCarouselDetailRow["Image_Event_Creator"] ?? String.Empty).ToString(),
                ImageLabel = (string) imgForCarouselDetailRow["Image_Label"]
            };
            return imgForCarouselDetailViewModel;
        }
    }    
}