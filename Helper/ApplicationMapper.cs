using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Model;

namespace BlogAPI.Helper
{
    public class ApplicationMapper : Profile
    {
        private readonly IWebHostEnvironment webHost;

        public ApplicationMapper(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            CreateMap<PostModel, Post>().ForMember(dest => dest.Photo, opt => opt.MapFrom(src => ConvertIFormFileToString(src.FileUpload)));
        }
        private string ConvertIFormFileToString(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                var uploadFoderPath = Path.Combine(webHost.WebRootPath, "uploads"); // tao duong dan
                Directory.CreateDirectory(uploadFoderPath); // tao thu muc moi tai duong dan 

                var filePath = Path.GetFileNameWithoutExtension(Path.GetFileName(Path.GetRandomFileName()))
                                    + Path.GetExtension(file.FileName);

                var fullPath = Path.Combine(uploadFoderPath, filePath);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }

                var photo = "/uploads/" + filePath;
                return photo;
            }
            return null;
        }
    }
}