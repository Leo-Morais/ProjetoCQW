using ProjetoCQW.DTO;
using ProjetoCQW.Model;


namespace ProjetoCQW.Service
{
    public interface IMontadoraService
    {
        Task<Montadora> Add(MontadoraDTO montadoraDTO);

        Task Delete(int id);

        Task<Montadora> Update(int id, string nome = null, string urlSite = null);

        Task<List<Montadora>> Get();

        Montadora Get(int montadoraId);
    }
}
