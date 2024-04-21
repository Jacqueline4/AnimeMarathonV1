using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("UsuariosComentarios")]
    public class UsersComments : BaseEntity
    {
        [ForeignKey("UsuarioId")]
        public int UserId { get; set; }
        [ForeignKey("ComentarioId")]
        public int CommentId { get; set; }
    }
}
