using ProjetoCQW.Model;
using ProjetoCQW.ViewModel;

namespace ProjetoCQW.Service
{
    public interface IModeloCarroService
    {
        Task<ModeloCarro> Add(ModeloCarroDTO modeloCarro);

        void Delete(int id);

        //ModeloCarro Update();

        List<ModeloCarro> Get();
    }
}
