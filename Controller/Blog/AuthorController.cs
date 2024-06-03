using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.Blog
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Author.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var data = await unitOfWork.Author.GetByIDAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Author.Add(author);
                await unitOfWork.SaveChangeAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Author author, int id)
        {
            if (id != author.ID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            unitOfWork.Author.Update(author);
            await unitOfWork.SaveChangeAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = unitOfWork.Author.GetByID(id);
            if (data == null)
            {
                return NotFound();
            }
            unitOfWork.Author.Delete(data);
            await unitOfWork.SaveChangeAsync();
            return NoContent();
        }

    }
}