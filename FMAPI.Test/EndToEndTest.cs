using System;
using Xunit;
using FleetManager.Models;
using FleetManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FMAPI.Test
{
    public class CarsControllerEtETest
    {
<<<<<<< HEAD:FMAPI.Test/UnitTest1.cs
        /* [Fact]
        public void GetAllCars()
        {
            var controller = new CarsController();
            var result = controller.GetAllCars() as OkObjectResult;
            Assert.NotNull(result);
        } */

=======
>>>>>>> 8fb916ef37585ca545b9e3132ff68e9a43c0b3c7:FMAPI.Test/EndToEndTest.cs
        [Fact]
        public async void GetCarByIdSmokeTest()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://fleetmanager.hopto.org/");
                var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
                var response = await httpClient.GetAsync("api/cars/1");
                string jsonString = null;
                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                    Assert.NotNull(jsonString);
                }

                Car car = JsonConvert.DeserializeObject<Car>(jsonString);
                Assert.NotNull(car);
                Assert.Equal(car.ID, 1);
                Assert.Equal(car.Regno, "AAA-111");
                Assert.Equal(car.Year, 1900);
            }
        }
    }

}