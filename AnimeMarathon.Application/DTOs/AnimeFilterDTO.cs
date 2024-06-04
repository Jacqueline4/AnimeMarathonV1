using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarathon.Application.DTOs
{
    public class AnimeFilterDTO
    {
        //Anime nombre
        public string? Nombre { get; set; }
        //Año publicacion desde
        public int? Desde { get; set; }
        //Año publicacion hasta
        public int? Hasta{ get; set; }

        public int? GeneroId { get; set; }
        public int? CategoriaId { get; set; }
        public string? Pegi { get; set; } //TODO int
        public string? Subtipo  { get; set; }

        //TOP, SKIP
        
        
    }

    public class PaginationDto
    {

        public int top { get; set; }
        public int skip { get; set; }

    }

    public class ExtendedAnimeFilterDto
    {
        public AnimeFilterDTO Filter { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}
