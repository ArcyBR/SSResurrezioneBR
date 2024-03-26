using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel
{
    public interface IPaginationInfo
    {
        int CurrentPage {get;}
        int TotalResult {get;}
        int Results {get;}
        int ResultsPerPage {get;}
        string Search {get;}
        string OrderBy {get;}
        bool Ascending {get;}
        string Chiamante {get;}
    }
}