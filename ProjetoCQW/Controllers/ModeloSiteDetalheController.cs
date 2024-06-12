using Microsoft.AspNetCore.Mvc;
using ProjetoCQW.CustomExceptions;
using ProjetoCQW.DTO;
using ProjetoCQW.Service;

namespace ProjetoCQW.Controllers
{
    [ApiController]
    [Route("/api/v1/ModeloSiteDetalhe")]
    public class ModeloSiteDetalheController : ControllerBase
    {
        private readonly IModeloSiteDetalheService _modeloSiteDetalheService;

        public ModeloSiteDetalheController(IModeloSiteDetalheService modeloSiteDetalheService)
        {
            _modeloSiteDetalheService = modeloSiteDetalheService ?? throw new ArgumentNullException(nameof(modeloSiteDetalheService)); ;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ModeloSiteDetalheDTO modeloSiteDetalheDTO)
        {
            var modelo = await _modeloSiteDetalheService.Add(modeloSiteDetalheDTO);
            return Ok(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelo = await _modeloSiteDetalheService.Get();
            return Ok(modelo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModeloSiteDetalheDTO modeloSiteDetalheDTO)
        {
            try
            {
                var updateModelo = await _modeloSiteDetalheService.Update(id, modeloSiteDetalheDTO);
                return Ok(updateModelo);

            }
            catch (IdNotFoundException infe)
            {
                return NotFound(infe.Message);
            }
            catch (WrongPropertyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _modeloSiteDetalheService.Delete(id);
            return Ok();
        }

    }
}