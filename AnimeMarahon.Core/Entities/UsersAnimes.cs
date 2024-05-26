using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("UsuariosAnimes")]
    public class UsersAnimes : BaseEntity
    {
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        public virtual Anime Anime { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        public virtual User Ususario { get; set; }
        [Column("Estado")]
        public string? Status { get; set; }

        //public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
