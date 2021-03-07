using System.Threading.Tasks;
using GreenHealthApi.Endpoints.Medicaments.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenHealthApi.Endpoints.Medicaments
{
    [Route("api/v1/medicaments/")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IMedicamentWriter _writer;
        private readonly IMedicamentReader _reader;

        public Controller(IMedicamentWriter writer, IMedicamentReader reader)
        {
            _writer = writer;
            _reader = reader;
        }
        [Authorize]
        [Route("add/")]
        [HttpPost]
        public async Task<IActionResult> Add(MedicamentAddModel medicamentModel)
        {
            var medicament = await _writer.AddMedicamentAsync(medicamentModel);
            return Ok(medicament);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetMedicament(int id)
        {
            var medicament = await _reader.GetMedicamentAsync(id);
            return Ok(medicament);
        }
        
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllMedicaments()
            {        
            var medicaments = await _reader.GetAllMedicamentsAsync();
            return Ok(medicaments);
        }
        
        [Route("all/")]
        [HttpGet]
        public async Task<IActionResult> AllMedicaments()
        {        
            var medicaments = await _reader.AllMedicamentsAsync();
            return Ok(medicaments);
        }
        
        [Route("hide/{id}")]
        [HttpPut]
        [Authorize]
        public async Task ChangeHide(int id, bool isHide)
        {
            await _writer.ChangeHideVal(isHide, id);
        }
    }
}