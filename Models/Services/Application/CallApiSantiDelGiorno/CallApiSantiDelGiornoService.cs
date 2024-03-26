using System.Data;
using Newtonsoft.Json;
using System.Text;
using SSResurrezioneBR.Models.ViewModel.Home;

namespace SSResurrezioneBR.Models.Services.Application.CallApiSantiDelGiorno
{
    public class CallApiSantiDelGiornoService : ICallApiSantiDelGiornoService
    {
        private readonly IConfiguration _configuration;
        public CallApiSantiDelGiornoService(IConfiguration configuration)
        {
             _configuration = configuration;
        }
        public async Task<string> GetSantiDelGiornoAsync()
        {
            using var httpClient = new HttpClient();
            string UrlSitoSantoDelGiorno = _configuration.GetValue<string>("UrlSitoSantoDelGiorno");

            HttpResponseMessage response = await httpClient.GetAsync(UrlSitoSantoDelGiorno);
                
            response.EnsureSuccessStatusCode(); // Ensure the response is successful
            
            string responseBody = await response.Content.ReadAsStringAsync();
            
            var santiDelGiorno = JsonConvert.DeserializeObject<List<SantiDelGiornoViewModel>>(responseBody);
            
           
            var arrSantiDelGiorno = santiDelGiorno?.Select(santo=>new {Nome=santo.Nome?.Replace(","," e"), Tipologia = santo.Tipologia?.Replace(","," e")});
            var strSantiDelGiorno = string.Join(",", arrSantiDelGiorno!);
            strSantiDelGiorno = new StringBuilder(strSantiDelGiorno)
                                    .Replace("{ Nome = ", "")
                                    .Replace(", Tipologia =","")
                                    .Replace(" }","")
                                    .ToString();

            return strSantiDelGiorno;
        }
    }
}