using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel.Almanacco;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;

namespace SSResurrezioneBR.Models.ViewModel
{
    public class GenericListViewModel : IPaginationInfo
    {
        public ListViewModel<AlmanaccoViewModel>? ListAlmanaccoViewModel {get;set;}
        public ListInputModel? ListInputModel {get;set;}
        public ListViewModel<CoroPolifonicoMaterMisericordieViewModel>? ListCoroPolifonicoMaterMisericordieViewModel {get;set;}

        int IPaginationInfo.CurrentPage => ListInputModel.Page;

        private int _TotalCount;
        public int TotalResult {
            get {
                    if (ListInputModel.Chiamante == "Almanacco") {
                        _TotalCount = ListAlmanaccoViewModel.TotalCount;
                    }else if (ListInputModel.Chiamante == "CoroPolifonicoMaterMisericordie"){
                        _TotalCount = ListCoroPolifonicoMaterMisericordieViewModel.TotalCount;
                    }
                return _TotalCount;
                } 
        }
        
        int IPaginationInfo.ResultsPerPage => ListInputModel.Limit;

        string IPaginationInfo.Search => ListInputModel.Search;

        string IPaginationInfo.OrderBy => ListInputModel.OrderBy;

        bool IPaginationInfo.Ascending => ListInputModel.Ascending;

        string IPaginationInfo.Chiamante => ListInputModel.Chiamante;
        int _Results; 
        int IPaginationInfo.Results {
            get {
                if (ListInputModel.Chiamante == "Almanacco") {
                    _Results = ListAlmanaccoViewModel.Results.Count;
                }else if (ListInputModel.Chiamante == "CoroPolifonicoMaterMisericordie"){
                    _Results = ListCoroPolifonicoMaterMisericordieViewModel.Results.Count;
                }
                return _Results;
                } 
        } 
    }
}