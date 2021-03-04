using API.Register.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using RegistrationModule_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule_WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private IUserService _userService;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public RegisterController(IUserService userService, IConfiguration configuration, IMailService service)
        {
            _userService = userService;
            _configuration = configuration;
            _mailService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmp([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }





        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
                //return RedirectToAction("Reset", "Reset");
            }

            return BadRequest(result);
        }

        public IActionResult ConfirmEmailTest(string userId, string token)
        {
            return View();
        }



    }
}
