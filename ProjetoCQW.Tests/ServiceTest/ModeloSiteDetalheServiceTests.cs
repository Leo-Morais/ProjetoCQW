using Microsoft.EntityFrameworkCore;
using Moq;
using ProjetoCQW.DTO;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjetoCQW.Tests.ServiceTest
{
    public class ModeloSiteDetalheServiceTests
    {
       
        private ConnectionContext GetInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ConnectionContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ConnectionContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task ModeloSiteDetalheService_Add_ReturnDetalhe()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var service = new ModeloSiteDetalheService(context);

            var modeloSiteDetalheDTO = new ModeloSiteDetalheDTO
            {
                UrlSite = "www.teste.com",
                XpathCor = "//Cor",
                XpathImg = "//Img",
                XpathModelo = "//Modelo",
                XpathNome = "//Nome",
                XpathValor = "//Valor"
            };
         

            // Act
            var result = await service.Add(modeloSiteDetalheDTO);
            await context.SaveChangesAsync();

            // Assert
            var modeloSite = await context.ModeloSiteDetalhes.FindAsync(result.id);
            Assert.NotNull(modeloSite);
            Assert.Equal(modeloSiteDetalheDTO.UrlSite, modeloSite.UrlSite);
            Assert.Equal(modeloSiteDetalheDTO.XpathNome, modeloSite.XpathNome);
            Assert.Equal(modeloSiteDetalheDTO.XpathImg, modeloSite.XpathImg);
            Assert.Equal(modeloSiteDetalheDTO.XpathValor, modeloSite.XpathValor);
            Assert.Equal(modeloSiteDetalheDTO.XpathCor, modeloSite.XpathCor);
            Assert.Equal(modeloSiteDetalheDTO.XpathModelo, modeloSite.XpathModelo);
        }

        [Fact]
        public async Task ModeloSiteDetalheService_Update_ReturnDetalhe()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var service = new ModeloSiteDetalheService(context);
            var modeloSiteDetalhe = new ModeloSiteDetalhe 
            {
                id = 1,
                XpathValor = "//oldValor",
                XpathNome = "//oldName",
                XpathCor = "//oldCor",
                XpathModelo = "//oldModelo",
                UrlSite = "www.old.com",
                XpathImg = "//oldImg",
                DataAtualizacao = DateTime.Now,
                DataCriacao = DateTime.Now,
            };
            context.ModeloSiteDetalhes.Add(modeloSiteDetalhe);
            await context.SaveChangesAsync();

            var modeloSiteDetalheDTO = new ModeloSiteDetalheDTO
            {
                UrlSite = "www.teste.com",
                XpathCor = "//Cor",
                XpathImg = "//Img",
                XpathModelo = "//Modelo",
                XpathNome = "//Nome",
                XpathValor = "//Valor"
            };

            // Act
            var result = await service.Update(1,modeloSiteDetalheDTO);
            // Assert

            var updatedModeloSite = await context.ModeloSiteDetalhes.FindAsync(1);
            Assert.NotNull(updatedModeloSite);
            Assert.Equal(modeloSiteDetalheDTO.XpathValor, updatedModeloSite.XpathValor);
        }


        [Fact]
        public async Task ModeloSiteDetalheService_Delete()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var service = new ModeloSiteDetalheService(context);

            var modeloSiteDetalhe = new ModeloSiteDetalhe
            {
                id = 1,
                UrlSite = "www.teste.com",
                XpathCor = "//Cor",
                XpathImg = "//Img",
                XpathModelo = "//Modelo",
                XpathNome = "//Nome",
                XpathValor = "//Valor",
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now
            };
            context.Add(modeloSiteDetalhe);
            await context.SaveChangesAsync();

            // Act
            await service.Delete(1);
            await context.SaveChangesAsync();

            // Assert
            var result = await context.ModeloCarros.ToListAsync();
            Assert.DoesNotContain(result, r => r.id == modeloSiteDetalhe.id);
            Assert.Empty(result);
        }

        [Fact]
        public async Task ModeloSiteDetalheService_Get_ReturnList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var service = new ModeloSiteDetalheService(context);

            var modeloSiteDetalhes = new List<ModeloSiteDetalhe>
            {
                new ModeloSiteDetalhe 
                {
                    id = 1, 
                    XpathValor = "Valor1", 
                    UrlSite = "www.teste1.com", 
                    XpathCor ="Cor1",
                    XpathImg = "Img1",
                    XpathModelo = "Modelo1",
                    XpathNome = "Nome1",
                    DataAtualizacao= DateTime.Now,
                    DataCriacao = DateTime.Now
                },
                new ModeloSiteDetalhe
                {
                    id = 2,
                    XpathValor = "Valor2",
                    UrlSite = "www.teste2.com",
                    XpathCor ="Cor2",
                    XpathImg = "Img2",
                    XpathModelo = "Modelo2",
                    XpathNome = "Nome2",
                    DataAtualizacao= DateTime.Now,
                    DataCriacao = DateTime.Now
                },
            };
            context.ModeloSiteDetalhes.AddRange(modeloSiteDetalhes);
            await context.SaveChangesAsync();
            // Act
            var result = await service.Get();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task ModeloSiteDetalheService_GetById_ReturnModeloSiteDetalhe()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var service = new ModeloSiteDetalheService(context);

            var modeloSiteDetalhe = new ModeloSiteDetalhe
            {
                id = 1,
                XpathValor = "Valor1",
                UrlSite = "www.teste1.com",
                XpathCor = "Cor1",
                XpathImg = "Img1",
                XpathModelo = "Modelo1",
                XpathNome = "Nome1",
                DataAtualizacao = DateTime.Now,
                DataCriacao = DateTime.Now
            };
            context.ModeloSiteDetalhes.Add(modeloSiteDetalhe);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.id);

        }
    }


}
