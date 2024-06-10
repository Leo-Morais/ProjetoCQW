using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.Service;
using ProjetoCQW.ViewModel;

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
        public IActionResult Get()
        {
            var modeloCarro = _modeloCarroService.Get();
            return Ok(modeloCarro);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _modeloCarroService.Delete(id);
            return Ok();
        }
    }
}
