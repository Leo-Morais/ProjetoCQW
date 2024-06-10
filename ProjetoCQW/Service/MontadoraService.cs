using ProjetoCQW.Model;
using ProjetoCQW.Repository;

namespace ProjetoCQW.Service
{
    public class MontadoraService : IMontadoraService
    {
        private readonly ConnectionContext _context;
        public MontadoraService(ConnectionContext context)
        {
            _context = context;
        }

        //Adiciona uma nova Montadora no banco de dados.
        public void Add(Montadora montadora)
        {
            _context.Montadoras.Add(montadora);
            _context.SaveChanges();
        }

        //Deleta a informação dentro da tabela recebendo o id por parâmetro.
        public void Delete(int id)
        {
            var montadora = _context.Montadoras.Find(id);
            if (montadora != null)
            {
                _context.Montadoras.Remove(montadora);
                _context.SaveChanges();
            }
        }

        //Atualiza Nome e/ou url da Montadora.
        public Montadora Update(int id, string nome, string urlSite)
        {
            var montadora = _context.Montadoras.Find(id);
            if (montadora == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                montadora.Nome = nome;
            }
            
            if (!string.IsNullOrEmpty(urlSite))
            {
                montadora.UrlSite = urlSite;
            }
            montadora.DataAtualizacao = DateTime.Now;
            _context.Montadoras.Update(montadora);
            _context.SaveChanges();

            return montadora;
        }

        //Retorna a lista de Montadora
        public List<Montadora> Get()
        {
            return _context.Montadoras.ToList();
        }

        public Montadora? Get(int montadoraId)
        {
            return _context.Montadoras.Find(montadoraId);
        }


    }
}
