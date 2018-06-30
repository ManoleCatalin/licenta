using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    //[Authorize(Policy = "ApiUser")]
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

        [HttpGet("{userId}")]
        public ActionResult<List<ReadInterestModel>> Get(Guid userId)
        {
            var interests = _unitOfWork.Interests.GetAll(userId);
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

        [HttpPost("{userId}/{interestId}")]
        public ActionResult<List<ReadInterestModel>> AddInterest(Guid userId, Guid interestId)
        {
            _unitOfWork.UserInterests.Add(new Core.Domain.UserInterest { InterestId = interestId, UserId = userId});
            _unitOfWork.Complete();
           
            return Ok();
        }

        [HttpDelete("{userId}/{interestId}")]
        public ActionResult<List<ReadInterestModel>> DeleteInterest(Guid userId, Guid interestId)
        {
            _unitOfWork.UserInterests.Remove(new Core.Domain.UserInterest { InterestId = interestId, UserId = userId });
            _unitOfWork.Complete();

            return NoContent();
        }
    }
}