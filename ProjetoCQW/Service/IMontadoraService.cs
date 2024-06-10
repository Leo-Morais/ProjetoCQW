using ProjetoCQW.Model;

namespace ProjetoCQW.Service
{
    public interface IMontadoraService
    {
        void Add(Montadora montadora);

        void Delete(int id);

        Montadora Update(int id, string nome = null, string urlSite = null);

        List<Montadora> Get();

        Montadora Get(int montadoraId);
    }
}
