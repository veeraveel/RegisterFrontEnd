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

    }
}
