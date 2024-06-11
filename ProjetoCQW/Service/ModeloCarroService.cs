using ProjetoCQW.Repository;
using ProjetoCQW.Model;
using ProjetoCQW.DTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ModeloCarro> Update(int id, ModeloCarroDTO modeloCarroDTO)
        {
            var modeloCarro = await _context.ModeloCarros.FindAsync(id);
            if (modeloCarro == null) 
            {
                return null;
            } 

            if (modeloCarroDTO.Montadora_Id != null)
            {
                var montadora =  _montadoraService.Get(modeloCarroDTO.Montadora_Id);
                if (montadora == null) return null;
                modeloCarro.Montadora = montadora;
                modeloCarro.Montadora_Id = montadora.id;
            }

            if (modeloCarroDTO.Ano != null)
                modeloCarro.Ano = modeloCarroDTO.Ano;
            if (!string.IsNullOrEmpty(modeloCarroDTO.Nome))
                modeloCarro.Nome = modeloCarroDTO.Nome;
            if (!string.IsNullOrEmpty(modeloCarroDTO.Cor))
                modeloCarro.Cor = modeloCarroDTO.Cor;
            if (modeloCarroDTO.Valor != null)
                modeloCarro.Valor = modeloCarroDTO.Valor;
            if (!string.IsNullOrEmpty(modeloCarroDTO.Versao))
                modeloCarro.Versao = modeloCarroDTO.Versao;

            modeloCarro.DataAtualizacao = DateTime.Now;

            _context.ModeloCarros.Update(modeloCarro);
            await _context.SaveChangesAsync();

            return modeloCarro;
        }
    }
}
