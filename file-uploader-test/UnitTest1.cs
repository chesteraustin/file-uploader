using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace file_uploader_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task DefaultRoute_ReturnsHelloWorld()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("");
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.AreEqual("Hello World!", stringResult);
        }
    }
}