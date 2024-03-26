using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Almanacco;

namespace SSResurrezioneBR.Models.Services.Application.Almanacco
{
    public interface IAlmanaccoService
    {
        Task<AlmanaccoViewModel?> CreateEventAlmanaccoAsync(CreateEventAlmanaccoInputModel inputModel);
        Task<ListViewModel<AlmanaccoViewModel>?> GetAlmanaccosAsync(ListInputModel model);
        Task<bool>IsTitleAvailableAsync(string titolo, int id);
        Task<AlmanaccoViewModel> EditEventoAlmanaccoAsync(AlmanaccoEditInputModel inputModel);
        Task<AlmanaccoEditInputModel> GetAlmanaccoForEditingAsync(int id);
        Task<bool>CreateEventoAlmanaccoIsTitleAvailableAsync(string Title);
        Task<string>DeleteEventoAlmanaccoAsync(int Id);
    }
}