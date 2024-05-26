using AnimeMarahon.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathon.Application.Services.DTOs
{
    public class UserDTO : BaseDTO
    {


        [Column("Nombre")]
        public string Name { get; set; }
        [Column("Apellidos")]
        public string? LastName { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("Contraseña")]
        public string? Password { get; set; }

    }
}
