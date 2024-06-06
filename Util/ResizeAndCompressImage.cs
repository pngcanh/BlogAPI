using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace BlogAPI.Util
{
    public class ResizeAndCompressImage
    {
        // public static string ImageNewSize(Image image, int maxWidth, int maxHeight)
        // {
        //     if (image.Width > maxWidth || image.Height > maxHeight)
        //     {
        //         double widthRatio = (double)image.Width / (double)maxWidth;
        //         double heightRatio = (double)image.Height / (double)maxHeight;
        //         double ratio = Math.Max(widthRatio, heightRatio);
        //         int newWidth = (int)(image.Width / ratio);
        //         int newHeight = (int)(image.Height / ratio);
        //         return newHeight.ToString() + "," + newWidth.ToString();
        //     }
        //     else
        //     {
        //         return image.Height.ToString() + "," + image.Width.ToString();
        //     }
        // }
        // public static string CompressImage(IFormFile file)
        // {
        //     string fileName = string.Empty;
        //     string path = string.Empty;
        //     if (file.Length > 0 && file != null)
        //     {
        //         fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        //         path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload"));
        //         string fullPath = Path.Combine(path, fileName);
        //         using (var image = Image.Load(file.OpenReadStream()))
        //         {
        //             string newSize = ImageNewSize(image, 600, 600);
        //             string[] sizeArr = newSize.Split(',');
        //             image.Mutate(h => h.Resize(Convert.ToInt32(sizeArr[1]), Convert.ToInt32(sizeArr[0])));
        //             image.Save(fullPath);
        //             return fullPath;
        //         }

        //     }
        //     return "file tai len khog hop le";
        // }
        public static void CompressImage(Stream srcImgStream, string targetPath)
        {
            try
            {
                // Convert stream to image
                using var image = System.Drawing.Image.FromStream(srcImgStream);

                float maxHeight = 900.0f;
                float maxWidth = 900.0f;
                int newWidth;
                int newHeight;

                var originalBMP = new Bitmap(srcImgStream);
                int originalWidth = originalBMP.Width;
                int originalHeight = originalBMP.Height;

                if (originalWidth > maxWidth || originalHeight > maxHeight)
                {
                    // To preserve the aspect ratio  
                    float ratioX = (float)maxWidth / (float)originalWidth;
                    float ratioY = (float)maxHeight / (float)originalHeight;
                    float ratio = Math.Min(ratioX, ratioY);
                    newWidth = (int)(originalWidth * ratio);
                    newHeight = (int)(originalHeight * ratio);
                }

                else
                {
                    newWidth = (int)originalWidth;
                    newHeight = (int)originalHeight;
                }

                var bitmap = new Bitmap(originalBMP, newWidth, newHeight);
                var imgGraph = Graphics.FromImage(bitmap);

                imgGraph.SmoothingMode = SmoothingMode.Default;
                imgGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                imgGraph.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                var extension = Path.GetExtension(targetPath).ToLower();
                // for file extension having png and gif
                if (extension == ".png" || extension == ".gif")
                {
                    // Save image to targetPath
                    bitmap.Save(targetPath, image.RawFormat);
                }

                // for file extension having .jpg or .jpeg
                else if (extension == ".jpg" || extension == ".jpeg")
                {
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    Encoder myEncoder = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    var parameter = new EncoderParameter(myEncoder, 50L);
                    encoderParameters.Param[0] = parameter;

                    // Save image to targetPath
                    bitmap.Save(targetPath, jpgEncoder, encoderParameters);
                }
                bitmap.Dispose();
                imgGraph.Dispose();
                originalBMP.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }

}

