using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Models.ViewModel;

namespace SSResurrezioneBR.Customization.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        //public IViewComponentResult Invoke(GenericListViewModel model){
        public IViewComponentResult Invoke(IPaginationInfo model){
             return View(model);
        }       
    }
}