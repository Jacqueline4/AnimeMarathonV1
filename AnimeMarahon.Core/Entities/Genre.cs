using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Generos")]
    public class Genre : BaseEntity
    {
        public Genre()
        {
            AnimesGenre = new HashSet<AnimeGenre>();
        }

        [Column("Nombre")]
        public string? Name { get; set; }

        public virtual IEnumerable<AnimeGenre> AnimesGenre { get; set; }        
       
    }
}
