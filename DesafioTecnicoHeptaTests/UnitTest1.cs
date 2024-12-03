using Microsoft.AspNetCore.Mvc.Testing;
using Painel;
namespace DesafioTecnicoHeptaTests
{
    public class UnitTest1 :IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest1()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory;
        }

        [Fact]
        public async void TestDiagnostico()
        {
            // Arrange
            var client  = _factory.CreateClient();

            // Act            
            string vDados = "00100,11110,10110,10111,10101,01111,00111,11100,10000,11001,00010,01010";
            var parameters = new Dictionary<string, string> { { "vDados", vDados }};
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync("/Diagnostico", encodedContent);

            // Assert
            int code = (int) response.StatusCode;
            Assert.Equal(200, code);

        }
    }
}