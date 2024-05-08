using AnimeMarahon.Core.Entities.Base;
using AnimeMarathonV1.DTOs.NewFolder;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeMarathonV1.DTOs
{
    public class AnimeCategoryDTO : BaseDTO
    {
         
            public int CategoryId { get; set; }           
            public int AnimeId { get; set; }
        
    }
}
