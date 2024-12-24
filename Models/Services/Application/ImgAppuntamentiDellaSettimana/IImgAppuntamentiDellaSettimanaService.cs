using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.AppuntamentiDellaSettimana
{
    public interface IImgAppuntamentiDellaSettimanaService
    {
        Task<ImgAppuntamentiDellaSettimanaDetailViewModel> EditImgAppuntamentiDellaSettimanaAsync(ImgAppuntamentiDellaSettimanaEditInputModel inputModel);
        Task<ImgAppuntamentiDellaSettimanaDetailViewModel> GetImgAppuntamentiDellaSettimanaDetailAsync(long id);
        Task<ImgAppuntamentiDellaSettimanaEditInputModel> GetImgAppuntamentiDellaSettimanaForEditingAsync(long id);
        Task<ImgAppuntamentiDellaSettimanaViewModel> GetImgAppuntamentiDellaSettimanaAsync();
    }
}