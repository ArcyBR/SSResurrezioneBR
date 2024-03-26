using System.Data;

namespace SSResurrezioneBR.Models.ViewModel.Incarichi
{
    public class IncaricoViewModel
    {
        public int? Id {get; set;}
        public string? Cognome {get; set;}
        public string? Titolo { get; set; }
        public string? Nome { get; set; }
        public string? Incarico { get; set; }
        public string? LinkDiocesi { get; set; }

        public static IncaricoViewModel FromDataRow(DataRow incarichiRow)
        {
           IncaricoViewModel incaricoViewModel = new IncaricoViewModel {
                Id = Convert.ToInt32(incarichiRow["Id"]),
                Cognome = (string)incarichiRow["Cognome"],
                Titolo = (string) incarichiRow["Titolo"],
                Nome = (string) incarichiRow["Nome"],
                Incarico = (string) incarichiRow["Incarico"],
                LinkDiocesi = (string) incarichiRow["Link_Diocesi"]
           };
           return incaricoViewModel;
        }
    }
}