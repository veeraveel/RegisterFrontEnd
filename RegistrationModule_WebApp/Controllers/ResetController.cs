﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RegistrationModule_WebApp.Models;
using RegistrationModule_WebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationModule_WebApp.Controllers
{
    public class ResetController : Controller
    {

        private IUserService _userService;
        private IConfiguration _configuration;
        private IMailService _mailService;
        public ResetController(IUserService userService, IConfiguration configuration, IMailService service)
        {
            _userService = userService;
            _configuration = configuration;
            _mailService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reset()
        {
            return View();
        }

        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);

                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/Reset.html");
            }

            return BadRequest(result);
        }

        public IActionResult ConfirmEmailTest(string userId, string token)
        {
            return View();
        }
    }
}
