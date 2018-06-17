using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public InterestsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<List<ReadInterestModel>> Get(int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null)
            {
                return UnprocessableEntity();
            }

            var interests = _unitOfWork.Interests.Get((int)pageIndex, (int)pageSize);
            var readPosts = new List<ReadInterestModel>();
            foreach (var interest in interests)
            {
                readPosts.Add(new ReadInterestModel()
                {
                   Id = interest.Id,
                   Name = interest.Name,
                   ThumbnailImgUrl = interest.ThumbnailImgUrl      
                });
            }

            return Ok(readPosts);
        }


        [HttpPost]
        public ActionResult<Post> Create([FromBody] CreatePostModel post)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }

                var user = _unitOfWork.Users.Get(post.UserId);
            if (null == user)
            {
                return BadRequest();
            }

            var interests = new List<Interest>();
            foreach (var interestGuid in post.Interests)
            {
                var interestById = _unitOfWork.Interests.Get(interestGuid);
                if (interestById == null)
                {
                    return BadRequest();
                }

                interests.Add(interestById);
            }

            var createdPost = new Post()
            {
                CreateAt = user.CreatedAt,
                Description = post.Description,
                User = user,
                Interests = interests,
                SourceUrl = post.SourceUrl,
                Title = post.Title
            };

            return new ObjectResult(post) { StatusCode = 201 };
        }
    }
}