using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Services.Application.Almanacco;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;

namespace SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie
{
    public class MemoryCacheCoroPolifonicoMaterMisericordieService : ICachedCoroPolifonicoMaterMisericordieService
    {
        private readonly IConfiguration configuration;
        private readonly ICoroPolifonicoMaterMisericordieService coroPolifonicoMaterMisericordieService;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheCoroPolifonicoMaterMisericordieService(IConfiguration configuration, ICoroPolifonicoMaterMisericordieService coroPolifonicoMaterMisericordieService, IMemoryCache memoryCache)
        {
            this.configuration = configuration;
            this.coroPolifonicoMaterMisericordieService = coroPolifonicoMaterMisericordieService;
            this.memoryCache = memoryCache;
        }

        public Task<ListViewModel<CoroPolifonicoMaterMisericordieViewModel>> GetCoroPolifonicoMaterMisericordiesAsync(ListInputModel model)
        {           
            return memoryCache.GetOrCreateAsync($"CoroPolifonicoMaterMisericordies{model.Search}-{model.Page}-{model.OrderBy}-{model.Ascending}", CacheEntry=>{
                CacheEntry.SetSize(1);
                CacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(configuration.GetValue<int>("CachedExpiredSeconds")));
                return coroPolifonicoMaterMisericordieService.GetCoroPolifonicoMaterMisericordiesAsync(model);
            });
        }
        public Task<CoroPolifonicoMaterMisericordieViewModel> CreateEventCoroPolifonicoMaterMisericordieAsync(CreateEventCoroPolifonicoMaterMisericordieInputModel inputModel){
            return memoryCache.GetOrCreateAsync($"CoroPolifonicoMaterMisericordies{inputModel.Equals}", CacheEntry=>{
                CacheEntry.SetSize(1);
                CacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(configuration.GetValue<int>("CachedExpiredSeconds")));
                return coroPolifonicoMaterMisericordieService.CreateEventCoroPolifonicoMaterMisericordieAsync(inputModel);
            });
        }

        public Task<bool> IsTitleAvailableAsync(string titolo, int id)
        {
           return coroPolifonicoMaterMisericordieService.IsTitleAvailableAsync(titolo, id);
        }
        public async Task<CoroPolifonicoMaterMisericordieViewModel> EditEventoCoroPolifonicoMaterMisericordieAsync(CoroPolifonicoMaterMisericordieEditInputModel inputModel)
        {
           return await coroPolifonicoMaterMisericordieService.EditEventoCoroPolifonicoMaterMisericordieAsync(inputModel);
        }
        public async Task<CoroPolifonicoMaterMisericordieEditInputModel> GetCoroPolifonicoMaterMisericordieForEditingAsync(int id){
            return await coroPolifonicoMaterMisericordieService.GetCoroPolifonicoMaterMisericordieForEditingAsync(id);
        }

        public async Task<bool> CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailableAsync(string title)
        {
            return await coroPolifonicoMaterMisericordieService.CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailableAsync(title);
        }

        public async Task DeletePhotoFromEventCoroPolifonicoMaterMisericordieAsync(DeletePhotoFromEventCoroPolifonicoMaterMisericordieInputModel inputModel)
        {
            await coroPolifonicoMaterMisericordieService.DeletePhotoFromEventCoroPolifonicoMaterMisericordieAsync(inputModel);
            memoryCache.Remove(inputModel.CoroPolifonicoMaterMisericordiePhotoId);
            memoryCache.Remove(inputModel.CoroPolifonicoMaterMisericordieEventId);
        }
        public async Task<string> DeleteEventoCoroPolifonicoMaterMisericordieAsync(int id)
        {
            DeleteEventoCoroPolifonicoMaterMisericordieInputModel inputModel = new DeleteEventoCoroPolifonicoMaterMisericordieInputModel();
            memoryCache.Remove(inputModel.CoroPolifonicoMaterMisericordieEventId);
            return await coroPolifonicoMaterMisericordieService.DeleteEventoCoroPolifonicoMaterMisericordieAsync(id);
        }
    }
}