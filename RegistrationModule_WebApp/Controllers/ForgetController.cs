using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationModule_WebApp.Models;
using RegistrationModule_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationModule_WebApp.Controllers
{
    public class ForgetController : Controller
    {

        private IUserService _userService;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public ForgetController(IUserService userService, IConfiguration configuration, IMailService service)
        {
            _userService = userService;
            _configuration = configuration;
            _mailService = service;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forgetpass()
        {
            return View();
        }

        public async Task<IActionResult>ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);
   
            if (result.IsSuccess)
                return Ok(result);
            // 200

            return BadRequest(result); // 400
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var model = new ResetPasswordViewModel();
            model.Email = email;
            model.Token = token;

            return View("~/pages/ResetPasswordview.cshtml", model);
        }

        //public IActionResult ConfirmEmailTest(string userId, string token)
        //{
        //    return View();
        //}





        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)

                    return Ok(result);



                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


       






    }
}
