using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using ProjetoCQW.DTO;
using ProjetoCQW.Model;
using ProjetoCQW.Repository;
using ProjetoCQW.Service;



namespace ProjetoCQW.Tests.ServiceTest
{
    public class MontadoraServiceTests
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
        public async Task MontadoraService_Add_ReturnsSuccess()
        {
            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);


            var montadoraDto = new MontadoraDTO
            {
                Name = "Teste",
                UrlSite = "www.teste.com",
            };


            // Act
            var result = await montadoraService.Add(montadoraDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(montadoraDto.Name, result.Nome);
            Assert.Equal(montadoraDto.UrlSite, result.UrlSite);
        }


        [Fact]
        public async Task MontadoraService_Delete()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);

            var montadora = new MontadoraDTO 
            {
                Name = "Teste",
                UrlSite = "www.teste.com"
            };
            var result = await montadoraService.Add(montadora);
            await context.SaveChangesAsync();

            // Act
            await montadoraService.Delete(result.id);
            await context.SaveChangesAsync();

            // Assert
            var montadoras = await context.Montadoras.ToListAsync();
            Assert.DoesNotContain(montadoras, m => m.id == result.id);
        }


        [Fact]
        public async Task MontadoraService_Update_ReturnMontadora()
        {

            // Arrange

            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);

            var montadora = new Montadora 
            {
                id = 1, 
                Nome = "Old", 
                UrlSite= "www.old.com", 
                DataAtualizacao = DateTime.Now, 
                DataCriacao = DateTime.Now 
            };

            context.Add(montadora);
            await context.SaveChangesAsync();

            // Act
            var novoResult = await montadoraService.Update(1, "Teste2", "www.Teste2.com");

            // Assert
            Assert.NotNull(novoResult);
            Assert.Equal("Teste2",novoResult.Nome);
            Assert.Equal("www.Teste2.com", novoResult.UrlSite);

        }


        [Fact]
        public async Task MontadoraService_GetById_ReturnMontadora()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);

            var montadora = new Montadora
            {
                id = 1,
                Nome = "Teste",
                UrlSite = "www.teste.com",
                DataAtualizacao = DateTime.Now,
                DataCriacao = DateTime.Now
            };
            context.Montadoras.Add(montadora);

            // Save changes to ensure the ID is generated
            await context.SaveChangesAsync();

            // Act
            var result = await montadoraService.GetById(montadora.id);

            // Assert
            result.id.Should().Be(1); // Verifique se o ID foi gerado corretamente
            montadora.id.Should().Be(result.id);
            montadora.Nome.Should().Be("Teste");
            montadora.UrlSite.Should().Be("www.teste.com");
        }

        [Fact]
        public async Task MontadoraService_Get_ReturnsList()
        {
            // Arrange
            var context = GetInMemoryContext(Guid.NewGuid().ToString());
            var montadoraService = new MontadoraService(context);

            var montadora = new List<Montadora>
            {
                new Montadora
                {
                    id = 1,
                    Nome = "Teste",
                    UrlSite = "www.teste.com",
                    DataAtualizacao = DateTime.Now,
                    DataCriacao = DateTime.Now
                },
                new Montadora
                {
                   id = 2,
                    Nome = "Teste2",
                    UrlSite = "www.teste2.com",
                    DataAtualizacao = DateTime.Now,
                    DataCriacao = DateTime.Now
                },
            };
            context.Montadoras.AddRange(montadora);
            await context.SaveChangesAsync();
            // Act
            var result = await montadoraService.Get();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

    }



}


