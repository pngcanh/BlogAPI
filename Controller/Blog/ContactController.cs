using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.Blog
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await unitOfWork.Contact.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var contact = await unitOfWork.Contact.GetByIDAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Contact.Add(model);
                await unitOfWork.SaveChangeAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = unitOfWork.Contact.GetByIDAsync(id);
            if (contact != null)
            {
                unitOfWork.Contact.Delete(contact);
                await unitOfWork.SaveChangeAsync();
                return NoContent();
            }
            return NotFound();
        }
    }
}