using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SSResurrezioneBR.Models.ViewModel.Almanacco;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie;

namespace SSResurrezioneBR.Models.Services.Application.Almanacco
{
    public class MemoryCacheAlmanaccoService : ICachedAlmanaccoService
    {
        private readonly IConfiguration configuration;
        private readonly IAlmanaccoService almanaccoService;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheAlmanaccoService(IConfiguration configuration, IAlmanaccoService almanaccoService, IMemoryCache memoryCache)
        {
            this.configuration = configuration;
            this.almanaccoService = almanaccoService;
            this.memoryCache = memoryCache;
        }

        public Task<AlmanaccoViewModel?> CreateEventAlmanaccoAsync(CreateEventAlmanaccoInputModel inputModel)
        {
            return memoryCache.GetOrCreateAsync($"Almanaccos{inputModel.Titolo_Almanacco}", CacheEntry=>{
                    CacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(configuration.GetValue<int>("CachedExpiredSeconds")));
                    return almanaccoService.CreateEventAlmanaccoAsync(inputModel);
                });
        }

        public Task<ListViewModel<AlmanaccoViewModel>?> GetAlmanaccosAsync(ListInputModel model)
        {        
            bool canCache = model.Page <= 5 && string.IsNullOrEmpty(model.Search);

            if(canCache){
                return memoryCache.GetOrCreateAsync($"Almanaccos{model.Search}-{model.Page}-{model.OrderBy}-{model.Ascending}", CacheEntry=>{
                    CacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(configuration.GetValue<int>("CachedExpiredSeconds")));
                    return almanaccoService.GetAlmanaccosAsync(model);
                });
            }
            return almanaccoService.GetAlmanaccosAsync(model);
        }

        public Task<bool> IsTitleAvailableAsync(string titolo, long id)
        {
            return almanaccoService.IsTitleAvailableAsync(titolo, id);
        }
        public async Task<AlmanaccoViewModel> EditEventoAlmanaccoAsync(AlmanaccoEditInputModel inputModel)
        {
           return await almanaccoService.EditEventoAlmanaccoAsync(inputModel);
        }
        public async Task<AlmanaccoEditInputModel> GetAlmanaccoForEditingAsync(long id){
            return await almanaccoService.GetAlmanaccoForEditingAsync(id);
        }

        public async Task<bool> CreateEventoAlmanaccoIsTitleAvailableAsync(string title)
        {
             return await almanaccoService.CreateEventoAlmanaccoIsTitleAvailableAsync(title);
        }
        public async Task<string> DeleteEventoAlmanaccoAsync(long id)
        {
            DeleteEventoAlmanaccoInputModel inputModel = new DeleteEventoAlmanaccoInputModel();
            memoryCache.Remove(inputModel.AlmanaccoEventId);
            return await almanaccoService.DeleteEventoAlmanaccoAsync(id);
        }
    }
}