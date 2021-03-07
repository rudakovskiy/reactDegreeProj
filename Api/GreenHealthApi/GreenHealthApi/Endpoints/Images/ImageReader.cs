using System;
using System.IO;
using System.Threading.Tasks;
using Fixe.Api.Customer.Endpoints.Images.DTO;
using GreenHealthApi.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace GreenHealthApi.Endpoints.Images
{
    public interface IImageReader
    {
        Task<ImageModel> GetImageInfoByNameAsync(string imageName);
    }
    
    public class ImageReader : IImageReader
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConnectionStringGetter _conStr;

        public ImageReader(IWebHostEnvironment hostingEnvironment, IConnectionStringGetter conStr)
        {
            _hostingEnvironment = hostingEnvironment;
            _conStr = conStr;
        }
        public async Task<ImageModel> GetImageInfoByNameAsync(string imageName)
        {
            var path =  Path.Combine( _hostingEnvironment.ContentRootPath, imageName);
            if (File.Exists(path))
            {
                var imageInfo = new FileInfo(path);
                byte[] data = File.ReadAllBytes(path);
                var image = new ImageModel(imageInfo.Name, data, Convert.ToInt32(imageInfo.Length));
                return image;
            }
            else
            {
                throw new ImageDoesNotExistException("There is no current image.");
            }
        }
    }
}