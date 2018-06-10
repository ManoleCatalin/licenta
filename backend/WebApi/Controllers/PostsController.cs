using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Post;

namespace WebApi.Controllers
{
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
        public ActionResult<List<ReadPostModel>> GetAll()
        {
            var posts = _unitOfWork.Posts.GetAll();
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
                    UserId = post.Author.Id
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
                Author = user,
                Interests = interests,
                SourceUrl = post.SourceUrl,
                Title = post.Title
            };

            return new ObjectResult(post) { StatusCode = 201 };
        }
    }
}