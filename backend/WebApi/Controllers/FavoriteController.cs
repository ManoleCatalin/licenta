using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models.Favorite;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FavoritesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateFavoriteModel favorite)
        {
            var created = new Favorite { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, PostId = favorite.PostId, UserId = favorite.UserId };
            _unitOfWork.Favorites.Add(created);
            _unitOfWork.Complete();
            return new ObjectResult(created) { StatusCode = 201 };
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var favToDelete = _unitOfWork.Favorites.Get(id);
            if (favToDelete == null)
            {
                return BadRequest();
            }

            _unitOfWork.Favorites.Remove(favToDelete);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
