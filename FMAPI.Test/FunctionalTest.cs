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
    public class CarsControllerFunctionTest
    {
        [Fact]
        public void CreateCarsControllerInstanceTest() {
            var controller = new CarsController();
            Assert.NotNull(controller);
        }

        [Fact]
        public void GetAllCarsTest()
        {
            var controller = new CarsController();
            Assert.IsType<OkObjectResult>(controller.GetAllCars());
            var result = controller.GetAllCars();
            Assert.NotNull(result);
        }

        [Fact]
        public void PostActionTest() {
            var controller = new CarsController();
            var product = new Car { Regno = "test-test", Year = 2000, InspectionDate = "2000-01-01", ModelID = 1, MotorID = 1, BrandID = 1 };
            Assert.IsType<CreatedResult>(controller.CreateCar(product));
        }
    }
}
