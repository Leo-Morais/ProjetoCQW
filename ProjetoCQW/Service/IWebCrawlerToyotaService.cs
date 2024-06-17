using ProjetoCQW.DTO;
using ProjetoCQW.Model;

namespace ProjetoCQW.Service
{
    public interface IWebCrawlerToyotaService
    {
        Task<ModeloCarro> Update(int modeloCarroID, int ModeloSiteID);

        Task<ModeloCarro> UpdateXS(int modeloCarroID, int ModeloSiteID);
    }
}
