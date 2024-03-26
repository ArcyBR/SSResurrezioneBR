using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.Almanacco
{
    public class AlmanaccoViewModel
    {
        public List<AlmanaccoFotoViewModel> AlamanaccoFoto {get; set;}
        public AlmanaccoViewModel()
        {
            AlamanaccoFoto = new List<AlmanaccoFotoViewModel>();
        }
        public int? AlmanaccoId {get; set;}
        public string? AlmanaccoDescrizione {get; set;}
        public string? AlmanaccoTitolo {get; set;}
        public DateTime AlmanaccoDataEvento {get;set;}
        public string? AlmanaccoCreatoreEvento {get;set;}
 
        /*
        public static AlmanaccoViewModel FromDataRow (DataRow almanaccoDetailRow)
        {
            AlmanaccoViewModel almanaccoDetailViewModel = new AlmanaccoViewModel {
                AlmanaccoId = Convert.ToInt32(almanaccoDetailRow["Id_Almanacco"]),
                AlmanaccoDescrizione = (string) almanaccoDetailRow["Descrizione_Almanacco"],
                AlmanaccoTitolo = (string) almanaccoDetailRow["Titolo_Almanacco"],
                AlmanaccoDataEvento = DateOnly.Parse((string) almanaccoDetailRow["Data_Evento_Almanacco"]),
                AlmanaccoCreatoreEvento = (string) almanaccoDetailRow["Creatore_Evento_Almanacco"]
            };
            return almanaccoDetailViewModel;
        }
        */
        public static AlmanaccoViewModel FromEntity (Entities.Almanacco almanacco){
            return new AlmanaccoViewModel {
                    AlmanaccoId = Convert.ToInt32(almanacco.IdAlmanacco),
                    AlmanaccoDescrizione = Convert.ToString(almanacco.DescrizioneAlmanacco),
                    AlmanaccoTitolo = Convert.ToString(almanacco.TitoloAlmanacco),
                    AlmanaccoDataEvento = almanacco.DataEventoAlmanacco,
                    AlmanaccoCreatoreEvento = Convert.ToString(almanacco.CreatoreEventoAlmanacco),
                    AlamanaccoFoto = almanacco.AlmanaccoFotos.Select(almanaccoFoto => new AlmanaccoFotoViewModel{
                        AlmanaccoFotoId = Convert.ToInt32(almanaccoFoto.IdAlmanaccoFoto),
                        AlmanaccoFotoPathFoto = (string) almanaccoFoto.PathFotoAlmanaccoFoto
                    }).ToList()
            };
        }    
    }
}