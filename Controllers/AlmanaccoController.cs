using Microsoft.AspNetCore.Mvc;
using SSResurrezioneBR.Models.Entities;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Services.Application.Almanacco;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Almanacco;

namespace SSResurrezioneBR.Controllers
{
    public class AlmanaccoController : Controller
    {
        private readonly IAlmanaccoService _almanacco;
        public AlmanaccoController(ICachedAlmanaccoService almanacco)
        {
            this._almanacco = almanacco;
        }
        
        public async Task<IActionResult> Index(ListInputModel model)
        {
            ViewData["Title"] = "Almanacco";
            ListViewModel<AlmanaccoViewModel> listAlmanaccoViewModel = await _almanacco.GetAlmanaccosAsync(model);
            
            GenericListViewModel almanaccoListViewModel = new GenericListViewModel {
                ListAlmanaccoViewModel = listAlmanaccoViewModel,
                ListInputModel = model
            };

            return View(almanaccoListViewModel);
        }

        public IActionResult CreateAlmanaccoEvent(){
            ViewData["Title"] = "Crea un nuovo evento per l'Almanacco";
            var inputModel = new CreateEventAlmanaccoInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlmanaccoEvent (CreateEventAlmanaccoInputModel inputModel){
            if (ModelState.IsValid){
                try{
                    AlmanaccoViewModel almanacco = await _almanacco.CreateEventAlmanaccoAsync(inputModel);
                    return RedirectToAction(nameof(EditEventoAlmanacco), new { almanaccoId = almanacco.AlmanaccoId, almanaccoTitoloNuovoEvento = almanacco.AlmanaccoTitolo }); 
                }catch(TitleUnavailableException){
                    ModelState.AddModelError(nameof(inputModel.Titolo_Almanacco), "Questo titolo giá esiste");
                }
            }
            ViewData["Title"] = "Crea un nuovo evento per l'Almanacco";
            return View(inputModel);
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public async Task<IActionResult> CreateEventoAlmanaccoIsTitleAvailable(CreateEventAlmanaccoInputModel inputModel){
            bool result = await _almanacco.CreateEventoAlmanaccoIsTitleAvailableAsync(inputModel.Titolo_Almanacco);
            return Json(result);
        }
        public async Task<IActionResult> IsTitleAvailable(AlmanaccoEditInputModel inputModel){
            bool result = await _almanacco.IsTitleAvailableAsync(inputModel.AlmanaccoTitolo.ToString(), inputModel.AlmanaccoId);
            return Json(result);
        }

        public async Task<IActionResult> EditEventoAlmanacco(long almanaccoId, string almanaccoTitoloNuovoEvento="")
        {
            AlmanaccoEditInputModel viewModel = await _almanacco.GetAlmanaccoForEditingAsync(almanaccoId);
            if (string.IsNullOrEmpty(almanaccoTitoloNuovoEvento))
            {
                ViewData["Title"] = "Modifica evento Almanacco: " + viewModel.AlmanaccoTitolo;
            }
            else
            {
                ViewData["Title"] = "Inserimento nuovo evento Almanacco: " + almanaccoTitoloNuovoEvento;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditEventoAlmanacco(AlmanaccoEditInputModel inputModel)
        {  
            if(ModelState.IsValid){
                try{
                    AlmanaccoViewModel almanacco = await _almanacco.EditEventoAlmanaccoAsync(inputModel);
                    TempData["MessaggioDiConferma"]="I dati sono stati salvati con successo";
                    return RedirectToAction(nameof(Index), new{chiamante = "Almanacco"});
                }
                catch(TitleUnavailableException){
                    ModelState.AddModelError(nameof(AlmanaccoViewModel.AlmanaccoTitolo),"Questo titolo giá esiste");
                }
            }
            ViewData["Title"] = "Modifica Almanacco";
            return View(inputModel);
        }

        public async Task<IActionResult> DeleteEventoAlmanacco(long Id)
        {
            string titoloEventoAlmanacco = await _almanacco.DeleteEventoAlmanaccoAsync(Id);
            TempData["MessaggioDiConferma"] = $"L\'evento dell'almanacco : {titoloEventoAlmanacco} è stato eliminato";
            //RedirectToAction(nameof(CoroPolifonicoMaterMisericordieController.Index), "CoroPolifonicoMaterMisericordie", new { CoroPolifonicoMaterMisericordieEventId = Id });
            return RedirectToAction(nameof(AlmanaccoController.Index), new { chiamante = "Almanacco" });
        }
    }
}
