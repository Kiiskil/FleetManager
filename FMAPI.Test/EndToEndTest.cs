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
        [Fact]
        public async void GetCarByIdSmokeTest()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000/");
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