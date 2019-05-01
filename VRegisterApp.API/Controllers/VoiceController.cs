using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VRegisterApp.API.DTO.Login;
using VRegisterApp.API.DTO.Register;
using VRegisterApp.API.Services.Abstract;

namespace VRegisterApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoiceController : ControllerBase
    {
        private readonly IVoiceService _voiceService;
        public VoiceController(IVoiceService voiceService)
        {
            _voiceService = voiceService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request Empty");
            }

            var result = await _voiceService.RegisterUser(request);

            if (result)
            {
                return Ok("Register successful");
            }
            else
            {
                return BadRequest("Registration failed");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request Empty");
            }

            var result = await _voiceService.LoginUser(request);

            if (result)
            {
                return Ok("Login successful");
            }
            else
            {
                return BadRequest("Login failed");
            }
        }

        [HttpGet("getContext/{email}")]
        public async Task<IActionResult> GetContext(string email)
        {
            var context = await _voiceService.GetUserContext(email);

            if (context != "")
            {
                return Ok(context);
            }
            else
            {
                return BadRequest("User does not exists!");
            }
        }
    }
}
