using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSResurrezioneBR.Models.Options;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.ViewModel.Almanacco;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.Exceptions;
using Microsoft.Data.Sqlite;
using SSResurrezioneBR.Models.Exceptions.Application;

namespace SSResurrezioneBR.Models.Services.Application.Almanacco
{
    public class EfCoreAlmanaccoService : IAlmanaccoService
    {
        private readonly SSResurrezioneDbContext dbContext;
        private readonly IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions;
        private readonly ILogger<EfCoreAlmanaccoService> logger;

        public EfCoreAlmanaccoService(ILogger<EfCoreAlmanaccoService> logger, SSResurrezioneDbContext dbContext, IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.ssResurrezioneOptions = ssResurrezioneOptions;
        }

        public async Task<AlmanaccoViewModel> CreateEventAlmanaccoAsync(CreateEventAlmanaccoInputModel inputModel){
            string titoloAlmanacco = inputModel.Titolo_Almanacco;
            string creatoreEventoAlmanacco = "me medesimo";
            var almanacco = new Entities.Almanacco (titoloAlmanacco, creatoreEventoAlmanacco);
            dbContext.Add(almanacco);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exc) when ((exc.InnerException as SqliteException)?.SqliteErrorCode == 19)
            {
                throw new TitleUnavailableException("Almanacco",titoloAlmanacco, exc);
            }
            
            return AlmanaccoViewModel.FromEntity(almanacco);
        }
        public async Task<ListViewModel<AlmanaccoViewModel>> GetAlmanaccosAsync(ListInputModel model)
        {
            logger.LogInformation("invocato metodo {0}", nameof(GetAlmanaccosAsync));
            
            IQueryable<Entities.Almanacco> baseQuery = dbContext.Almanaccos;
            
            switch(model.OrderBy)
            {
                case "AlmanaccoDataEvento":
                if(model.Ascending){
                    baseQuery = baseQuery.OrderBy(almanacco => almanacco.DataEventoAlmanacco);
                }else{
                    baseQuery = baseQuery.OrderByDescending(almanacco => almanacco.DataEventoAlmanacco);
                }
                break;
                case "AlmanaccoTitolo":
                if(model.Ascending){
                    baseQuery = baseQuery.OrderBy(almanacco => almanacco.TitoloAlmanacco);
                }else{
                    baseQuery = baseQuery.OrderByDescending(almanacco => almanacco.TitoloAlmanacco);
                }
                break;
            }

            IQueryable<AlmanaccoViewModel> queryLinq = baseQuery
            .AsNoTracking()
            .Where (almanacco => almanacco.ImageVisibileAlmanacco == 1 && almanacco.TitoloAlmanacco.ToUpper().Contains(model.Search.ToUpper()))
            .Include(almanacco => almanacco.AlmanaccoFotos)   
            .Select(almanacco => AlmanaccoViewModel.FromEntity(almanacco));
            
            List<AlmanaccoViewModel> almanaccos = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
            .ToListAsync();

            int totalCount = await queryLinq.CountAsync();

            ListViewModel<AlmanaccoViewModel> result = new(){
                Results = almanaccos,
                TotalCount =  totalCount
            };

            return result;
        }
        public async Task<AlmanaccoEditInputModel> GetAlmanaccoForEditingAsync(int id)
        {
            IQueryable<AlmanaccoEditInputModel> queryLinq = dbContext.Almanaccos
                .AsNoTracking()
                .Where(almanacco => almanacco.IdAlmanacco == id)
                .Select(almanacco => AlmanaccoEditInputModel.FromEntity(almanacco)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            AlmanaccoEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Almanacco evento {id} not found", id);
                throw new AlmanaccoEventoNotFoundException(viewModel.AlmanaccoTitolo,id);
            }

            return viewModel;
        }
        public async Task<AlmanaccoViewModel> EditEventoAlmanaccoAsync(AlmanaccoEditInputModel inputModel)
        {
            Entities.Almanacco almanacco = await dbContext.Almanaccos.FindAsync((long)inputModel.AlmanaccoId);
            almanacco.ChangeTitle(inputModel.AlmanaccoTitolo);
            almanacco.ChangeDescription(inputModel.AlmanaccoDescrizione);
            almanacco.ChangeEventData(inputModel.AlmanaccoDataEvento);

            dbContext.Entry(almanacco).Property(almanacco => almanacco.RowVersion).OriginalValue = inputModel.AlmanaccoRowVersion;

            try{
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new OptimisticConcurrencyException();
            }
            catch (DbUpdateException exc) when ((exc.InnerException as SqliteException)?.SqliteErrorCode==19){
                throw new TitleUnavailableException("Almanacco",inputModel.AlmanaccoTitolo, exc);
            }
            return AlmanaccoViewModel.FromEntity(almanacco);
        }
        public async Task<bool> IsTitleAvailableAsync(string titolo, int id){
            bool titleExist = await dbContext.Almanaccos.AnyAsync(almanacco => EF.Functions.Like(almanacco.TitoloAlmanacco, titolo));
            bool idExist = await dbContext.Almanaccos.AnyAsync(almanacco => EF.Functions.Like(almanacco.IdAlmanacco.ToString(), id.ToString()));
            if(titleExist && idExist){
                return true;
            }else {
                return !titleExist;
            }
        }
        public async Task<bool> CreateEventoAlmanaccoIsTitleAvailableAsync(string titolo)
        {
            bool titleExist = await dbContext.Almanaccos.AnyAsync(almanacco => EF.Functions.Like(almanacco.TitoloAlmanacco, titolo));
            return !titleExist;
        }

        public async Task<string> DeleteEventoAlmanaccoAsync(int Id)
        {
            var queryLinq = dbContext.Almanaccos.Find((long)Id);

            if (queryLinq == null)
            {
                logger.LogWarning("Almanacco evento {id} not found", Id);
                if (queryLinq != null)
                {
                    throw new AlmanaccoEventoNotFoundException(queryLinq.TitoloAlmanacco!, Id);
                }
            }

            // la cancellazione � di tipo logico
            queryLinq!.CreatoreEventoAlmanacco = "Me Medesimo";
            queryLinq!.ImageVisibileAlmanacco = 0;
            //dbContext.CoroPolifonicoMaterMisericordies.Where(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie == Id).Delete();
            await dbContext.SaveChangesAsync();
            return queryLinq!.TitoloAlmanacco;
        }
    }
}