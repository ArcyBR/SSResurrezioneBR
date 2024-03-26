using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.ImgForCarousel
{
    public interface IImgForCarouselService
    {
        Task<ImgForCarouselDetailViewModel> CreateInitiativeAsync(CreateInitiativeInputModel inputModel);
        Task<List<ImgForCarouselViewModel>> GetImgForCarouselAsync();
        Task<ImgForCarouselDetailViewModel> GetImgForCarouselDetailAsync(int id);
        Task<bool>IsTitleAvailableAsync(string title,int id);
        Task<bool>CreateInitiativeIsTitleAvailableAsync(string title);
        Task<ImgForCarouselEditInputModel> GetImgForCarouselForEditingAsync(int id);
        Task<ImgForCarouselDetailViewModel> EditImgForCarouselAsync(ImgForCarouselEditInputModel inputModel);
        Task DeleteInitiativeAsync (int id);
    }
}