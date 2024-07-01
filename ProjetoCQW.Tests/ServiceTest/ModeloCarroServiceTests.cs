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

namespace ProjetoCQW.Tests.ServiceTest
{
    public class ModeloCarroServiceTests
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
        public async Task ModeloCarroService_Add_ReturnModeloCarro()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);
            var modeloSiteService = new ModeloSiteDetalheService(context);

            var service = new ModeloCarroService(context, montadoraService, modeloSiteService);


            var montadora = new Montadora
            {
                id = 1,
                Nome = "Montadora Teste",
                UrlSite = "www.Teste.com"
            };

            var modeloSiteDetalhe = new ModeloSiteDetalhe
            { 
                id = 1,
                UrlSite = "www.teste.com",
                XpathCor = "//Cor",
                XpathImg = "//Img",
                XpathModelo = "//Modelo",
                XpathNome = "//Nome",
                XpathValor = "//Valor"
            };


            var carroDTO = new ModeloCarroDTO
            {
                Ano = 1,
                Cor = "preto",
                Imagem = "imagem",
                Nome = "carro",
                Valor = 1,
                Versao = "premium",
                ModeloSite_Id = 1,
                Montadora_Id = 1,                
            };

            // Act
            var result = await service.Add(carroDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(carroDTO.Ano, result.Ano);
        }

        public async Task ModeloCarroService_Delete()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);
            var modeloSiteService = new ModeloSiteDetalheService(context);

            var service = new ModeloCarroService(context, montadoraService, modeloSiteService);

            var carro = new ModeloCarro
            {
                id = 1,
                Ano = 1,
                Cor = "preto",
                Imagem = "imagem",
                Nome = "carro",
                Valor = 1,
                Versao = "premium",
                ModeloSite_Id = 1,
                Montadora_Id = 1,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            };

            // Act
            await service.Delete(1);
            // Assert
            var result = await context.ModeloCarros.ToListAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task ModeloCarroService_Update_ReturnModeloCarro()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);
            var modeloSiteService = new ModeloSiteDetalheService(context);

            var service = new ModeloCarroService(context, montadoraService, modeloSiteService);

            var carro = new ModeloCarro
            {
                id = 1,
                Ano = 1,
                Cor = "oldColor",
                Imagem = "oldImagem",
                Nome = "oldCarro",
                Valor = 15,
                Versao = "old",
                ModeloSite_Id = 1,
                Montadora_Id = 1,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,

            };


            var carroDTO = new ModeloCarroDTO
            {
                Ano = 1,
                Cor = "preto",
                Imagem = "imagem",
                Nome = "carro",
                Valor = 1,
                Versao = "premium",
                ModeloSite_Id = 1,
                Montadora_Id = 1,
            };

            // Act
            var result = service.Update(1, carroDTO);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public async Task ModeloCarroService_GetById_ReturnModeloCarro()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);
            var modeloSiteService = new ModeloSiteDetalheService(context);

            var service = new ModeloCarroService(context, montadoraService, modeloSiteService);

            var carro = new ModeloCarro
            {
                id = 1,
                Ano = 1,
                Cor = "oldColor",
                Imagem = "oldImagem",
                Nome = "oldCarro",
                Valor = 15,
                Versao = "old",
                ModeloSite_Id = 1,
                Montadora_Id = 1,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,

            };

            context.ModeloCarros.Add(carro);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetById(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.id);
        }

        [Fact]
        public async Task ModeloCarroService_Get_ReturnListModeloCarro()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);
            var modeloSiteService = new ModeloSiteDetalheService(context);

            var service = new ModeloCarroService(context, montadoraService, modeloSiteService);


            var carros = new List<ModeloCarro>
            {
                new ModeloCarro
                {
                    id = 1,
                    Ano = 1,
                    Cor = "preto",
                    Imagem = "imagem",
                    Nome = "carro",
                    Valor = 1,
                    Versao = "premium",
                    ModeloSite_Id = 1,
                    Montadora_Id = 1,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                },
                new ModeloCarro
                {
                    id = 2,
                    Ano = 2,
                    Cor = "branco",
                    Imagem = "imagem2",
                    Nome = "carro2",
                    Valor = 2,
                    Versao = "premium3",
                    ModeloSite_Id = 2,
                    Montadora_Id = 2,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                },
            };
            context.ModeloCarros.AddRange(carros);
            await context.SaveChangesAsync();

            // Act
            var result = await service.Get();
            // Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
