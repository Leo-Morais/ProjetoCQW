using ProjetoCQW.DTO;
using ProjetoCQW.Model;

namespace ProjetoCQW.Service
{
    public interface IWebCrawlerService
    {
        Task<ModeloCarro> Update(int modeloCarroID, int ModeloSiteID);
    }
}
