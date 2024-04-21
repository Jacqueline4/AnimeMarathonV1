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
        [Column("Comentario")]
        public string? CommentString { get; set; }

    }
}
