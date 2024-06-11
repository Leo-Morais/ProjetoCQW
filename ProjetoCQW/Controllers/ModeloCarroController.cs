using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.Service;
using ProjetoCQW.DTO;

namespace ProjetoCQW.Controllers
{
    [ApiController]
    [Route("/api/v1/ModeloCarro")]
    public class ModeloCarroController : ControllerBase
    {
        private readonly IModeloCarroService _modeloCarroService;

        public ModeloCarroController(IModeloCarroService modeloCarroService)
        {
            _modeloCarroService = modeloCarroService ?? throw new ArgumentNullException(nameof(modeloCarroService));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] ModeloCarroDTO modeloCarroDTO)
        {
            var  carro = await _modeloCarroService.Add(modeloCarroDTO);
            return Ok(carro);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modeloCarro = await _modeloCarroService.Get();
            return Ok(modeloCarro);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModeloCarroDTO modeloCarroDTO)
        {
            var updatedCarro = await _modeloCarroService.Update(id, modeloCarroDTO);
            if (updatedCarro == null)
            {
                return NotFound();
            }
            return Ok(updatedCarro);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _modeloCarroService.Delete(id);
            return Ok();
        }
    }
}
