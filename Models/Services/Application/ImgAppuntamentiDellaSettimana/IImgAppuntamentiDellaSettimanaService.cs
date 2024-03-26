using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.AppuntamentiDellaSettimana
{
    public interface IImgAppuntamentiDellaSettimanaService
    {
        Task<ImgAppuntamentiDellaSettimanaDetailViewModel> EditImgAppuntamentiDellaSettimanaAsync(ImgAppuntamentiDellaSettimanaEditInputModel inputModel);
        Task<ImgAppuntamentiDellaSettimanaDetailViewModel> GetImgAppuntamentiDellaSettimanaDetailAsync(int id);
        Task<ImgAppuntamentiDellaSettimanaEditInputModel> GetImgAppuntamentiDellaSettimanaForEditingAsync(int id);
        Task<ImgAppuntamentiDellaSettimanaViewModel> GetImgAppuntamentiDellaSettimanaAsync();
    }
}