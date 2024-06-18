using Microsoft.AspNetCore.Mvc;
using ProjetoCQW.DTO;
using ProjetoCQW.Service;

namespace ProjetoCQW.Controllers
{
    [ApiController]
    [Route("/api/v1/Crawler")]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IWebCrawlerToyotaService _webCrawlerToyotaService;

        public WebCrawlerController(IWebCrawlerToyotaService webCrawlerToyotaService)
        {
            _webCrawlerToyotaService = webCrawlerToyotaService ?? throw new ArgumentNullException(nameof(webCrawlerToyotaService));
        }
     
        [HttpPut]

        public async Task<IActionResult> Update(int modeloCarroID, int modeloSiteID)
        {
           var crawlerUpdate = await _webCrawlerToyotaService.Update(modeloCarroID, modeloSiteID);
            return Ok(crawlerUpdate);
        }
      
    }
}
