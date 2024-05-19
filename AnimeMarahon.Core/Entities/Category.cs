using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Categorias")]
    public class Category: BaseEntity
    {
        [Column("Nombre")]
        public string? Name { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }
    }
}
