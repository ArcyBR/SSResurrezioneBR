using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSResurrezioneBR.Models.Services.Application;
using SSResurrezioneBR.Models.Services.Application.Incarichi;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Informazioni;

namespace SSResurrezioneBR.Controllers
{
    
    public class InformazioniController : Controller
    {
        private readonly ILogger<InformazioniController> _logger;
        private readonly IIncarichiService _incarichi;

        public InformazioniController(ILogger<InformazioniController> logger, IIncarichiService incarichi)
        {
            _logger = logger;
            _incarichi = incarichi;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Informazioni";
            dynamic mymodel = new ExpandoObject(); // passa più viewModel alla view
            mymodel.Informazioni = new InformazioniViewModel();
            mymodel.Incarichi = await _incarichi.GetIncarichiAsync();
            return View(mymodel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}