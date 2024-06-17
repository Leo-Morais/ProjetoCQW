using Microsoft.EntityFrameworkCore;
using ProjetoCQW.DTO;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoCQW.Service
{

    public class ModeloSiteDetalheService : IModeloSiteDetalheService
    {
        private readonly ConnectionContext _context;
        public ModeloSiteDetalheService(ConnectionContext context)
        {
            _context = context;
            
        }

        public async Task<ModeloSiteDetalhe> Add(ModeloSiteDetalheDTO modeloSiteDetalhe)
        {

            var modeloSite = new ModeloSiteDetalhe()
            {
                UrlSite = modeloSiteDetalhe.UrlSite,
                XpathCor = modeloSiteDetalhe.XpathCor,
                XpathImg = modeloSiteDetalhe.XpathImg,
                XpathModelo = modeloSiteDetalhe.XpathModelo,
                XpathValor = modeloSiteDetalhe.XpathValor,
                XpathNome = modeloSiteDetalhe.XpathNome,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
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

            if (modeloSiteDetalhe.XpathNome == null || modeloSiteDetalhe.XpathNome == string.Empty)
            {
                throw new WrongPropertyException("Nome Invalido");
            }

            if (modeloSiteDetalhe.XpathModelo == null || modeloSiteDetalhe.XpathModelo == string.Empty)
            {
                throw new WrongPropertyException("Modelo Invalido");
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

            var modelo = _context.ModeloSiteDetalhes.Find(id) ?? throw new Exception("Id não encontrado");

            modelo.XpathValor = modeloSiteDetalhe.XpathValor;
            modelo.XpathCor = modeloSiteDetalhe.XpathCor;
            modelo.XpathImg = modeloSiteDetalhe.XpathImg;
            modelo.XpathModelo = modeloSiteDetalhe.XpathModelo;
            modelo.UrlSite = modeloSiteDetalhe.UrlSite;
            modelo.XpathNome = modeloSiteDetalhe.XpathNome;
            modelo.DataAtualizacao = DateTime.Now;

            _context.ModeloSiteDetalhes.Update(modelo);
            await _context.SaveChangesAsync();

            return modelo;
        }
    }
}