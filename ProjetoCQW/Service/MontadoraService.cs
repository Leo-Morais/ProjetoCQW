using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjetoCQW.CustomExceptions;
using ProjetoCQW.DTO;
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
        public async Task<Montadora> Add(MontadoraDTO montadoraDto)
        {
            var montadora = new Montadora()
            {
                Nome = montadoraDto.Name,
                UrlSite = montadoraDto.UrlSite,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            };

            await _context.Montadoras.AddAsync(montadora);
            await _context.SaveChangesAsync();

            return montadora;
        }

        //Deleta a informação dentro da tabela recebendo o id por parâmetro.
        public async Task Delete(int id)
        {
            var montadora = await _context.Montadoras.FindAsync(id);
            if (montadora == null)
            {
                throw new IdNotFoundException($"Montadora com o ID {id} não encontrada.");
            }

            _context.Montadoras.Remove(montadora);
            await _context.SaveChangesAsync();

        }

        //Atualiza Nome e/ou url da Montadora.
        public async Task<Montadora> Update(int id, string nome, string urlSite)
        {

            if (nome == null || nome == string.Empty)
            {
                throw new WrongPropertyException("Nome inválido");
            }

            if (urlSite == null || urlSite == string.Empty)
            {
                throw new WrongPropertyException("UrlSite inválido");
            }

            if (id < 0)
            {
                throw new IdNotFoundException("Id inválido");
            }


            var montadora = await _context.Montadoras.FindAsync(id);
            if (montadora == null)
            {
                throw new IdNotFoundException($"Montadora com o ID {id} não encontrada.");
            }

            montadora.Nome = nome;
            montadora.UrlSite = urlSite;    
            montadora.DataAtualizacao = DateTime.Now;
             _context.Montadoras.Update(montadora);
            await _context.SaveChangesAsync();

            return montadora;
        }

        //Retorna a lista de Montadora
        public async Task<List<Montadora>> Get()
        {
            return await _context.Montadoras.ToListAsync();
        }

        public Montadora? Get(int montadoraId)
        {
            return _context.Montadoras.Find(montadoraId);
        }

        public async Task<Montadora> GetById(int id)
        {
            var montadoraEncontrada = await _context.Montadoras.FindAsync(id);
            if (montadoraEncontrada == null)
            {
                throw new IdNotFoundException($"Montadora com o ID {id} não encontrada.");
            }
            return montadoraEncontrada;
        }
    }
}
