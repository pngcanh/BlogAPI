using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.Data;
using BlogAPI.Model;
using BlogAPI.Repository;
using BlogAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controller.Blog
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHost;

        public PostController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await unitOfWork.Post.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var post = await unitOfWork.Post.GetByIDAsync(id);
            return Ok(post);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PostDTO entity)
        {
            var author = await unitOfWork.Author.GetByIDAsync(entity.AuthorID);
            if (author == null)
            {
                return BadRequest("khong tim thay tac gia");
            }

            var category = await unitOfWork.Category.GetByIDAsync(entity.CateID);
            if (category == null)
            {
                return BadRequest("khong tim thay the loai");
            }

            //tao duong dan :thu muc hien tai cua ung dung+wwwroot/uploads
            if (entity.FileUpload != null && entity.FileUpload.Length > 0)
            {
                var uploadFoderPath = Path.Combine(webHost.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFoderPath);

                var file = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName())) + Path.GetExtension(entity.FileUpload.FileName);

                var fileUrl = Path.Combine(uploadFoderPath, file);
                //mo luong doc cho file tai len
                Stream stream = entity.FileUpload.OpenReadStream();
                //nen file
                ResizeAndCompressImage.CompressImage(stream, fileUrl);

                if (ModelState.IsValid)
                {
                    var post = new Post
                    {
                        Content = entity.Content,
                        Title = entity.Title,
                        Slug = GenerateSlug.AutoMapSlug(entity.Title),
                        AuthorID = entity.AuthorID,
                        CateID = entity.CateID,
                        Photo = "/uploads/" + file
                    };
                    unitOfWork.Post.Add(post);
                    await unitOfWork.SaveChangeAsync();
                    return Ok("Tao bai thanh cong");
                }
                return BadRequest(ModelState);
            }
            return BadRequest("file khong hop le");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PostDTO entity)
        {
            var post = await unitOfWork.Post.GetByIDAsync(id);
            if (post == null)
            {
                return BadRequest("Bai viet khong ton tai");
            }
            var author = await unitOfWork.Author.GetByIDAsync(entity.AuthorID);
            if (author == null)
            {
                return BadRequest("khong tim thay tac gia");
            }

            var category = await unitOfWork.Category.GetByIDAsync(entity.CateID);
            if (category == null)
            {
                return BadRequest("khong tim thay the loai");
            }

            //tao duong dan :thu muc hien tai cua ung dung+wwwroot/uploads
            if (entity.FileUpload != null && entity.FileUpload.Length > 0)
            {
                var uploadFoderPath = Path.Combine(webHost.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFoderPath);

                var file = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName())) + Path.GetExtension(entity.FileUpload.FileName);

                var fileUrl = Path.Combine(uploadFoderPath, file);
                //mo luong doc cho file tai len
                Stream stream = entity.FileUpload.OpenReadStream();
                //nen file
                ResizeAndCompressImage.CompressImage(stream, fileUrl);

                if (ModelState.IsValid)
                {


                    post.Content = entity.Content;
                    post.Title = entity.Title;
                    post.Slug = GenerateSlug.AutoMapSlug(entity.Title);
                    post.AuthorID = entity.AuthorID;
                    post.CateID = entity.CateID;
                    post.Photo = "/uploads/" + file;

                    unitOfWork.Post.Update(post);
                    await unitOfWork.SaveChangeAsync();
                    return Ok("Cap nhat bai viet thanh cong");
                }
                return BadRequest(ModelState);
            }
            return BadRequest("file khong hop le");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await unitOfWork.Post.GetByIDAsync(id);
            if (post != null)
            {
                unitOfWork.Post.Delete(post);
                await unitOfWork.SaveChangeAsync();
                return NoContent();
            }
            return BadRequest("bai viet khong ton tai");
        }

    }
}