using System.Data;
using ImageMagick;
using Microsoft.Data.Sqlite;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Exceptions.Application;
using SSResurrezioneBR.Models.Exceptions.Infrastructure;
using SSResurrezioneBR.Models.InputModels;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.ImgForCarousel
{
    public class ImgForCarouselService : IImgForCarouselService
    {
        private readonly ISqliteDatabaseAccessor db;
        private readonly ILogger<ImgForCarouselService> logger;
        private readonly IImgPersister imagePersister;
        public ImgForCarouselService(ILogger<ImgForCarouselService> logger, ISqliteDatabaseAccessor db, IImgPersister imagePersister)
        {
            this.logger = logger;
            this.db = db;
            this.imagePersister = imagePersister;
        }
        public async Task<List<ImgForCarouselViewModel>> GetImgForCarouselAsync()
        {
            logger.LogInformation("Chiamato il metodo {0} ", nameof(GetImgForCarouselAsync));
            
            FormattableString query = @$"Select Image_Id, Image_path, Image_label, Image_Content_Title, Image_Visible, Image_Event_Creator from Img_For_Carousel where Image_Visible=1";
            DataSet dataset = await db.QueryAsync(query);
            DataTable dataTable = dataset.Tables[0];

            List<ImgForCarouselViewModel> imgForCarouselList = new ();

            foreach(DataRow imgForCarouselRow in dataTable.Rows){
                ImgForCarouselViewModel imgForCarousel = ImgForCarouselViewModel.FromDataRow(imgForCarouselRow);
                imgForCarouselList.Add(imgForCarousel);
            }
            return imgForCarouselList;
        }

        public async Task<ImgForCarouselDetailViewModel> GetImgForCarouselDetailAsync(long id)
        {
            logger.LogInformation("Chiamato il metodo {0} ", nameof(GetImgForCarouselDetailAsync));

            FormattableString query = @$"Select Image_Id, Image_path, Image_label, Image_Content_Title, Image_Visible, Image_Content_Description, Image_Event_Creator from Img_For_Carousel 
                                where Image_Visible=1 and Image_id = {id}";
            DataSet dataset = await db.QueryAsync(query);
            DataTable dataTable = dataset.Tables[0];
            if(dataTable.Rows.Count != 1){
                throw new InvalidOperationException($"Did not return exactly 1 row for image carousel {id}");
            }
            var imgForCarouselDetailRow = dataTable.Rows[0];
            ImgForCarouselDetailViewModel imgForCarouselDetail = ImgForCarouselDetailViewModel.FromDataRow(imgForCarouselDetailRow);

            return imgForCarouselDetail;
        }

        public async Task<ImgForCarouselDetailViewModel> CreateInitiativeAsync(CreateInitiativeInputModel inputModel)
        {
            string imageContentTitle = inputModel.Image_Content_Title!;
            string eventCreator = "me medesimo";
            string immagineDiDefautl = @"img/IniziativeInCorso/defaultImage.jpg";
            bool immagineVisibile = true;
            string immageLabel = string.Empty;
            string imgageContentDescription = string.Empty;
            
            try
            {
                var dataSet = await db.QueryAsync(@$"Insert into Img_For_Carousel (Image_Content_Title, Image_Event_Creator,Image_Path, Image_Visible, Image_Label, Image_Content_Description) 
                                                    values ({imageContentTitle},{eventCreator},{immagineDiDefautl},{immagineVisibile},{immageLabel},{imgageContentDescription}); Select last_insert_rowid();");
                ;
                int imageId = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                ImgForCarouselDetailViewModel imgForCarousel = await GetImgForCarouselDetailAsync(imageId);
            
                return imgForCarousel;
            } catch (SqliteException exc) when (exc.SqliteErrorCode==19){
                throw new TitleUnavailableException("Iniziativa", inputModel.Image_Content_Title!, exc);
            }
        }
        public async Task<bool> IsTitleAvailableAsync(string titolo, long id){
            bool titleExist = await db.QueryScalarAsync<bool>($"Select count(*) from Img_For_Carousel where Image_Content_Title like {titolo} and Image_Id<>{id} and Image_Visible = 1");
            return !titleExist;
        }

        public async Task<bool> CreateInitiativeIsTitleAvailableAsync (string titolo){
            bool titleExist = await db.QueryScalarAsync<bool>($"Select count(*) from Img_For_Carousel where Image_Content_Title like {titolo}");
            return !titleExist;
        }

        public async Task<ImgForCarouselEditInputModel> GetImgForCarouselForEditingAsync(long id){
            FormattableString query = @$"Select Image_Id, Image_path, Image_label, Image_Content_Title, Image_Visible, Image_Content_Description, Image_Event_Creator, RowVersion from Img_For_Carousel 
                                where Image_Visible=1 and Image_id = {id}";
            DataSet dataSet = await db.QueryAsync(query);
            var imgForCarouselTable = dataSet.Tables[0];
            if(imgForCarouselTable.Rows.Count==0){
                logger.LogWarning($"Attivitá {id} non trovata");
                throw new ImgNotFoundException("Iniziativa",id);
            }
            var imgForCarouselRow = imgForCarouselTable.Rows[0];
            var imgForCarouselEditInputModel = ImgForCarouselEditInputModel.FromDataRow(imgForCarouselRow);
            return imgForCarouselEditInputModel;
        }

        public async Task DeleteInitiativeAsync (long id){
            string eventCreator = "me medesimo";
            
            int affectedRows = await db.CommandAsync($@"UPDATE Img_For_Carousel SET Image_Visible = 0, 
             Image_Event_Creator = {eventCreator} where Image_Id = {id};");
            if (affectedRows == 0)
            {
                throw new ImgNotFoundException("Iniziativa", id);
            }
        }

        public async Task<ImgForCarouselDetailViewModel> EditImgForCarouselAsync(ImgForCarouselEditInputModel inputModel)
        {
            string imagePath = null!;
            try
            {
                if (inputModel.Image != null)
                {
                    imagePath = await imagePersister.SaveImageAsync(inputModel.Image);
                }
                else
                {
                    imagePath =  @"img/IniziativeInCorso/defaultImage.jpg";
                }
                int affectedRows = await db.CommandAsync(@$"Update Img_For_Carousel Set Image_Path = {imagePath}, 
                                                            Image_Content_Title={inputModel.ImageContentTitle},
                                                            Image_Content_Description={inputModel.ImageContentDescription},
                                                            Image_Label={inputModel.ImageLabel},
                                                            Image_Event_Creator = {inputModel.ImageEventCreator},
                                                            Image_Visible = true
                                                            Where Image_Id = {inputModel.ImageId} 
                                                            and RowVersion={inputModel.RowVersion}; ");
                if (affectedRows == 0)
                {
                    bool imgForCarouselExist = await db.QueryScalarAsync<bool>($"Select count(*) from Img_For_Carousel where Image_Id={inputModel.ImageId}");
                    if (imgForCarouselExist)
                    {
                        throw new OptimisticConcurrencyException();
                    }
                    else
                    {
                        throw new ImgNotFoundException("Iniziativa", (long)inputModel.ImageId!);
                    }
                }
            }
            catch(SqliteException exc) when (exc.ErrorCode==19){
                throw new TitleUnavailableException("Iniziativa", inputModel.ImageContentTitle!, exc);
            }
            catch(ImagePersistenceException exc)
            {
                throw new ImgInvalidException(inputModel.Image!, exc);
            }

            ImgForCarouselDetailViewModel imgForCarousel = await GetImgForCarouselDetailAsync((long)inputModel.ImageId!);
            return imgForCarousel;
        }
    }
} 