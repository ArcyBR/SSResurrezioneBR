using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSResurrezioneBR.Models.Entities;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Exceptions.Application;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Options;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Almanacco;
using SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie;
using System.Globalization;
using Z.EntityFramework.Plus;

namespace SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie
{
    public class EfCoreCoroPolifonicoMaterMisericordieService : ICoroPolifonicoMaterMisericordieService
    {
        private readonly SSResurrezioneDbContext dbContext;
        private readonly IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions;
        private readonly ILogger<EfCoreCoroPolifonicoMaterMisericordieService> logger;

        public EfCoreCoroPolifonicoMaterMisericordieService(ILogger<EfCoreCoroPolifonicoMaterMisericordieService> logger, SSResurrezioneDbContext dbContext, IOptionsMonitor<SSResurrezioneOptions> ssResurrezioneOptions)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.ssResurrezioneOptions = ssResurrezioneOptions;
        }
        public async Task<ListViewModel<CoroPolifonicoMaterMisericordieViewModel>> GetCoroPolifonicoMaterMisericordiesAsync(ListInputModel model)
        {
            logger.LogInformation("invocato metodo {0}", nameof(GetCoroPolifonicoMaterMisericordiesAsync));
            
            IQueryable<Entities.CoroPolifonicoMaterMisericordie> baseQuery = dbContext.CoroPolifonicoMaterMisericordies;
            
            switch(model.OrderBy)
            {
                case "CoroPolifonicoMaterMisericordieDataEvento":
                if(model.Ascending){
                    baseQuery = baseQuery.OrderBy(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.DataEventoCoroPolifonicoMaterMisericordie);
                }else{
                    baseQuery = baseQuery.OrderByDescending(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.DataEventoCoroPolifonicoMaterMisericordie);
                }
                break;
                case "CoroPolifonicoMaterMisericordieTitolo":
                if(model.Ascending){
                    baseQuery = baseQuery.OrderBy(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie);
                }else{
                    baseQuery = baseQuery.OrderByDescending(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie);
                }
                break;
            }

            IQueryable<CoroPolifonicoMaterMisericordieViewModel> queryLinq = baseQuery
            .AsNoTracking()
            .Where (coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie.ToUpper().Contains(model.Search.ToUpper()))
            .Include(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.CoroPolifonicoMaterMisericordieFotos)
            .Select(coroPolifonicoMaterMisericordie => CoroPolifonicoMaterMisericordieViewModel.FromEntity(coroPolifonicoMaterMisericordie));
            
            List<CoroPolifonicoMaterMisericordieViewModel> coroPolifonicoMaterMisericordie = await queryLinq
            .Skip(model.Offset)
            .Take(model.Limit)
            .ToListAsync();

            int totalCount = await queryLinq.CountAsync();
            
            ListViewModel<CoroPolifonicoMaterMisericordieViewModel> result = new(){
                Results = coroPolifonicoMaterMisericordie,
                TotalCount =  totalCount
            };
            return result;
        }

        public async Task<CoroPolifonicoMaterMisericordieViewModel> CreateEventCoroPolifonicoMaterMisericordieAsync(CreateEventCoroPolifonicoMaterMisericordieInputModel inputModel){
            string titoloEventoCoroPolifonicoMaterMisericordie = inputModel.Titolo_CoroPolifonicoMaterMisericordie;
            string creatoreEventoCoroPolifonicoMaterMisericordie = "Me medesimo";
            var coroPolifonicoMaterMisericordie = new CoroPolifonicoMaterMisericordie(titoloEventoCoroPolifonicoMaterMisericordie, creatoreEventoCoroPolifonicoMaterMisericordie);
            dbContext.Add(coroPolifonicoMaterMisericordie);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException exc) when ((exc.InnerException as SqliteException)?.SqliteErrorCode == 19)
            {
                throw new TitleUnavailableException("CoroPolifonicoMaterMisericordie",titoloEventoCoroPolifonicoMaterMisericordie, exc);
            }

            return CoroPolifonicoMaterMisericordieViewModel.FromEntity(coroPolifonicoMaterMisericordie);
        }

        public async Task<CoroPolifonicoMaterMisericordieEditInputModel> GetCoroPolifonicoMaterMisericordieForEditingAsync(long id)
        {
            IQueryable<CoroPolifonicoMaterMisericordieEditInputModel> queryLinq = dbContext.CoroPolifonicoMaterMisericordies
                .AsNoTracking()
                .Where(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie == id)
                .Select(coroPolifonicoMaterMisericordie => CoroPolifonicoMaterMisericordieEditInputModel.FromEntity(coroPolifonicoMaterMisericordie)); //Usando metodi statici come FromEntity, la query potrebbe essere inefficiente. Mantenere il mapping nella lambda oppure usare un extension method personalizzato

            CoroPolifonicoMaterMisericordieEditInputModel viewModel = await queryLinq.FirstOrDefaultAsync();

            if (viewModel == null)
            {
                logger.LogWarning("Coro Polifonico Mater Misericordie evento {id} not found", id);
                throw new CoroPolifonicoMaterMisericordieEventoNotFoundException(viewModel.CoroPolifonicoMaterMisericordieTitolo,id);
            }

            return viewModel;
        }

        public async Task<CoroPolifonicoMaterMisericordieViewModel> EditEventoCoroPolifonicoMaterMisericordieAsync(CoroPolifonicoMaterMisericordieEditInputModel inputModel){
            Entities.CoroPolifonicoMaterMisericordie coro = await dbContext.CoroPolifonicoMaterMisericordies.FindAsync((long)inputModel.CoroPolifonicoMaterMisericordieId);
            coro.ChangeTitle(inputModel.CoroPolifonicoMaterMisericordieTitolo);
            coro.ChangeDescription(inputModel.CoroPolifonicoMaterMisericordieDescrizione);
            coro.ChangeEventData(inputModel.CoroPolifonicoMaterMisericordieDataEvento);

            dbContext.Entry(coro).Property(coro => coro.RowVersion).OriginalValue = inputModel.CoroPolifonicoMaterMisericordieRowVersion;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new OptimisticConcurrencyException();
            }
            catch (DbUpdateException exc) when ((exc.InnerException as SqliteException)?.SqliteErrorCode==19){
                throw new TitleUnavailableException("CoroPolifonicoMaterMisericordie",inputModel.CoroPolifonicoMaterMisericordieTitolo, exc);
            }
            return CoroPolifonicoMaterMisericordieViewModel.FromEntity(coro);
        }

        public async Task<bool> IsTitleAvailableAsync(string titolo, long id)
        {
            bool titleExist = await dbContext.CoroPolifonicoMaterMisericordies.AnyAsync(coroPolifonicoMaterMisericordie => EF.Functions.Like(coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie, titolo));
            bool idExist = await dbContext.CoroPolifonicoMaterMisericordies.AnyAsync(coroPolifonicoMaterMisericordie => EF.Functions.Like(coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie.ToString(), id.ToString()));
            if(titleExist && idExist){
                return true;
            }else {
                return !titleExist;
            }
        }

        public async Task<bool> CreateEventoCoroPolifonicoMaterMisericordieIsTitleAvailableAsync(string titolo)
        {
            bool titleExist = await dbContext.CoroPolifonicoMaterMisericordies.AnyAsync(coroPolifonicoMaterMisericordie => EF.Functions.Like(coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie, titolo));
            return !titleExist;
        }
        public async Task DeletePhotoFromEventCoroPolifonicoMaterMisericordieAsync(DeletePhotoFromEventCoroPolifonicoMaterMisericordieInputModel inputModel)
        {
            await dbContext.CoroPolifonicoMaterMisericordieFotos
                .Where(coroPolifonicoMaterMisericordieFoto =>
                coroPolifonicoMaterMisericordieFoto.IdCoroPolifonicoMaterMisericordieFoto == inputModel.CoroPolifonicoMaterMisericordiePhotoId).DeleteAsync();
            dbContext.SaveChanges();
        }

        public async Task<string> DeleteEventoCoroPolifonicoMaterMisericordieAsync(long Id)
        {
            var queryLinq = dbContext.CoroPolifonicoMaterMisericordies.Find((long)Id);

            if (queryLinq == null)
            {
                logger.LogWarning("Coro Polifonico Mater Misericordie evento {id} not found", Id);
                if (queryLinq != null)
                {
                    throw new CoroPolifonicoMaterMisericordieEventoNotFoundException(queryLinq.TitoloCoroPolifonicoMaterMisericordie!, Id);
                }
            }
            
            // la cancellazione è di tipo logico
            queryLinq!.CreatoreEventoCoroPolifonicoMaterMisericordie = "Me Medesimo";
            queryLinq!.ImageVisibileCoroPolifonicoMaterMisericordie = 0;
            //dbContext.CoroPolifonicoMaterMisericordies.Where(coroPolifonicoMaterMisericordie => coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie == Id).Delete();
            await dbContext.SaveChangesAsync();
            return queryLinq!.TitoloCoroPolifonicoMaterMisericordie;
        }
    }
}