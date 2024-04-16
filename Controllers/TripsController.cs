using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.DataViews.Trips;

namespace TrainSystem.Controllers
{
	[Route("Trips/Trips")]
	[ApiController]
	public class TripsController: ControllerBase
    {
		private readonly TripsRepository _tripsRepository;

		public TripsController(TripsRepository tripsRepository)
		{
            _tripsRepository = tripsRepository;
		}

		[HttpGet]
		public IActionResult GetTrips()
		{
			TripsIndexVM model = new TripsIndexVM
			{
				Items = _tripsRepository.GetAll()
			};

			return Ok(model);
		}

		[HttpPost]
		public IActionResult CreateTrip([FromBody] TripsCreateVM model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

            Trip newTrip = new Trip
			{
                TrainId = model.TrainId,
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                ArrivalTime = model.ArrivalTime,
                DepartureTime = model.DepartureTime
			};

			_tripsRepository.Save(newTrip);

			return CreatedAtAction(nameof(GetTrips), new { id = newTrip.Id }, newTrip);
		}

		[HttpGet("{id}")]
		public IActionResult GetTrip(int id)
		{
			Trip trip = _tripsRepository.GetById(id);

			if (trip == null)
				return NotFound();

			return Ok(trip);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateTrip(int id, [FromBody] TripsEditVM model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			Trip tripToUpdate = _tripsRepository.GetById(id);

			if (tripToUpdate == null)
				return NotFound();

			tripToUpdate.TrainId = model.TrainId;
			tripToUpdate.StartPoint = model.StartPoint;
			tripToUpdate.EndPoint = model.EndPoint;
			tripToUpdate.ArrivalTime = model.ArrivalTime;
			tripToUpdate.DepartureTime = model.DepartureTime;

			_tripsRepository.Save(tripToUpdate);

			return Ok(tripToUpdate);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteTrip(int id)
		{
			Trip tripToDelete = _tripsRepository.GetById(id);

			if (tripToDelete == null)
				return NotFound();

			_tripsRepository.Delete(tripToDelete);

			return NoContent();
		}

	}
}
