using System.ComponentModel.DataAnnotations;

namespace SSResurrezioneBR.Models.InputModels
{
    public class DeleteEventoCoroPolifonicoMaterMisericordieInputModel
    {
        [Required]
        public int CoroPolifonicoMaterMisericordieEventId { get; set; }
    }
}
