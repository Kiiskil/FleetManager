FleetManager
API-demo
Iiro Laukkanen 2019
github.com/Kiiskil
http://fleetmanager.hopto.org/api/cars

Very basic RESTful API, made with .NET CORE 2.1

App can be setup on .NET Core-enabled server right away by copying contents of \fleetmanager\FleetManager\bin\Release\netcoreapp2.1\publish to server.
The MySQL-database have to be set up manually.

Seed data on database, in four tables, is:
	Four cars
		-carID: Int (1-4)
		-regno: String
		-year: Int
		-inspection_date: DATE ("YYYY-MM-DD")
		-modelID: Int (FK_modelID)
		-brandID: Int (FK_brandID)
		-motorID: Int (FK_motorID)

	Two brands
		-brandID: Int (1-2)
		-name: String
	Two models
		-modelID: Int (1-2)
		-name: String
	Three motors
		-motorID: Int (1-3)
		-motorsize: Double
		-power: Int


API returns serialized objects, formatted in JSON
API-calls:

GET {URL}/api/cars
	-Lists all cars in DB

GET {URL}/api/cars/{id}
	-Lists car with ID
	--GET localhost:5000/api/cars/1 -> returns one car whose id is 1

GET {URL}/api/cars/year/{from}/{to}
	-Lists all cars within that range of manufacturing years
	--GET localhost:5000/api/year/1910/1940

GET {URL}/api/cars/brand/{brand}/model/{model}
	-Lists all cars with that brand id and / or that model id. Zeros are ignored.
	--GET localhost:5000/api/brand/1/model/1 -> All cars with brand id = 1 and model id = 1
	--GET localhost:5000/api/brand/1/model/0 -> All cars with brand id = 1

POST {URL}/api/cars
	-Create a new car
	-Body of HTTP-query have to be JSON-formatted.
	{
        "regno": "DDD-111",
        "year": 1960,
        "inspectionDate": "1950-01-01",
        "modelID": 1,
        "motorID": 2,
        "brandID": 2
        }

PUT {URL}/api/cars/{id}
	-Change attributes of car with id
	-Body of HTTP-query have to be JSON-formatted, all fields required.

	{
	"id":4,
        "regno": "DDD-111",
        "year": 1960,
        "inspectionDate": "1950-01-01",
        "modelID": 1,
        "motorID": 2,
        "brandID": 2
        }

DELETE {URL}/api/cars/{id}
	-Delete car with id

API is running on fleetmanager.hopto.org, so it can be tested on that URL.

TODO: 
	-PUT-method currently needs all properties through query to work. If some properties are missing, use existing ones.
	-Add try-catches
	-Make same functionality using Fluent API
	-Sanitation of SQL-queries
