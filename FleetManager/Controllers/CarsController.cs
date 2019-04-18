using Microsoft.AspNetCore.Mvc;
using FleetManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Controllers {

    [Route("api/[controller]")]
    public class CarsController : Controller {

        private CarsDbContext dbContext;

        public CarsController() {
            //SIIRRA yhteystiedot AppSetting.json:iin
           /*  string connectionString = "server=192.168.100.13;port=3306;database=fleet_manager;userid=admin;pwd=arnoboy11;sslmode=none"; */
            string connectionString = "server=iirola.hopto.org;port=5001;database=fleet_manager;userid=admin;pwd=arnoboy11";
            dbContext = CarsDbContextFactory.Create(connectionString);
        }

        // GET api/cars/all
        //KARSI model,brand ja motor koska ne ovat NULL ilman Include-metodia ja/tai erikseen maaritettyja relaatioita
        [HttpGet("{all}")]
        public ActionResult Get() {
            return Ok(dbContext.Car
            .ToArray());
        }

        // GET api/cars/101
        [HttpGet("{id}")]
        public ActionResult GetByID(int id) {
            // var Car = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
            var Car = dbContext.Car
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
        [HttpGet("year/{low}/{high}")]
        public ActionResult GetByYear(int low, int high) {
            // var Car = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
            var Car = dbContext.Car

            .Where(car => car.Year >= low && car.Year <= high).ToArray();
            if (Car != null) {
                return Ok(Car);
            } else {
                return NotFound();
            }
        }

        // GET api/model/101
        //Zeros are ignored, so this method can be used for both filters separately or by themselves
        [HttpGet("brand/{brand}/model/{model}")]
        public ActionResult GetByModel(int brand, int model) {
            return Ok(brand);
        }

        /* // GET api/brand/101
        [HttpGet("{brand}")]
        public ActionResult GetByModel(int id) {

        } */

        // POST api/Cars
        [HttpPost]
        public ActionResult CreateCar([FromBody]Car Car) {
            if (!ModelState.IsValid)
                return BadRequest();

            dbContext.Car.Add(Car);
            dbContext.SaveChanges();
            return Created("api/Cars", Car);
        }

        // PUT api/Cars/101
        [HttpPut("{id}")]
        public ActionResult ModifyCar(int id, [FromBody]Car Car) {
            // var target = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
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
            // var Car = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
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