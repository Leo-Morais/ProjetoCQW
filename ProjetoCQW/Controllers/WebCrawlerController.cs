using Microsoft.AspNetCore.Mvc;
using ProjetoCQW.DTO;
using ProjetoCQW.Service;

namespace ProjetoCQW.Controllers
{
    [ApiController]
    [Route("/api/v1/Crawler")]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IWebCrawlerService _webCrawlerService;

        public WebCrawlerController(IWebCrawlerService webCrawlerService)
        {
            _webCrawlerService = webCrawlerService ?? throw new ArgumentNullException(nameof(webCrawlerService));
        }

        [HttpPut]
        public async Task<IActionResult> Update(int modeloCarroID, int modeloSiteID)
        {
           var crawlerUpdate = await _webCrawlerService.Update(modeloCarroID, modeloSiteID);
            return Ok(crawlerUpdate);
        }
    }
}
