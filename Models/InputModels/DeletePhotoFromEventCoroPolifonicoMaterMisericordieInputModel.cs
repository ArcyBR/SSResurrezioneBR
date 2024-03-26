using System.ComponentModel.DataAnnotations;

namespace SSResurrezioneBR.Models.InputModels
{
    public class DeletePhotoFromEventCoroPolifonicoMaterMisericordieInputModel
    {
        [Required]
        public int CoroPolifonicoMaterMisericordiePhotoId {  get; set; }
        public int CoroPolifonicoMaterMisericordieEventId { get; set; }
        public string CoroPolifonicoMaterMisericordieEventTitle { get; set; }
    }
}
