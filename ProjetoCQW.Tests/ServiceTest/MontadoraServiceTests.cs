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
        private readonly ConnectionContext _context;
        private readonly MontadoraService _montadoraService;


        public MontadoraServiceTests()
        {
            // Inicializa o banco de dados em memória
            var options = new DbContextOptionsBuilder<ConnectionContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new ConnectionContext(options);
            _montadoraService = new MontadoraService(_context);
        }

        [Fact]
        public async Task MontadoraService_Add_ReturnsSuccess()
        {
            // Arrange
            var montadoraDto = new MontadoraDTO
            {
                Name = "Teste",
                UrlSite = "www.teste.com",
            };


            // Act
            var result = await _montadoraService.Add(montadoraDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(montadoraDto.Name, result.Nome);
            Assert.Equal(montadoraDto.UrlSite, result.UrlSite);
        }


        [Fact]
        public async Task MontadoraService_Delete()
        {
            // Arrange
            var montadora = new MontadoraDTO 
            {
                Name = "Teste",
                UrlSite = "www.teste.com"
            };
            var result = await _montadoraService.Add(montadora);
            await _context.SaveChangesAsync();

            // Act
            await _montadoraService.Delete(result.id);
            await _context.SaveChangesAsync();

            // Assert
            var montadoras = await _context.Montadoras.ToListAsync();
            Assert.DoesNotContain(montadoras, m => m.id == result.id);
        }


        [Fact]
        public async Task MontadoraService_Update_ReturnMontadora()
        {
         
            // Arrange
            var montadora = new MontadoraDTO
            {
                Name = "Teste",
                UrlSite = "Teste"
            };
            var result = await _montadoraService.Add(montadora);
            await _context.SaveChangesAsync();

            // Act
            var novoResult = await _montadoraService.Update(result.id, "Teste2", "www.Teste2.com");

            // Assert
            Assert.NotNull(novoResult);
            Assert.Equal("Teste2",novoResult.Nome);
            Assert.Equal("www.Teste2.com", novoResult.UrlSite);

        }


        [Fact]
        public async Task MontadoraService_Get_ReturnMontadora()
        {
            // Arrange
            var montadoraDto = new MontadoraDTO
            {
                Name = "Teste",
                UrlSite = "www.teste.com",
            };
            var result = await _montadoraService.Add(montadoraDto);

            // Save changes to ensure the ID is generated
            await _context.SaveChangesAsync();

            // Act
            var montadora = await _montadoraService.GetById(result.id);

            // Assert
            result.id.Should().Be(0); // Verifique se o ID foi gerado corretamente
            montadora.id.Should().Be(result.id);
            montadora.Nome.Should().Be("Teste");
            montadora.UrlSite.Should().Be("www.teste.com");
        }

    }



}


