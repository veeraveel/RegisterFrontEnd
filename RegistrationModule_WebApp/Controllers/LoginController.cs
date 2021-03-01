using API.Register.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationModule_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationModule_WebApp.Controllers
{
    public class LoginController : Controller
    {

        private IUserService _userService;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public LoginController(IUserService userService, IConfiguration configuration, IMailService service)
        {
            _userService = userService;
            _configuration = configuration;
            _mailService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        // /api/auth/login
        [HttpPost]
        public async Task<IActionResult> LoginEmp([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");

                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}
