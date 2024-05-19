using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("AnimesCategorias")]
    public class AnimeCategory : BaseEntity
    {
        [ForeignKey("CategoriaId")]
        public int CategoriaId { get; set; }
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }
    }
}
