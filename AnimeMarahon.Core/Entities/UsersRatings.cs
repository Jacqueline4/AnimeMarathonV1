using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("UsuariosValoraciones")]
    public class UsersRatings : BaseEntity
    {
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        [ForeignKey("ValoracionId")]
        public int RatingId { get; set; }
    }
}
