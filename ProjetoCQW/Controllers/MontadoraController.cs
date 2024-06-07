using Microsoft.AspNetCore.Mvc;
using ProjetoCQW.Model;
using ProjetoCQW.Service;
using ProjetoCQW.ViewModel;

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
        public IActionResult Add([FromForm] MontadoraViewModel montadoraView)
        {
            var montadora = new Montadora(montadoraView.Name, montadoraView.UrlSite);
            _montadoraService.Add(montadora);
            return Ok();
        }

        //Rota para receber os dados
        [HttpGet]
        public IActionResult Get()
        {
            var montadora = _montadoraService.Get();
            return Ok(montadora);
        }

        //Rota de atualização
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MontadoraViewModel montadoraView)
        {
            var updatedMontadora = _montadoraService.Update(id, montadoraView.Name, montadoraView.UrlSite);
            if (updatedMontadora == null)
            {
                return NotFound();
            }
            return Ok(updatedMontadora);
        }

        //Rota de delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _montadoraService.Delete(id);
            return Ok();
        }

    }
}
