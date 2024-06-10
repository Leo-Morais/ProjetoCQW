using ProjetoCQW.Repository;
using ProjetoCQW.Model;
using ProjetoCQW.ViewModel;

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

        public void Delete(int id)
        {
            var modeloCarros = _context.ModeloCarros.Find(id);
            if(modeloCarros != null) 
            {
                _context.Remove(modeloCarros);
                _context.SaveChanges();
            }
        }

        public List<ModeloCarro> Get()
        {
             return _context.ModeloCarros.ToList();
        }
    }
}
