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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await unitOfWork.Category.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var category = await unitOfWork.Category.GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(model);
                await unitOfWork.SaveChangeAsync();
                return CreatedAtAction(nameof(GetByID), new { id = model.ID }, model);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Category model, int id)
        {
            if (model.ID != id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            unitOfWork.Category.Update(model);
            await unitOfWork.SaveChangeAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await unitOfWork.Category.GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Delete(category);
            await unitOfWork.SaveChangeAsync();
            return NoContent();
        }

    }
}