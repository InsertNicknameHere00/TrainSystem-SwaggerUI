using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using TrainSystem.Extensions;
using TrainSystem.Models;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.DataViews.Stations;

[Route("Stations/Stations")]
[ApiController]
public class StationsController : ControllerBase
{
    private readonly StationsRepository _stationsRepository;

    public StationsController(StationsRepository stationsRepository)
    {
        _stationsRepository = stationsRepository;
    }

    [HttpGet]
    public IActionResult GetStations()
    {
        StationsIndexVM model = new StationsIndexVM
        {
            Items = _stationsRepository.GetAll()
        };

        return Ok(model);
    }

    [HttpPost]
    public IActionResult CreateStation([FromBody] StationsCreateVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Station newStation = new Station
        {
            Location = model.Location
        };

        _stationsRepository.Save(newStation);

        return CreatedAtAction(nameof(GetStations), new { id = newStation.Id }, newStation);
    }

    [HttpGet("{id}")]
    public IActionResult GetStation(int id)
    {
        Station station = _stationsRepository.GetById(id);

        if (station == null)
            return NotFound();

        return Ok(station);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStation(int id, [FromBody] StationsEditVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Station stationToUpdate = _stationsRepository.GetById(id);

        if (stationToUpdate == null)
            return NotFound();

        stationToUpdate.Location = model.Location;

        _stationsRepository.Save(stationToUpdate);

        return Ok(stationToUpdate);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStation(int id)
    {
        Station stationToDelete = _stationsRepository.GetById(id);

        if (stationToDelete == null)
            return NotFound();

        _stationsRepository.Delete(stationToDelete);

        return NoContent();
    }
}
