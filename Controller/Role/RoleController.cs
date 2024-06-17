using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.Role
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await unitOfWork.Role.GetAll();
            return Ok(roles);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> Create([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("ten role khong hop le");
            }
            // var roleExist = unitOfWork.Role.GetByName(roleName);
            // if (roleExist != null)
            // {
            //     return Conflict("role da ton tai");
            // }
            var result = await unitOfWork.Role.Create(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return Ok("tao role thanh cong");
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await unitOfWork.Role.GetByID(id);
            if (role == null)
            {
                return BadRequest();
            }
            var result = await unitOfWork.Role.Delete(role);
            if (result.Succeeded)
            {
                return Ok("xoa role thanh cong");
            }
            return BadRequest(result.Errors);
        }
    }
}