using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Services.Application.Almanacco;
using SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Entities;

namespace SSResurrezioneBR.Controllers
{
    public class CoroPolifonicoMaterMisericordieController : Controller
    {
        private readonly ILogger<CoroPolifonicoMaterMisericordieController> _logger;
        private readonly ICoroPolifonicoMaterMisericordieService _coroPolifonicoMaterMisericordie;

        public CoroPolifonicoMaterMisericordieController(ILogger<CoroPolifonicoMaterMisericordieController> logger, ICachedCoroPolifonicoMaterMisericordieService coroPolifonicoMaterMisericordie)
        {
            _logger = logger;
            this._coroPolifonicoMaterMisericordie = coroPolifonicoMaterMisericordie;
        }
        public async Task<IActionResult> Index(ListInputModel model)
        {
            ViewData["Title"] = "Coro Polifonico Mater Misericordie";
            ListViewModel<CoroPolifonicoMaterMisericordieViewModel> listCoroPolifonicoMaterMisericordieViewModel = await _coroPolifonicoMaterMisericordie.GetCoroPolifonicoMaterMisericordiesAsync(model);

            GenericListViewModel coroPolifonicoMaterMisericordieListViewModel = new GenericListViewModel
            {
                ListCoroPolifonicoMaterMisericordieViewModel = listCoroPolifonicoMaterMisericordieViewModel,
                ListInputModel = model
            };

            return View(coroPolifonicoMaterMisericordieListViewModel);
        }

        public IActionResult CreateChorusEvent()
        {
            ViewData["Title"] = "Crea un nuovo evento per il Coro";
            var inputModel = new CreateEventCoroPolifonicoMaterMisericordieInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChorusEvent(CreateEventCoroPolifonicoMaterMisericordieInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CoroPolifonicoMaterMisericordieViewModel coroPolifonicoMaterMisericordie = await _coroPolifonicoMaterMisericordie.CreateEventCoroPolifonicoMaterMisericordieAsync(inputModel);
                    return RedirectToAction(nameof(Index), inputModel);
                }
                catch (TitleUnavailableException)
                {
                    ModelState.AddModelError(nameof(inputModel.Titolo_CoroPolifonicoMaterMisericordie), "Questo titolo giá esiste");
                }
            }

            ViewData["Title"] = "Crea un nuovo evento per il Coro";
            return View(inputModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public async Task<IActionResult> CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailable(CreateEventAlmanaccoInputModel inputModel)
        {
            bool result = await _coroPolifonicoMaterMisericordie.CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailableAsync(inputModel.Titolo_Almanacco);
            return Json(result);
        }

        public async Task<IActionResult> IsTitleAvailable(CoroPolifonicoMaterMisericordieEditInputModel inputModel)
        {
            bool result = await _coroPolifonicoMaterMisericordie.IsTitleAvailableAsync(inputModel.CoroPolifonicoMaterMisericordieTitolo, (int)inputModel.CoroPolifonicoMaterMisericordieId);
            return Json(result);
        }

        public async Task<IActionResult> EditEventoCoroPolifonicoMaterMisericordie(int id)
        {
            CoroPolifonicoMaterMisericordieEditInputModel viewModel = await _coroPolifonicoMaterMisericordie.GetCoroPolifonicoMaterMisericordieForEditingAsync(id);
            ViewData["Title"] = "Modifica evento Coro: " + viewModel.CoroPolifonicoMaterMisericordieTitolo;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEventoCoroPolifonicoMaterMisericordie(CoroPolifonicoMaterMisericordieEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CoroPolifonicoMaterMisericordieViewModel almanacco = await _coroPolifonicoMaterMisericordie.EditEventoCoroPolifonicoMaterMisericordieAsync(inputModel);
                    TempData["MessaggioDiConferma"] = "I dati sono stati salvati con successo";
                    return RedirectToAction(nameof(Index), new { chiamante = "CoroPolifonicoMaterMisericordie" });
                }
                catch (TitleUnavailableException)
                {
                    ModelState.AddModelError(nameof(CoroPolifonicoMaterMisericordieViewModel.CoroPolifonicoMaterMisericordieTitolo), "Questo titolo giá esiste");
                }
            }
            ViewData["Title"] = "Modifica Coro Polifonico Mater Misericordie";
            return View(inputModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePhotoFromEventCoroPolifonicoMaterMisericordie(DeletePhotoFromEventCoroPolifonicoMaterMisericordieInputModel inputModel)
        {
            await _coroPolifonicoMaterMisericordie.DeletePhotoFromEventCoroPolifonicoMaterMisericordieAsync(inputModel);
            TempData["MessaggioDiConferma"] = "L\'immagine riferita all\'evento del coro: {} è stata eliminata";
            return RedirectToAction(nameof(CoroPolifonicoMaterMisericordieController.Index), "CoroPolifonicoMaterMisericordie", new { CoroPolifonicoMaterMisericordieEventId = inputModel.CoroPolifonicoMaterMisericordieEventId });
        }

        public async Task<IActionResult> DeleteEventoCoroPolifonicoMaterMisericordie(int Id)
        {
            string titoloEventoCoro = await _coroPolifonicoMaterMisericordie.DeleteEventoCoroPolifonicoMaterMisericordieAsync(Id);
            TempData["MessaggioDiConferma"] = $"L\'evento del coro: {titoloEventoCoro} è stato eliminato";
            //RedirectToAction(nameof(CoroPolifonicoMaterMisericordieController.Index), "CoroPolifonicoMaterMisericordie", new { CoroPolifonicoMaterMisericordieEventId = Id });
            return RedirectToAction(nameof(CoroPolifonicoMaterMisericordieController.Index), new { chiamante = "CoroPolifonicoMaterMisericordie" });
        }
    }
}