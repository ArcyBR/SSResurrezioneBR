using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Options;

namespace SSResurrezioneBR.Customization.ModelBinders
{
    public class ListInputModelBinders: IModelBinder
    {
        private readonly IConfiguration configuration;

        public IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions { get; }
        public ListInputModelBinders(IConfiguration configuration, IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions)
        {
            this.configuration = configuration;
            this.ssResurrezioneOptions = ssResurrezioneOptions;
            
        }
        public Task BindModelAsync(ModelBindingContext bindingContext){
            //recuperiamo i valori grazie ai value provider
            string? search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            int page = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Page").FirstValue);
            string? orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            bool ascending = default;
            if(bindingContext.ValueProvider.GetValue("Ascending").FirstValue == null){
                ascending = configuration.GetValue<bool>("SSResurrezione:Order:Ascending");
            } else{
                ascending = Convert.ToBoolean(bindingContext.ValueProvider.GetValue("Ascending").FirstValue); 
            }
            /*
            if(bindingContext.ValueProvider.GetValue("Ascending").FirstValue != null 
             && bindingContext.ValueProvider.GetValue("inputOrderBy").FirstValue == orderBy){
                ascending = Convert.ToBoolean (bindingContext.ValueProvider.GetValue("Ascending").FirstValue);
            }
            */
            string? chiamante = bindingContext.ValueProvider.GetValue("Chiamante").FirstValue;

            // Creiamo l'istanza del ListInputModel
            var InputModel = new ListInputModel(search, page, orderBy, ascending, ssResurrezioneOptions.CurrentValue, chiamante);

            // Impostiamo il risultato per notificare che la creazione è avvenuta con successo
            bindingContext.Result = ModelBindingResult.Success(InputModel);

            return Task.CompletedTask;
        }

            
            
    }
}