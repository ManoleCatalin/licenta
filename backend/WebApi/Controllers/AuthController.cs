using System;
using System.Threading.Tasks;
using AuthService;
using AuthService.Helpers;
using Core.Domain;
using Data.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Models.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = new User { Id = Guid.NewGuid(),
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = model.Password           
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]LoginModel model)
        {
            var userToVerify = await _userManager.FindByNameAsync(model.UserName);
            if (userToVerify == null)
            {
                return new BadRequestResult();
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, model.Password))
            {
                var identity = _jwtFactory.GenerateClaimsIdentity(userToVerify.UserName, userToVerify.Id);
                if (identity != null)
                {
                    return new OkObjectResult(await Tokens.GenerateJwt(identity, _jwtFactory, new TokenUserModel(userToVerify),
                        _jwtOptions));
                }

            }
            
            return BadRequest("Invalid username or password.");
        }
    }
}