
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class AnimeGenreDTO : BaseDTO
    {
        
        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }
        
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
   
    }
}
