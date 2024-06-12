using Microsoft.EntityFrameworkCore;
using ProjetoCQW.DTO;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.CustomExceptions;

namespace ProjetoCQW.Service
{

    public class ModeloSiteDetalheService : IModeloSiteDetalheService
    {
        private readonly ConnectionContext _context;
        private readonly IMontadoraService _montadoraService;
        public ModeloSiteDetalheService(ConnectionContext context, IMontadoraService montadoraService)
        {
            _context = context;
            _montadoraService = montadoraService;
        }

        public async Task<ModeloSiteDetalhe> Add(ModeloSiteDetalheDTO modeloSiteDetalhe)
        {
            var montadora = _montadoraService.Get(modeloSiteDetalhe.Montadora_Id);

            var modeloSite = new ModeloSiteDetalhe()
            {
                UrlSite = modeloSiteDetalhe.UrlSite,
                XpathAno = modeloSiteDetalhe.XpathAno,
                XpathCor = modeloSiteDetalhe.XpathCor,
                XpathImg = modeloSiteDetalhe.XpathImg,
                XpathModelo = modeloSiteDetalhe.XpathModelo,
                XpathValor = modeloSiteDetalhe.XpathValor,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Montadora = montadora,
                Montadora_Id = montadora.id
            };
            await _context.AddAsync(modeloSite);
            await _context.SaveChangesAsync();

            return modeloSite;
        }

        public async Task Delete(int id)
        {
            var modeloSite = await _context.ModeloSiteDetalhes.FindAsync(id);
            if (modeloSite != null)
            {
                _context.Remove(modeloSite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ModeloSiteDetalhe>> Get()
        {
            return await _context.ModeloSiteDetalhes.ToListAsync();
        }

        public ModeloSiteDetalhe Get(int id)
        {
            return _context.ModeloSiteDetalhes.Find(id);
        }

        public async Task<ModeloSiteDetalhe> Update(int id, ModeloSiteDetalheDTO modeloSiteDetalhe)
        {
            if (id == 0)
            {
                throw new IdNotFoundException("Id Inválido");
            }

            if (modeloSiteDetalhe.XpathValor == null || modeloSiteDetalhe.XpathValor == string.Empty)
            {
                throw new WrongPropertyException("Valor Invalido");
            }

            if (modeloSiteDetalhe.XpathModelo == null || modeloSiteDetalhe.XpathModelo == string.Empty)
            {
                throw new WrongPropertyException("Modelo Invalido");
            }

            if (modeloSiteDetalhe.XpathAno == null || modeloSiteDetalhe.XpathAno == string.Empty)
            {
                throw new WrongPropertyException("Ano Invalido");
            }

            if (modeloSiteDetalhe.XpathCor == null || modeloSiteDetalhe.XpathCor == string.Empty)
            {
                throw new WrongPropertyException("Cor Invalido");
            }

            if (modeloSiteDetalhe.XpathImg == null || modeloSiteDetalhe.XpathImg == string.Empty)
            {
                throw new WrongPropertyException("Imagem Invalida");
            }

            if (modeloSiteDetalhe.UrlSite == null || modeloSiteDetalhe.UrlSite == string.Empty)
            {
                throw new WrongPropertyException("Url Invalida");
            }

            if (modeloSiteDetalhe.Montadora_Id == 0)
            {
                throw new IdNotFoundException("ModeloCarro_Id Invalido");
            }

            var modelo = _context.ModeloSiteDetalhes.Find(id) ?? throw new Exception("Id não encontrado");

            modelo.XpathValor = modeloSiteDetalhe.XpathValor;
            modelo.XpathCor = modeloSiteDetalhe.XpathCor;
            modelo.XpathAno = modeloSiteDetalhe.XpathAno;
            modelo.XpathImg = modeloSiteDetalhe.XpathImg;
            modelo.XpathModelo = modeloSiteDetalhe.XpathModelo;
            modelo.UrlSite = modeloSiteDetalhe.UrlSite;
            modelo.DataAtualizacao = DateTime.Now;

            _context.ModeloSiteDetalhes.Update(modelo);
            await _context.SaveChangesAsync();

            return modelo;
        }
    }
}