using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.Models.Like;

namespace WebApi.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LikesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateLikeModel like)
        {
            var created = new Like { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, PostId = like.PostId, UserId = like.UserId};
            _unitOfWork.Likes.Add(created);
            _unitOfWork.Complete();
            return new ObjectResult(created) { StatusCode = 201 };
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var likeToDelete = _unitOfWork.Likes.Get(id);
            if (likeToDelete == null)
            {
                return BadRequest();
            }

            _unitOfWork.Likes.Remove(likeToDelete);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}