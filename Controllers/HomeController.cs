using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SSResurrezioneBR.Models;
using SSResurrezioneBR.Models.Entities;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Exceptions.Application;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Services.Application.AppuntamentiDellaSettimana;
using SSResurrezioneBR.Models.Services.Application.CallApiSantiDelGiorno;
using SSResurrezioneBR.Models.Services.Application.ImgForCarousel;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;
using SSResurrezioneBR.Models.ViewModel.Home;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;

namespace SSResurrezioneBR.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IImgForCarouselService _imgForCarouselService;
    private readonly IConfiguration _configuration;
    private readonly ICallApiSantiDelGiornoService _callApiSantiDelGiornoService;
    private readonly IImgAppuntamentiDellaSettimanaService _imgAppuntamentiDellaSettimanaService;

    public HomeController(IConfiguration configuration, 
            ILogger<HomeController> logger, 
            IImgForCarouselService imgForCarouselService, 
            ICallApiSantiDelGiornoService callApiSantiDelGiornoService,
            IImgAppuntamentiDellaSettimanaService imgAppuntamentiDellaSettimana)
    {
        _configuration = configuration;
        _imgForCarouselService = imgForCarouselService;
        _callApiSantiDelGiornoService = callApiSantiDelGiornoService;
        _imgAppuntamentiDellaSettimanaService = imgAppuntamentiDellaSettimana;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Pagina Principale";
        dynamic myModel = new ExpandoObject();
        try
        {
            var listImgForCarouselViewModel = new List<ImgForCarouselViewModel>();
            listImgForCarouselViewModel = await _imgForCarouselService.GetImgForCarouselAsync();
            var imgAppuntamentiDellaSettimanaViewModel = new ImgAppuntamentiDellaSettimanaViewModel();
            imgAppuntamentiDellaSettimanaViewModel = await _imgAppuntamentiDellaSettimanaService.GetImgAppuntamentiDellaSettimanaAsync();

            // santi del giorno
            myModel.santiDelGiorno = await _callApiSantiDelGiornoService.GetSantiDelGiornoAsync();
            
            // immagini Attivitá in corso
            myModel.imgForCarousel = listImgForCarouselViewModel;

            // immagine Appuntamenti della settimana
            myModel.appuntamentiDellaSettimana = imgAppuntamentiDellaSettimanaViewModel;
        }
        catch (HttpRequestException ex)
        {
            // Handle any exceptions that occurred during the request
            _logger.LogInformation("Si è verificato il seguente errore: {0} ", ex.Message);
        }

        return View(myModel);
    }

    public ActionResult ModalPopUp()  
    {  
        return View();  
    }  

    // creazione di una iniziativa visibile nella home
    public IActionResult CreateInitiative(){
        ViewData["Title"] = "Crea una nuova iniziativa";
        var inputModel = new CreateInitiativeInputModel();
        return View(inputModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInitiative(CreateInitiativeInputModel inputModel){
        if (ModelState.IsValid){
            try{
                ImgForCarouselDetailViewModel imageForCarousel = await _imgForCarouselService.CreateInitiativeAsync(inputModel);
                return RedirectToAction(nameof(EditInitiative), new { id = imageForCarousel.ImageId });
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(Index), nameof(EditInitiative), inputModel);
            }
            catch (TitleUnavailableException){
                ModelState.AddModelError(nameof(inputModel.Image_Content_Title), "Questo titolo giá esiste");
            }
        }
            
        ViewData["Title"] = "Crea una nuova iniziativa";
        return View(inputModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> CreateInitiativeIsTitleAvailable(CreateInitiativeInputModel inputModel){
        bool result = await _imgForCarouselService.CreateInitiativeIsTitleAvailableAsync(inputModel.Image_Content_Title);
        return Json(result);
    }

    public async Task<IActionResult> IsTitleAvailable(ImgForCarouselEditInputModel inputModel){
        bool result = await _imgForCarouselService.IsTitleAvailableAsync(inputModel.ImageContentTitle, (int)inputModel.ImageId);
        return Json(result);
    }

    public async Task<IActionResult> EditInitiative(int id)
    {
        ImgForCarouselEditInputModel inputModel = await _imgForCarouselService.GetImgForCarouselForEditingAsync(id);

        // il titolo della pagina cambia se trattasi di nuovo inserimento o di modifica
        if (string.IsNullOrEmpty(inputModel.ImageLabel) && string.IsNullOrEmpty(inputModel.ImageContentDescription))
        {
            ViewData["Title"] = "Crea una nuova iniziativa";
        }
        else
        {
            ViewData["Title"] = "Modifica Iniziativa";
        }
        return View(inputModel);
    }
    [HttpPost]
    public async Task<IActionResult> EditInitiative(ImgForCarouselEditInputModel inputModel)
    {
        var imagePath = inputModel.ImagePath;
        if (imagePath == null)
        {
            inputModel.ImagePath = @"img/IniziativeInCorso/defaultImage.jpg";
        }
        if (ModelState.IsValid){
            try{
                ImgForCarouselDetailViewModel imgForCarousel = await _imgForCarouselService.EditImgForCarouselAsync(inputModel);
                TempData["MessaggioDiConferma"]="I dati sono stati salvati con successo";
                return RedirectToAction(nameof(Index),nameof(ImgForCarousel), new{id = inputModel.ImageId});
            }
            catch(TitleUnavailableException){
                ModelState.AddModelError(nameof(ImgForCarouselEditInputModel.ImageContentTitle),"Questo titolo giá esiste");
            }
            catch (ImgInvalidException)
            {
                ModelState.AddModelError(nameof(ImgForCarouselEditInputModel.Image), "L\'immagine selezionata non é valida");
            }
            catch (OptimisticConcurrencyException)
            {
                ModelState.AddModelError("", $"Spiacente, il salvataggio non é andato a buon fine perché nel frattempo un altro utente ha aggiornato \"Iniziative in corso\". Ti invito ad aggiornare la pagina e ripetere le modifiche.");
            }
            catch (Exception ex) {
                ModelState.AddModelError(nameof(ImgForCarouselEditInputModel.ImagePath), ex.Message);
            }
        }
        // il titolo della pagina cambia se trattasi di nuovo inserimento o di modifica
        if (string.IsNullOrEmpty(inputModel.ImageLabel) || string.IsNullOrEmpty(inputModel.ImageContentDescription))
        {
            ViewData["Title"] = "Crea una nuova iniziativa";
        }
        else
        {
            ViewData["Title"] = "Modifica Iniziativa";
        }
        return View(inputModel);
    }
    
    public async Task<IActionResult> DeleteInitiative(int id)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _imgForCarouselService.DeleteInitiativeAsync(id);
                TempData["MessaggioDiConferma"] = "L\'iniziativa è stata eliminata con successo";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof (DeleteInitiative), "Hoops! qualcosa è andato storto, l\'iniziativa non è stata eliminata!");
            }
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> EditAppuntamentiDellaSettimana(int id)
    {
        ViewData["Title"] = "Modifica Appuntamenti della Settimana";
        ImgAppuntamentiDellaSettimanaEditInputModel inputModel = await _imgAppuntamentiDellaSettimanaService.GetImgAppuntamentiDellaSettimanaForEditingAsync(id);
        return View(inputModel);
    }
    [HttpPost]
    public async Task<IActionResult> EditAppuntamentiDellaSettimana(ImgAppuntamentiDellaSettimanaEditInputModel inputModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                ImgAppuntamentiDellaSettimanaDetailViewModel imgAppuntamentiDellaSettimana = await _imgAppuntamentiDellaSettimanaService.EditImgAppuntamentiDellaSettimanaAsync(inputModel);
                TempData["MessaggioDiConferma"] = $"L'immagine \"Appuntamenti della settimana\" é stata salvata con successo";
                string homeControllerName = nameof(HomeController);
                return RedirectToAction(nameof(Index), homeControllerName.Replace("Controller", ""));
                //return RedirectToAction(nameof(EditAppuntamentiDellaSettimana), new { id = inputModel.IdFoto });
            }
            catch (ImgInvalidException)
            {
                ModelState.AddModelError(nameof(ImgAppuntamentiDellaSettimanaEditInputModel.Image), "L\'immagine selezionata non é valida");
            }
            catch (OptimisticConcurrencyException)
            {
                ModelState.AddModelError("", $"Spiacente, il salvataggio non é andato a buon fine perché nel frattempo un altro utente ha aggiornato \"Appuntamenti della Settimana\". Ti invito ad aggiornare la pagina e ripetere le modifiche.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(ImgAppuntamentiDellaSettimanaEditInputModel.Image), ex.Message);
            }
        }
        ViewData["Title"] = "Modifica Appuntamenti della Settimana";
        return View(inputModel);
    }
}