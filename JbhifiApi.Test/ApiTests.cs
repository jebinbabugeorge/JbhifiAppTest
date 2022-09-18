using JbhifiApi.Controllers;
using JbhifiApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace JbhifiApi.Test
{
    public class ApiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("london", "uk")]
        [TestCase("melbourne", "aus")]
        [TestCase("sydney", "aus")]
        public async Task VerifyGetRequestReturnsSuccessAsync(string city, string country)
        {
            var mockService = new Mock<IOpenWeatherMapService>();

            var controller = new WeatherForecastController(mockService.Object);

            var description = await controller.Get(city, country);

            Assert.IsInstanceOf<OkObjectResult>(description);
        }

        [Test]
        [TestCase("", "uk")]
        [TestCase("melbourne", "")]
        [TestCase("", "")]
        public async Task VerifyGetRequestReturnsBadRequestAsync(string city, string country)
        {
            var mockService = new Mock<IOpenWeatherMapService>();

            var controller = new WeatherForecastController(mockService.Object);

            var description = await controller.Get(city, country);

            Assert.IsInstanceOf<BadRequestObjectResult>(description);
        }

        [Test]
        [TestCase("london", "uk")]
        public async Task VerifyFailedWeatherDescriptionAsync(string city, string country)
        {
            var expectedDescription = $"The weather forecast for {city}, {country} is not available at the moment. Please try again later.";
            var mockService = new Mock<IOpenWeatherMapService>();

            mockService.Setup(x => x.GetWeatherDetailsAsync(city, country))
                       .ReturnsAsync(expectedDescription);

            var controller = new WeatherForecastController(mockService.Object);

            var description = await controller.Get(city, country);

            var actualDescription = (description as OkObjectResult).Value;

            Assert.That(expectedDescription, Is.EqualTo(actualDescription));
        }

        [Test]
        [TestCase("london", "uk", "scattered clouds")]
        [TestCase("melbourne", "aus", "cloudy sky")]
        public async Task VerifySuccessWeatherDescriptionAsync(string city, string country, string descr)
        {
            var expectedDescription = $"The weather forecast for {city}, {country} is {descr}";
            var mockService = new Mock<IOpenWeatherMapService>();

            mockService.Setup(x => x.GetWeatherDetailsAsync(city, country))
                       .ReturnsAsync(expectedDescription);

            var controller = new WeatherForecastController(mockService.Object);

            var description = await controller.Get(city, country);

            var actualDescription = (description as OkObjectResult).Value;

            Assert.That(expectedDescription, Is.EqualTo(actualDescription));
        }
    }
}