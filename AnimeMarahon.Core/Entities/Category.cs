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
        public Category() { 
            AnimesCategory= new HashSet<AnimeCategory>();
        }
        [Column("Nombre")]
        public string? Name { get; set; }

        public virtual IEnumerable<AnimeCategory> AnimesCategory { get; set; }
    }
}
