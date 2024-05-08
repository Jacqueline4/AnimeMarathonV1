﻿using AnimeMarahon.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeMarahon.Core.Entities
{
    [Table("AnimesGeneros")]
    public class AnimeGenre : BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }
        [Key]
        [Column(Order = 2)]
        [ForeignKey("AnimeId")]
        public int AnimeId { get; set; }

        //// Agrega propiedades de navegación si es necesario
        //public virtual Anime Anime { get; set; }
        //public virtual Genre Genre { get; set; }

    }
}
