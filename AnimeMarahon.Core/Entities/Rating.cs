using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Valoraciones")]
    public class Rating : BaseEntity
    {
        [Column("Valoracion")]
        public decimal? RatingVal { get; set; }

        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }

        public virtual  Anime Anime { get; set; }

        [Column("UsuarioId")]
        public int UserId { get; set; }


        public virtual User User { get; set; }
    }
}
