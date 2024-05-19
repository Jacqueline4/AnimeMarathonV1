using AnimeMarathon.Application.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.DTOs
{
    public class CommentDTO : BaseDTO
    {
        [Column("Fecha_hora")]
        public DateTime DateTime { get; set; }
        [Column("Comentario")]
        public string? CommentString { get; set; }
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }
    }
}
