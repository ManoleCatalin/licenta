using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using Core.Ordering;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Post;

namespace WebApi.Controllers
{
    // [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PostsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[HttpGet("interestId")]
        //public ActionResult<List<ReadPostModel>> GetPostsForInterest(, int? pageIndex, int? pageSize)
        //{
        //    var posts = _unitOfWork.GetPostsForInterest(interestId, (int)pageIndex, (int)pageSize).ToList();

        //    var readPosts = _mapper.Map<List<ReadPostModel>>(posts);
        //    for (int i = 0; i < readPosts.Count; i++)
        //    {
        //        readPosts[i].Interests = _mapper.Map<List<ReadInterestModel>>(posts[i].PostInterests);
        //        readPosts[i].LikeId = _unitOfWork.Likes.GetLikeIdOfPostForUser(posts[i].Id, userId);
        //        readPosts[i].FavoriteId = _unitOfWork.Favorites.GetFavoriteIdOfPostForUser(posts[i].Id, userId);
        //    }

        //    return Ok(readPosts);
        //}

        [HttpGet]
        public ActionResult<List<ReadPostModel>> Get(Guid userId, bool selfPosts, int? pageIndex, int? pageSize, string orderBy, Guid? interestId)
        {
            if (pageIndex == null || pageSize == null)
            {
                return UnprocessableEntity();
            }

            Ordering<Post> ordering = null;
            if (orderBy != null)
            {
                if (orderBy.Equals("popularity"))
                {
                    ordering = Ordering<Post>.CreateDesc(p => p.Likes.Count);
                }
                else if (orderBy.Equals("freshness"))
                {
                    ordering = Ordering<Post>.CreateDesc(p => p.CreatedAt);
                }
            }

            if (null == ordering && selfPosts)
            {
                ordering = Ordering<Post>.CreateDesc(p => p.CreatedAt);
            }

            var posts = _unitOfWork.GetPostsForUser(userId, selfPosts, (int)pageIndex, (int)pageSize, ordering, interestId).ToList();

            if (orderBy!= null && orderBy.Equals("favorite"))
            {
                posts = _unitOfWork.Posts.GetFavoritePosts(userId, (int)pageIndex, (int)pageSize).ToList();
            }

            var readPosts = _mapper.Map<List<ReadPostModel>>(posts);
            for (int i = 0; i < readPosts.Count; i++)
            {
                readPosts[i].Interests = _mapper.Map<List<ReadInterestModel>>(posts[i].PostInterests);
                readPosts[i].LikeId = _unitOfWork.Likes.GetLikeIdOfPostForUser(posts[i].Id, userId);
                readPosts[i].FavoriteId = _unitOfWork.Favorites.GetFavoriteIdOfPostForUser(posts[i].Id, userId);
                readPosts[i].LikesCount = _unitOfWork.Posts.GetLikesCountOfPost(posts[i].Id);
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

            var createdPost = new Post()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Description = post.Description,
                User = user,
                SourceUrl = post.SourceUrl,
                Title = post.Title
            };

            var interests = new List<PostInterest>();
            foreach (var interestGuid in post.Interests)
            {
                var interestById = _unitOfWork.Interests.Get(interestGuid);
                if (interestById == null)
                {
                    return BadRequest();
                }

                interests.Add(new PostInterest { Interest = interestById, Post = createdPost });
            }

            _unitOfWork.PostInterests.AddRange(interests);
            _unitOfWork.Complete();

            return new ObjectResult(post) { StatusCode = 201 };
        }

        [HttpPut]
        public ActionResult<Post> Edit([FromBody] EditPostModel post)
        {
            var existingPost = _unitOfWork.Posts.Get(post.Id);
            if (null == existingPost)
            {
                return BadRequest();
            }

            existingPost.Description = post.Description;
            existingPost.SourceUrl = post.SourceUrl;
            existingPost.Title = post.Title;

            _unitOfWork.PostInterests.RemoveRange(existingPost.PostInterests);
            _unitOfWork.Complete();

            existingPost.PostInterests = new List<PostInterest>();

            foreach (var interestGuid in post.Interests)
            {
                var interestById = _unitOfWork.Interests.Get(interestGuid);
                if (interestById == null)
                {
                    return BadRequest();
                }

                existingPost.PostInterests.Add(new PostInterest { Interest = interestById, Post = existingPost });
            }

            _unitOfWork.PostInterests.AddRange(existingPost.PostInterests);
            _unitOfWork.Complete();

            return new ObjectResult(existingPost) { StatusCode = 201 };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var postToDelete = _unitOfWork.Posts.Get(id);
            if (postToDelete == null)
            {
                return BadRequest();    
            }

            _unitOfWork.Posts.Remove(postToDelete);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}