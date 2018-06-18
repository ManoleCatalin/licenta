using System;
using System.Collections.Generic;
using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    //[Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<ReadUserModel>> Get(int? pageIndex, int? pageSize)
        {
            if (pageIndex == null || pageSize == null)
            {
                return UnprocessableEntity();
            }

            var users = _unitOfWork.Users.Get((int)pageIndex, (int)pageSize);
            var readUsers = new List<ReadUserModel>();
            foreach (var user in users)
            {
                readUsers.Add(new ReadUserModel() {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.UserName
                });
            }

            return Ok(readUsers);
        }

        [HttpGet("{id}")]
        public ActionResult<ReadUserModel> GetById(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new ReadUserModel() { Id = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] CreateUserModel user)
        {
            // user.Id = Guid.NewGuid();
            User userFromModel = new User()
            {
                UserName = user.Username,
                Email = user.Email,
                FirstName = user.Email,
                LastName = user.LastName,
                CreatedAt = DateTime.Now
            };

            _unitOfWork.Users.Add(userFromModel);
            _unitOfWork.Complete();

            return new ObjectResult(user) { StatusCode = 201 };
        }
    }
}