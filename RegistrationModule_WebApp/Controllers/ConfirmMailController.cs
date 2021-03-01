using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationModule_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationModule_WebApp.Controllers
{
    public class ConfirmMailController : Controller
    {


        private IUserService _userService;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public ConfirmMailController(IUserService userService, IConfiguration configuration, IMailService service)
        {
            _userService = userService;
            _configuration = configuration;
            _mailService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Confirm()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }

            return BadRequest(result);
        }

        //[HttpPost("ForgetPassword")]
        //public async Task<IActionResult> ForgetPassword(string email)
        //{
        //    if (string.IsNullOrEmpty(email))
        //        return NotFound();

        //    var result = await _userService.ForgetPasswordAsync(email);

        //    if (result.IsSuccess)
        //        return Ok(result); // 200

        //    return BadRequest(result); // 400
        //}

        //[HttpPost("ResetPassword")]
        //public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userService.ResetPasswordAsync(model);

        //        if (result.IsSuccess)
        //            return Ok(result);

        //        return BadRequest(result);
        //    }

        //    return BadRequest("Some properties are not valid");
        //}

    }
}
