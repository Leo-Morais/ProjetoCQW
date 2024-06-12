using ProjetoCQW.Model;
using ProjetoCQW.DTO;

namespace ProjetoCQW.Service
{
    public interface IModeloCarroService
    {
        Task<ModeloCarro> Add(ModeloCarroDTO modeloCarro);

        Task Delete(int id);

        Task<ModeloCarro> Update(int id, ModeloCarroDTO modeloCarro);

        Task<List<ModeloCarro>> Get();

        //ModeloCarro Get(int modeloCarroId);
    }
}