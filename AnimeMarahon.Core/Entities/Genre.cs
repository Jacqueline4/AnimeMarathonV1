using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Generos")]
    public class Genre : BaseEntity
    {
        [Column("Nombre")]
        public string? Name { get; set; }
      

    }
}
