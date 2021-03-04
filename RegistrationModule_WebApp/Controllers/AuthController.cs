//using API.Register.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using RegistrationModule_WebApp.Models;
//using RegistrationModule_WebApp.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RegistrationModule_WebApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private IUserService _userService;
//        private IConfiguration _configuration;
//        private IMailService _mailService;

//        public IActionResult Login()
//        {
//            return View();
//        }
//        public AuthController(IUserService userService, IConfiguration configuration, IMailService service)
//        {
//            _userService = userService;
//            _configuration = configuration;
//            _mailService = service;
//        }
//        [HttpPost]
//        public async Task<IActionResult> RegisterEmp([FromBody] RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userService.RegisterUserAsync(model);

//                if (result.IsSuccess)
//                    return Ok(result); // Status Code: 200 

//                return BadRequest(result);
//            }

//            return BadRequest("Some properties are not valid"); // Status code: 400
//        }

//        // /api/auth/login
//        [HttpPost("Login")]
//        public async Task<IActionResult> LoginEmp([FromBody] LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userService.LoginUserAsync(model);

//                if (result.IsSuccess)
//                {
//                    await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");

//                    return Ok(result);
//                }

//                return BadRequest(result);
//            }

//            return BadRequest("Some properties are not valid");
//        }

//        [HttpGet("ConfirmEmail")]
//        public async Task<IActionResult> ConfirmEmail(string userId, string token)
//        {
//            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
//                return NotFound();

//            var result = await _userService.ConfirmEmailAsync(userId, token);

//            if (result.IsSuccess)
//            {
//                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
//            }

//            return BadRequest(result);
//        }

//        [HttpPost("ForgetPassword")]
//        public async Task<IActionResult> ForgetPassword(string email)
//        {
//            if (string.IsNullOrEmpty(email))
//                return NotFound();

//            var result = await _userService.ForgetPasswordAsync(email);

//            if (result.IsSuccess)
//                return Ok(result); // 200

//            return BadRequest(result); // 400
//        }

//        [HttpPost("ResetPass")]
//        public async Task<IActionResult> ResetPass([FromForm] ResetPasswordViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _userService.ResetPasswordAsync(model);

//                if (result.IsSuccess)
//                    return Ok(result);

//                return BadRequest(result);
//            }

//            return BadRequest("Some properties are not valid");
//        }
//    }
//}
