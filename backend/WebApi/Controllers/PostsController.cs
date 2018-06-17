using System.Collections.Generic;
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Post;

namespace WebApi.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<List<ReadPostModel>> Get(int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null)
            {
                return UnprocessableEntity();
            }

            var posts = _unitOfWork.Posts.Get((int)pageIndex, (int)pageSize);
            var readPosts = new List<ReadPostModel>();
            foreach (var post in posts)
            {
                readPosts.Add(new ReadPostModel()
                {
                    Id = post.Id,
                    CreatedAt = post.CreateAt,
                    Description = post.Description,
                    SourceUrl = post.SourceUrl,
                    Title = post.Title,
                    UserId = post.User.Id
                });
            }

            return Ok(readPosts);
        }


        [HttpPost]
        public ActionResult<Post> Create([FromBody] CreatePostModel post)
        {
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