using System.Data;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.ViewModel;
using SSResurrezioneBR.Models.ViewModel.Incarichi;

namespace SSResurrezioneBR.Models.Services.Application.Incarichi
{
    public class IncarichiService : IIncarichiService
    {
        private readonly ISqliteDatabaseAccessor db;
        private readonly ILogger<IncarichiService> logger;
        public IncarichiService(ILogger<IncarichiService> logger, ISqliteDatabaseAccessor db)
        {
            this.logger = logger;
            this.db = db;
        }
        public async Task<List<IncaricoViewModel>> GetIncarichiAsync()
        {
            logger.LogInformation("invocato metodo {0}", nameof(GetIncarichiAsync));

            FormattableString query = $"Select Id, Cognome, Titolo, Nome, Incarico, Link_Diocesi from Incarichi";
            DataSet dataset = await db.QueryAsync(query);
            DataTable dataTable = dataset.Tables[0];

            List<IncaricoViewModel> incarichiList = new List<IncaricoViewModel>();

            foreach(DataRow incaricoRow in dataTable.Rows){
                IncaricoViewModel incarico = IncaricoViewModel.FromDataRow(incaricoRow);
                incarichiList.Add(incarico);
            }
            return incarichiList;
        }

        public Task<IncaricoDetailViewModel> GetIncaricoAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}