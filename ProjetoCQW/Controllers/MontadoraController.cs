using Microsoft.AspNetCore.Mvc;
using ProjetoCQW.Model;
using ProjetoCQW.Service;
using ProjetoCQW.DTO;
using ProjetoCQW.CustomExceptions;

namespace ProjetoCQW.Controllers
{
    [ApiController]
    [Route("api/v1/montadora")]
    public class MontadoraController : ControllerBase
    {
        private readonly IMontadoraService _montadoraService;

        public MontadoraController(IMontadoraService montadoraService)
        {
            _montadoraService = montadoraService ?? throw new ArgumentNullException(nameof(montadoraService));
        }

        //Rota para cadastrar montadora no banco
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] MontadoraDTO montadoraDTO)
        {
            var montadora = await _montadoraService.Add(montadoraDTO);
            return Ok(montadora);
        }

        //Rota para receber os dados
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var montadora = await _montadoraService.Get();
            return Ok(montadora);
        }

        //Rota de atualização
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MontadoraDTO montadoraDTO)
        {
            try
            {
                var updatedMontadora = await _montadoraService.Update(id, montadoraDTO.Name, montadoraDTO.UrlSite);
                return Ok(updatedMontadora);
            }
            catch (IdNotFoundException infe)
            {
                return NotFound(infe.Message);
            }catch (WrongPropertyException ex)
            {
                return BadRequest(ex.Message);
            }
   
        }

        //Rota de delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _montadoraService.Delete(id);
            return Ok();
        }

    }
}
