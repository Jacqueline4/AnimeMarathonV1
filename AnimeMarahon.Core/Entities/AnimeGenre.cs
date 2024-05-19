using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("AnimesGeneros")]
    public class AnimeGenre : BaseEntity
    {
      
        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }
      
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }   

    }
}
