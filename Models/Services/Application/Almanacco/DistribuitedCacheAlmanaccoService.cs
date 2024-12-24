using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Almanacco;

namespace SSResurrezioneBR.Models.Services.Application.Almanacco
{
    public class DistribuitedCacheAlmanaccoService : ICachedAlmanaccoService
    {
        private readonly IConfiguration _configuration;
        private readonly IAlmanaccoService _almanaccoService;
        private readonly IDistributedCache _distribuitedCache;

        public DistribuitedCacheAlmanaccoService(IConfiguration configuration, IAlmanaccoService almanaccoService, IDistributedCache distribuitedCache)
        {
            this._configuration = configuration;
            this._almanaccoService = almanaccoService;
            this._distribuitedCache = distribuitedCache;
        }

        public async Task<ListViewModel<AlmanaccoViewModel>> GetAlmanaccosAsync(ListInputModel model)
        {
            string key = $"Almanaccos";
            string serializeObject = await _distribuitedCache.GetStringAsync(key);
            if (serializeObject!=null)
            {
                return Deserialize<ListViewModel<AlmanaccoViewModel>>(serializeObject);
            }
            ListViewModel<AlmanaccoViewModel> almanaccos = await _almanaccoService.GetAlmanaccosAsync(model);
            serializeObject = Serialize(almanaccos);
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(_configuration.GetValue<int>("CachedExpiredSeconds")));
            await _distribuitedCache.SetStringAsync(key, serializeObject,cacheOptions);
            return almanaccos;
        }

        private string Serialize(Object obj)
        {
            // convertiamo un oggetto in una stringa json
            return JsonConvert.SerializeObject(obj);
        }
        private T Deserialize<T>(string serializedObject)
        {
            // riconvertiamo una stringa json in un oggetto
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public async Task<AlmanaccoViewModel> CreateEventAlmanaccoAsync(CreateEventAlmanaccoInputModel inputModel)
        {
            string key = $"Almanaccos";
            string serializeObject = await _distribuitedCache.GetStringAsync(key);
            if (serializeObject!=null)
            {
                return Deserialize<AlmanaccoViewModel>(serializeObject);
            }
            AlmanaccoViewModel almanaccos = await _almanaccoService.CreateEventAlmanaccoAsync(inputModel);
            serializeObject = Serialize(almanaccos);
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(_configuration.GetValue<int>("CachedExpiredSeconds")));
            await _distribuitedCache.SetStringAsync(key, serializeObject,cacheOptions);
            return almanaccos;
        }

        public async Task<bool> IsTitleAvailableAsync(string title)
        {
            throw new NotImplementedException();
        }
        public async Task<AlmanaccoViewModel> EditEventoAlmanaccoAsync(AlmanaccoEditInputModel inputModel)
        {
            throw new NotImplementedException();
        }
        public async Task<AlmanaccoEditInputModel> GetAlmanaccoForEditingAsync(long id){
            throw new NotImplementedException();
        }

        public Task<bool> IsTitleAvailableAsync(string titolo, long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateEventoAlmanaccoIsTitleAvailableAsync(string Title)
        {
            throw new NotImplementedException();
        }
        public Task<string> DeleteEventoAlmanaccoAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}