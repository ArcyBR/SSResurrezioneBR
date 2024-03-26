using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;

namespace SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie
{
    public interface ICoroPolifonicoMaterMisericordieService
    {
        Task<CoroPolifonicoMaterMisericordieViewModel> CreateEventCoroPolifonicoMaterMisericordieAsync(CreateEventCoroPolifonicoMaterMisericordieInputModel inputModel);
        Task<ListViewModel<CoroPolifonicoMaterMisericordieViewModel>> GetCoroPolifonicoMaterMisericordiesAsync(ListInputModel model);
        Task<bool>IsTitleAvailableAsync(string title, int id);
        Task<CoroPolifonicoMaterMisericordieViewModel> EditEventoCoroPolifonicoMaterMisericordieAsync(CoroPolifonicoMaterMisericordieEditInputModel inputModel);
        Task<CoroPolifonicoMaterMisericordieEditInputModel> GetCoroPolifonicoMaterMisericordieForEditingAsync(int id);
        Task<bool>CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailableAsync(string Title);
        Task DeletePhotoFromEventCoroPolifonicoMaterMisericordieAsync(DeletePhotoFromEventCoroPolifonicoMaterMisericordieInputModel inputModel);
        Task<string> DeleteEventoCoroPolifonicoMaterMisericordieAsync(int Id);
    }
}