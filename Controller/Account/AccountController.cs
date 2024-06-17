using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Model;
using BlogAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.Account
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); ;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await unitOfWork.Account.Register(model);

            if (result.Succeeded)
                return Ok();

            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await unitOfWork.Account.Login(model);

            if (string.IsNullOrEmpty(result))
                return Unauthorized();

            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoll(string userID, string roleName)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest("user khong ton tai");
            }
            var result = await unitOfWork.Account.AddRoleAsync(userID, roleName);
            if (!result.Succeeded)
            {
                return BadRequest("fail");
            }
            return Ok("ok");
        }

        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> RemoveRole(string userID, string roleName)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return BadRequest("user khong ton tai");
            }
            var result = await unitOfWork.Account.RemoveRoleAsync(userID, roleName);
            if (!result.Succeeded)
            {
                return BadRequest("fail");
            }
            return Ok("ok");
        }
    }
}