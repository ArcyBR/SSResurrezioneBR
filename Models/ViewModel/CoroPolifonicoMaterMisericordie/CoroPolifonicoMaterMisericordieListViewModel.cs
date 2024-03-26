using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.InputModels;

namespace SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie
{
    public class CoroPolifonicoMaterMisericordieListViewModel
    {
        public ListViewModel<CoroPolifonicoMaterMisericordieViewModel> ListCoroPolifonicoMaterMisericordieViewModel {get;set;}
        public ListInputModel ListInputModel {get;set;}
    }
}