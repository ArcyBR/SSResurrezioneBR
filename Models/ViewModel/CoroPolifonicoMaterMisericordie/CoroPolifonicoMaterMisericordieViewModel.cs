using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSResurrezioneBR.Models.ViewModel.CoroPolifonicoMaterMisericordie
{
    public class CoroPolifonicoMaterMisericordieViewModel
    {
        public List<CoroPolifonicoMaterMisericordieFotoViewModel> CoroPolifonicoMaterMisericordieFoto {get; set;}
        public CoroPolifonicoMaterMisericordieViewModel()
        {
            CoroPolifonicoMaterMisericordieFoto = new List<CoroPolifonicoMaterMisericordieFotoViewModel>();
        }
        public int? CoroPolifonicoMaterMisericordieId {get; set;}
        public string? CoroPolifonicoMaterMisericordieDescrizione {get; set;}
        public string? CoroPolifonicoMaterMisericordieTitolo {get; set;}
        public DateTime CoroPolifonicoMaterMisericordieDataEvento {get;set;}
        public string? CoroPolifonicoMaterMisericordieCreatoreEvento {get;set;}
        
        /*
        public static CoroPolifonicoMaterMisericordieViewModel FromDataRow (DataRow coroPolifonicoMaterMisericordieDetailRow)
        {
            CoroPolifonicoMaterMisericordieViewModel coroPolifonicoMaterMisericordieViewModel = new CoroPolifonicoMaterMisericordieViewModel {
                CoroPolifonicoMaterMisericordieId = Convert.ToInt32(coroPolifonicoMaterMisericordieDetailRow["Id_CoroPolifonicoMaterMisericordie"]),
                CoroPolifonicoMaterMisericordieDescrizione = (string) coroPolifonicoMaterMisericordieDetailRow["Descrizione_CoroPolifonicoMaterMisericordie"],
                CoroPolifonicoMaterMisericordieTitolo = (string) coroPolifonicoMaterMisericordieDetailRow["Titolo_CoroPolifonicoMaterMisericordie"],
                CoroPolifonicoMaterMisericordieDataEvento = DateOnly.Parse((string) coroPolifonicoMaterMisericordieDetailRow["Data_Evento_CoroPolifonicoMaterMisericordie"]),
                CoroPolifonicoMaterMisericordieCreatoreEvento = (string) coroPolifonicoMaterMisericordieDetailRow["Creatore_Evento_CoroPolifonicoMaterMisericordie"]
            };
            return coroPolifonicoMaterMisericordieViewModel;
        }
        */
        public static CoroPolifonicoMaterMisericordieViewModel FromEntity (Entities.CoroPolifonicoMaterMisericordie coroPolifonicoMaterMisericordie){
            return new CoroPolifonicoMaterMisericordieViewModel {
                    CoroPolifonicoMaterMisericordieId = Convert.ToInt32(coroPolifonicoMaterMisericordie.IdCoroPolifonicoMaterMisericordie),
                    CoroPolifonicoMaterMisericordieDescrizione = Convert.ToString(coroPolifonicoMaterMisericordie.DescrizioneEventoCoroPolifonicoMaterMisericordie),
                    CoroPolifonicoMaterMisericordieTitolo = Convert.ToString(coroPolifonicoMaterMisericordie.TitoloCoroPolifonicoMaterMisericordie),
                    CoroPolifonicoMaterMisericordieDataEvento = coroPolifonicoMaterMisericordie.DataEventoCoroPolifonicoMaterMisericordie,
                    CoroPolifonicoMaterMisericordieFoto = coroPolifonicoMaterMisericordie.CoroPolifonicoMaterMisericordieFotos.Select(coroPolifonicoMaterMisericordieFoto => new CoroPolifonicoMaterMisericordieFotoViewModel{
                        CoroPolifonicoMaterMisericordieFotoId = Convert.ToInt32(coroPolifonicoMaterMisericordieFoto.IdCoroPolifonicoMaterMisericordieFoto),
                        CoroPolifonicoMaterMisericordieFotoPathFoto = (string) coroPolifonicoMaterMisericordieFoto.PathFotoCoroPolifonicoMaterMisericordieFoto
                    }).ToList()
            };
        }    
    }
}