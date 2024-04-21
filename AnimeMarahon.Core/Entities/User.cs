using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("Usuarios")]
    public class User : BaseEntity
    {
        [Column("Nombre")]
        public string Name { get; set; }
        [Column("Apellidos")]
        public string? LastName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Contraseña")]
        public string? Password { get; set; }
        
    }
}
