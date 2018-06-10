using System;
using System.Collections.Generic;
using Business.Repository;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public ActionResult<List<ReadUserModel>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var readUsers = new List<ReadUserModel>();
            foreach (var user in users)
            {
                readUsers.Add(new ReadUserModel() {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
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
                Username = user.Username,
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
                Username = user.Username,
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