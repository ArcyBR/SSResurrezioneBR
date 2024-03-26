using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Models.Services.Application.ImgForCarousel;
using SSResurrezioneBR.Models.ViewModel.Home;
 
namespace SSResurrezioneBR.Controllers
{
    public class ImgForCarouselController : Controller
    {
        private readonly IImgForCarouselService _imgForCarousel;

        public ImgForCarouselController(IImgForCarouselService imgForCarousel)
        {
            this._imgForCarousel = imgForCarousel;
        }
        // Controller di pagina di dettaglio delle attivitá
        public async Task<IActionResult> Index(int id)
        {           
            ImgForCarouselDetailViewModel imgForCarouselDetailViewModel = new ();
            imgForCarouselDetailViewModel = await _imgForCarousel.GetImgForCarouselDetailAsync(id);
            ViewData["Title"] = imgForCarouselDetailViewModel.ImageLabel;
            return View(imgForCarouselDetailViewModel);
        }
    }
}
