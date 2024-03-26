using System.ComponentModel.DataAnnotations;

namespace SSResurrezioneBR.Models.InputModels
{
    public class DeleteEventoAlmanaccoInputModel
    {
        [Required]
        public int AlmanaccoEventId { get; set; }
    }
}
