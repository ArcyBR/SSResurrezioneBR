using ImageMagick;
using Microsoft.Data.Sqlite;
using SSResurrezioneBR.Models.Entities;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Exceptions.Application;
using SSResurrezioneBR.Models.Exceptions.Infrastructure;
using SSResurrezioneBR.Models.Services.Application.AppuntamentiDellaSettimana;
using SSResurrezioneBR.Models.Services.Application.ImgForCarousel;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.ViewModel.Home;
using System.Data;

namespace SSResurrezioneBR.Models.Services.Application.ImgAppuntamentiDellaSettimana
{
    public class ImgAppuntamentiDellaSettimanaService: IImgAppuntamentiDellaSettimanaService
    {
        private readonly ISqliteDatabaseAccessor db;
        private readonly ILogger<ImgAppuntamentiDellaSettimanaService> logger;
        private readonly IImgPersisterAS imagePersister;

        public ImgAppuntamentiDellaSettimanaService(ILogger<ImgAppuntamentiDellaSettimanaService> logger, ISqliteDatabaseAccessor db, IImgPersisterAS imagePersister)
        {
            this.logger = logger;
            this.db = db;
            this.imagePersister = imagePersister;
        }
        public async Task<ImgAppuntamentiDellaSettimanaDetailViewModel> GetImgAppuntamentiDellaSettimanaDetailAsync(int id)
        {
            logger.LogInformation("Chiamato il metodo {0} ", nameof(GetImgAppuntamentiDellaSettimanaDetailAsync));

            FormattableString query = @$"Select IdFoto, PathFoto, ReviewerFoto from AppuntamentiDellaSettimana_Foto 
                                where IdFoto = {id}";
            DataSet dataset = await db.QueryAsync(query);
            DataTable dataTable = dataset.Tables[0];
            if (dataTable.Rows.Count != 1)
            {
                throw new InvalidOperationException($"Did not return exactly 1 row for image carousel {id}");
            }
            var imgAppuntamentiDellaSettimanaDetailRow = dataTable.Rows[0];
            ImgAppuntamentiDellaSettimanaDetailViewModel imgAppuntamentiDellaSettimanaDetail = ImgAppuntamentiDellaSettimanaDetailViewModel.FromDataRow(imgAppuntamentiDellaSettimanaDetailRow);

            return imgAppuntamentiDellaSettimanaDetail;
        }

        public async Task<ImgAppuntamentiDellaSettimanaDetailViewModel> EditImgAppuntamentiDellaSettimanaAsync(ImgAppuntamentiDellaSettimanaEditInputModel inputModel)
        {
            string reviewerFoto = "me medesimo";
            try
            {
                if (inputModel.Image != null)
                {
                    string pathFoto = await imagePersister.SaveImageAsync(inputModel.Image);

                    int affectedRows = await db.CommandAsync($"UPDATE AppuntamentiDellaSettimana_Foto SET PathFoto = COALESCE({pathFoto}, PathFoto), ReviewerFoto = {reviewerFoto} where IdFoto = {inputModel.IdFoto} and RowVersion={inputModel.RowVersion};");
                    if (affectedRows == 0)
                    {
                        bool appuntamentiDellaSettimanaExist = await db.QueryScalarAsync<bool> ($"Select count(*) from AppuntamentiDellaSettimana_Foto where IdFoto={inputModel.IdFoto}");
                        if (appuntamentiDellaSettimanaExist)
                        {
                            throw new OptimisticConcurrencyException();
                        }
                        else
                        {
                            throw new ImgNotFoundException("Appuntamento della Settimana", (int)inputModel.IdFoto!);
                        }
                        
                    }
                }
            }
            catch (ImagePersistenceException exc)
            {
                throw new ImgInvalidException(inputModel.Image!, exc);
            }

            ImgAppuntamentiDellaSettimanaDetailViewModel imgAppuntamentiDellaSettimana = await GetImgAppuntamentiDellaSettimanaDetailAsync((int)inputModel.IdFoto!);
            return imgAppuntamentiDellaSettimana;
        }

        public async Task<ImgAppuntamentiDellaSettimanaEditInputModel> GetImgAppuntamentiDellaSettimanaForEditingAsync(int id)
        {
            FormattableString query = @$"Select IdFoto, PathFoto, ReviewerFoto, RowVersion from AppuntamentiDellaSettimana_Foto 
                                where IdFoto = {id}";
            DataSet dataSet = await db.QueryAsync(query);
            var imgAppuntamentiDellaSettimanaTable = dataSet.Tables[0];
            if (imgAppuntamentiDellaSettimanaTable.Rows.Count == 0)
            {
                logger.LogWarning($"Immagine \"Appuntamenti della Settimana\" {id} non trovata");
                throw new ImgNotFoundException("Appuntamenti della Settimana", id);
            }
            var imgAppuntamentiDellaSettimanaRow = imgAppuntamentiDellaSettimanaTable.Rows[0];
            var imgAppuntamentiDellaSettimanaEditInputModel = ImgAppuntamentiDellaSettimanaEditInputModel.FromDataRow(imgAppuntamentiDellaSettimanaRow);
            return imgAppuntamentiDellaSettimanaEditInputModel;
        }
        public async Task<ImgAppuntamentiDellaSettimanaViewModel> GetImgAppuntamentiDellaSettimanaAsync()
        {
            logger.LogInformation("Chiamato il metodo {0} ", nameof(GetImgAppuntamentiDellaSettimanaAsync));

            FormattableString query = @$"Select IdFoto, PathFoto, ReviewerFoto from AppuntamentiDellaSettimana_Foto";
            DataSet dataset = await db.QueryAsync(query);
            DataTable dataTable = dataset.Tables[0];

            ImgAppuntamentiDellaSettimanaViewModel imgAppuntamentiDellaSettimana = new();

            foreach (DataRow imgAppuntamentiDellaSettimanaRow in dataTable.Rows)
            {
                imgAppuntamentiDellaSettimana = ImgAppuntamentiDellaSettimanaViewModel.FromDataRow(imgAppuntamentiDellaSettimanaRow);
            }
            return imgAppuntamentiDellaSettimana;
        }
    }
}