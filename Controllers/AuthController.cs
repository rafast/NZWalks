using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        // POST api/<AuthController>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTOs.LoginRequest loginRequest)
        {
            //Check if user is authenticated
            //Check user and password
            var user = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                //Generate JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest();
        }
    }
}

