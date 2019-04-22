using Microsoft.AspNetCore.Mvc;
using FleetManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Controllers {

    [Route("api/[controller]")]
    public class CarsController : Controller {

        private CarsDbContext dbContext;

        public CarsController() {
            //DB-tiedot AppSetting.jsonissa, josta startup hakee sen
            string connectionString = Startup.ConnectionString;
            dbContext = CarsDbContextFactory.Create(connectionString);
        }

        // GET api/cars/
        [HttpGet]
        public ActionResult GetAllCars() {
            return Ok(dbContext.Car
            //RAW SQL-query
            .FromSql("SELECT carID, regno, year, inspection_date, brandID, motorID, modelID FROM cars")
            .ToArray());
        }

        // GET api/cars/101
        [HttpGet("{id}")]
        public ActionResult GetByID(int id) {
            var Car = dbContext.Car
            //Eager loading of additional data through enitity framework
            .Include(car => car.Motor)
            .Include(car => car.Model)
            .Include(car => car.Brand)
            .SingleOrDefault(car => car.ID == id);
            if (Car != null) {
                return Ok(Car);
            } else {
                return NotFound();
            }
        }

        //GET api/year/from/to/
        //using Route to get args
        [HttpGet("year/{low}/{high}")]
        public ActionResult GetByYear(int low, int high) {
            var Car = dbContext.Car
            .Where(car => car.Year >= low && car.Year <= high).ToArray();
            if (Car != null) {
                return Ok(Car);
            } else {
                return NotFound();
            }
        }

        // GET brand/1/model/2
        //Zeros are ignored, so this method can be used for both filters separately or by themselves
        //using Route to get args
        [HttpGet("brand/{brand}/model/{model}")]
        public ActionResult GetByModel(int brand, int model) {
            var Car = dbContext.Car.ToArray();
            if (brand == 0)
            {
                Car = dbContext.Car
                .Where(car => car.Model.ID == model).ToArray();  
            }
            else if (model == 0)
            {
                Car = dbContext.Car
                .Where(car => car.Brand.ID == brand).ToArray();   
            }
            else
            {
                Car = dbContext.Car
                .Where(car => car.Model.ID == model && car.Brand.ID == brand).ToArray();   
            };
            if (Car != null) {
                return Ok(Car);
            } else {
                return NotFound();
            }
        }

        // POST api/cars
        //using Body to form car-objects
        [HttpPost]
        public ActionResult CreateCar([FromBody]Car Car) {
            if (!ModelState.IsValid)
                return BadRequest();

            dbContext.Car.Add(Car);
            dbContext.SaveChanges();
            return Created("api/Cars", Car);
        }

        // PUT api/cars/101
        //using Body to form car-objects
        [HttpPut("{id}")]
        public ActionResult ModifyCar(int id, [FromBody]Car Car) {
            var target = dbContext.Car.SingleOrDefault(car => car.ID == id);;
            if (target != null && ModelState.IsValid) {
                dbContext.Entry(target).CurrentValues.SetValues(Car);
                dbContext.SaveChanges();
                return Ok();
            } else {
                return BadRequest();
            }
        }

        // DELETE api/Cars/101
        [HttpDelete("{id}")]
        public ActionResult DeleteCar(int id) {
            var Car = dbContext.Car.SingleOrDefault(car => car.ID == id);
            if (Car != null) {
                dbContext.Car.Remove(Car);
                dbContext.SaveChanges();
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}