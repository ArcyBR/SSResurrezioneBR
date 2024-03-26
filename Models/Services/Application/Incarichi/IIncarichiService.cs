using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Incarichi;

namespace SSResurrezioneBR.Models.Services.Application.Incarichi
{
    public interface IIncarichiService
    {
        Task<List<IncaricoViewModel>> GetIncarichiAsync();
        Task<IncaricoDetailViewModel> GetIncaricoAsync(int id);
    }
}