using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}