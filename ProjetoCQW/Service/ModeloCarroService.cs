using ProjetoCQW.Repository;
using ProjetoCQW.Model;
using ProjetoCQW.DTO;
using Microsoft.EntityFrameworkCore;
using ProjetoCQW.CustomExceptions;

namespace ProjetoCQW.Service
{
    public class ModeloCarroService : IModeloCarroService
    {
        private readonly ConnectionContext _context;
        private readonly IMontadoraService _montadoraService;
        public ModeloCarroService(ConnectionContext context, IMontadoraService montadoraService)
        {
            _context = context;
            _montadoraService = montadoraService;
        }

        public async Task<ModeloCarro> Add(ModeloCarroDTO modeloCarro)
        {
            var montadora = _montadoraService.Get(modeloCarro.Montadora_Id);

            var carro = new ModeloCarro()
            {
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Ano = modeloCarro.Ano,
                Nome = modeloCarro.Nome,
                Valor = modeloCarro.Valor,
                Cor = modeloCarro.Cor,
                Versao = modeloCarro.Versao,
                Imagem = "imagem",
                Montadora = montadora,
                Montadora_Id = montadora.id
            };


            await _context.AddAsync(carro);
            await _context.SaveChangesAsync();

            return carro;
        }

        public async Task Delete(int id)
        {
            var modeloCarros = await _context.ModeloCarros.FindAsync(id);
            if(modeloCarros != null) 
            {
                _context.Remove(modeloCarros);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ModeloCarro>> Get()
        {
             return await _context.ModeloCarros.ToListAsync();
        }

        public ModeloCarro? Get(int modeloCarroId)
        {
            return _context.ModeloCarros.Find(modeloCarroId);
        }

        public async Task<ModeloCarro> Update(int id, ModeloCarroDTO modeloCarroDTO)
        {

            if (id == 0)
            {
                throw new IdNotFoundException("Id inválido");
            }

            if (modeloCarroDTO.Nome == null || modeloCarroDTO.Nome == string.Empty)
            {
                throw new WrongPropertyException("Nome Inválido");
            }

            if (modeloCarroDTO.Ano == 0)
            {
                throw new WrongPropertyException("Data Inválida");
            }

            if(modeloCarroDTO.Cor == null || modeloCarroDTO.Cor == string.Empty)
            {
                throw new WrongPropertyException("Cor Inválida");
            }

            if (modeloCarroDTO.Versao == null || modeloCarroDTO.Versao == string.Empty)
            {
                throw new WrongPropertyException("Versão Inválida");
            }

            if (modeloCarroDTO.Valor == 0)
            {
                throw new WrongPropertyException("Valor Inválido");
            }
            
            if (modeloCarroDTO.Montadora_Id == 0)
            {
                throw new IdNotFoundException("Montadora Id inválido");
            }

            var modeloCarro = _context.ModeloCarros.Find(id) ?? throw new Exception("Id não encontrado");

            modeloCarro.Nome = modeloCarroDTO.Nome;
            modeloCarro.Ano = modeloCarroDTO.Ano;
            modeloCarro.Valor = modeloCarroDTO.Valor;
            modeloCarro.Cor = modeloCarroDTO.Cor;
            modeloCarro.Versao = modeloCarroDTO.Versao;

            modeloCarro.DataAtualizacao = DateTime.Now;

            _context.ModeloCarros.Update(modeloCarro);
            await _context.SaveChangesAsync();

            return modeloCarro;
        }
    }
}
