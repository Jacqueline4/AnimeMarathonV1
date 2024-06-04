using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Comentarios")]
    public class Comment : BaseEntity
    {
        [Column("Fecha_hora")]
        public DateTime DateTime { get; set; }
        [Column("Comentario")]
        public string? CommentString { get; set; }
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        public virtual Anime Anime { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
        public virtual User User { get; set; }
    }
}
