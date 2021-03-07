using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Fixe.Api.Customer.Endpoints.Images.DTO;
using GreenHealthApi.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GreenHealthApi.Endpoints.Images
{
    public interface IImageWriter
    {
        Task<string> UpdateMedicamentImageAsync(string medicamentName, IFormFile image);
    }

    public class ImageWriter : IImageWriter
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConnectionStringGetter _conStrGetter;
        private readonly IStaticContentRootUrlGetter _contentRoot;

        public ImageWriter(IWebHostEnvironment hostingEnvironment, IConnectionStringGetter conStrGetter,
            IStaticContentRootUrlGetter staticContentRootUrlGetter)
        {
            _hostingEnvironment = hostingEnvironment;
            _conStrGetter = conStrGetter;
            _contentRoot = staticContentRootUrlGetter;
        }

        public async Task<string> UpdateMedicamentImageAsync(string medicamentName, IFormFile image)
        {
            string imageName;
            
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string secureKey = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());


            if (image.FileName.Contains(".jpeg"))
            {
                imageName = $"{medicamentName}_medicament_img.jpeg";
            }
            else if (image.FileName.Contains(".png"))
            {
                imageName = $"{medicamentName}_medicament_img.png";
            }
            else if (image.FileName.Contains(".jpg"))
            {
                imageName = $"{medicamentName}_medicament_img.jpg";
            }
            else
            {
                throw new WrongFileTypeException("There is a wrong type for image");
            }

            var folderPath = _hostingEnvironment.ContentRootPath;
            var imagePath = Path.Combine(folderPath, imageName);

            using var fileStream = new FileStream(imagePath, FileMode.Create);
            
                await image.CopyToAsync(fileStream);

                using (var con = new SqlConnection(_conStrGetter.Get()))
                {
                    try
                    {
                        con.Open();
                        var addImageQuery =
                            @"USE GreenHealth;
                              UPDATE Medicaments SET Medicaments.ImageName = @imageName 
                              WHERE Medicaments.Name = @name;";
                        await con.QueryFirstOrDefaultAsync(addImageQuery,
                            new {imageName = imageName, name = medicamentName});

                        var imageUrl = _contentRoot.Get() + @"/" + imageName;
                        return imageUrl;
                    }                    
                    catch (Exception e)
                    {
                        throw new BadRequestException(e.Message);
                    }
                }
        }
    }
}