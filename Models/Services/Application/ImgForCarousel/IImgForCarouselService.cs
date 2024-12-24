using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.ImgForCarousel
{
    public interface IImgForCarouselService
    {
        Task<ImgForCarouselDetailViewModel> CreateInitiativeAsync(CreateInitiativeInputModel inputModel);
        Task<List<ImgForCarouselViewModel>> GetImgForCarouselAsync();
        Task<ImgForCarouselDetailViewModel> GetImgForCarouselDetailAsync(long id);
        Task<bool>IsTitleAvailableAsync(string title,long id);
        Task<bool>CreateInitiativeIsTitleAvailableAsync(string title);
        Task<ImgForCarouselEditInputModel> GetImgForCarouselForEditingAsync(long id);
        Task<ImgForCarouselDetailViewModel> EditImgForCarouselAsync(ImgForCarouselEditInputModel inputModel);
        Task DeleteInitiativeAsync (long id);
    }
}