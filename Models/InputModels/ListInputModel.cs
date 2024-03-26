using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Customization.ModelBinders;
using SSResurrezioneBR.Models.Options;

namespace SSResurrezioneBR.Models.InputModels
{
    [ModelBinder(BinderType = typeof(ListInputModelBinders))]
    public class ListInputModel
    {
        public ListInputModel(string search, int page, string orderBy, bool ascending, SSResurrezioneOptions ssResurrezioneOptions, string chiamante)
        {
            var orderOption = ssResurrezioneOptions.Order;
            
            Search = search ?? "";
            Page = Math.Max(1, page); // nel caso in query string venga passato volutamete dall'utente un valore negativo;
            OrderBy = orderBy;
            Ascending = ascending;

            Limit = Convert.ToInt16(ssResurrezioneOptions.PerPage);
            Offset = (Page-1)*Limit;
            Chiamante = chiamante;
        }


        public string Search {get;}
        public int Page {get;} 
        public string OrderBy {get;}
        public bool Ascending {get;}
        public int Limit {get;}
        public int Offset {get;}
        public string Chiamante {get;}
    }
}