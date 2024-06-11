using ProjetoCQW.DTO;
using ProjetoCQW.Model;

namespace ProjetoCQW.Service
{
    public interface IModeloSiteDetalheService
    {
        Task<ModeloSiteDetalhe> Add (ModeloSiteDetalheDTO modeloSiteDetalhe);

        Task<ModeloSiteDetalhe> Update (int id, ModeloSiteDetalheDTO modeloSiteDetalhe);

        Task Delete (int id);

        Task<List<ModeloSiteDetalhe>> Get();
    }
}
