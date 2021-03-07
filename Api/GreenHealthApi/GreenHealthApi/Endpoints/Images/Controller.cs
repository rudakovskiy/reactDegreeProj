using System.Net;
using System.Threading.Tasks;
using Fixe.Api.Customer.Endpoints.Images;
using Fixe.Api.Customer.Endpoints.Images.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealthApi.Endpoints.Images
{   

    [Route("api/v1/images")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IImageWriter _writer;
        private readonly IImageReader _reader;

        public Controller(IImageWriter writer, IImageReader reader)
        {
            _writer = writer;
            _reader = reader;
        }
        
        [HttpPut]
        [Route("updateMedicamentImage/{medicamentName}")]
        public async Task<IActionResult> UpdateMedicamentImageByName( 
            [FromForm(Name="image")] IFormFile image, [FromRoute] string medicamentName)
        {    
            var imageName = await _writer.UpdateMedicamentImageAsync(medicamentName, image);
            return Ok();
        }
        
        /*[HttpGet]
        [Route("images/imageInfo/{imageName}")]
        public async Task<IActionResult> GetImageInfoByName(
            string imageName)
        {
            var image = await _reader.GetImageInfoByNameAsync(imageName);
            return Ok(image);
        }*/
    }
}