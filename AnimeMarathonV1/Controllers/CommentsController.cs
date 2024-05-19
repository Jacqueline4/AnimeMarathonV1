using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Interfaces.Base;
using AnimeMarathon.Application.Services.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeMarathonV1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBaseServices<CommentDTO, Comment> baseServices;
        public CommentsController(IMapper mapper, IBaseServices<CommentDTO, Comment> baseServices)
        {
            this.mapper = mapper;
            this.baseServices = baseServices;
        }
        [HttpGet]
        public async Task<IEnumerable<CommentDTO>> Get()
        {
            var list = await baseServices.GetList();
            var mapped = mapper.Map<IEnumerable<CommentDTO>>(list);
            return mapped;
        }

        [HttpPost]
        public async Task<CommentDTO> CreateComment(CommentDTO commentViewModel)
        {
            commentViewModel.DateTime = DateTime.Now;
            var mapped = mapper.Map<Comment>(commentViewModel);
            if (mapped == null)
                throw new Exception($"Entity could not be mapped.");

            var entityDto = await baseServices.Create(commentViewModel);

            var mappedViewModel = mapper.Map<CommentDTO>(entityDto);
            return mappedViewModel;
        }


        [HttpDelete]
        public async Task DeleteAnimeGenre(int animeGenreId)
        {
            await baseServices.Delete(animeGenreId);
        }
    }
}
