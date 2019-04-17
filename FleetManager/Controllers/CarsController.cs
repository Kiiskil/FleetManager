using Microsoft.AspNetCore.Mvc;
using FleetManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Controllers {

    [Route("api/[controller]")]
    public class CarsController : Controller {

        private CarsDbContext dbContext;

        public CarsController() {
            // string connectionString = "server=localhost;port=3306;database=sakila;userid=admin;pwd=arnoboy11;sslmode=none";
            string connectionString = "server=iirola.hopto.org;port=5001;database=fleet_manager;userid=admin;pwd=arnoboy11";
            dbContext = CarsDbContextFactory.Create(connectionString);
        }

        // GET api/Cars
        [HttpGet]
        public ActionResult Get() {
            return Ok(dbContext.Car
            .Include(car => car.Motor)
            .Include(car => car.Model)
            .Include(car => car.Brand)
            .ToArray());
        }

        // GET api/Cars/101
        [HttpGet("{id}")]
        public ActionResult Get(string id) {
            // var Car = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
            var Car = dbContext.Car.SingleOrDefault(a => a.Regno == id);
            if (Car != null) {
                return Ok(Car);
            } else {
                return NotFound();
            }
        }

        // POST api/Cars
        [HttpPost]
        public ActionResult Post([FromBody]Car Car) {
            if (!ModelState.IsValid)
                return BadRequest();

            dbContext.Car.Add(Car);
            dbContext.SaveChanges();
            return Created("api/Cars", Car);
        }

        // PUT api/Cars/101
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]Car Car) {
            // var target = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
            var target = dbContext.Car.SingleOrDefault(a => a.Regno == id);
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
        public ActionResult Delete(string id) {
            // var Car = dbContext.Car.SingleOrDefault(a => a.Car_ID == id);
            var Car = dbContext.Car.SingleOrDefault(a => a.Regno == id);
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