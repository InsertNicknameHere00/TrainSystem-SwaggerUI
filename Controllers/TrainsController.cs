using Microsoft.AspNetCore.Mvc;
using TrainSystem.Repositories;
using TrainSystem.Entities;
using TrainSystem.ViewModels.DataViews.Trains;

[Route("Trains/Trains")]
[ApiController]
public class TrainsController : ControllerBase
{
    private readonly TrainsRepository _trainsRepository;

    public TrainsController(TrainsRepository trainsRepository)
    {
        _trainsRepository = trainsRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var trainsList = _trainsRepository.GetAll();
        return Ok(trainsList);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var train = _trainsRepository.GetById(id);
        if (train == null)
            return NotFound();
        return Ok(train);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TrainsCreateVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newTrain = new Train
        {
            TrainNum = model.TrainNum,
            TrainType = model.TrainType,
            Capacity = model.Capacity
        };

        _trainsRepository.Save(newTrain);

        return CreatedAtAction(nameof(GetById), new { id = newTrain.Id }, newTrain);
    }

    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] TrainsEditVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingTrain = _trainsRepository.GetById(id);
        if (existingTrain == null)
            return NotFound();

        existingTrain.TrainNum = model.TrainNum;
        existingTrain.TrainType = model.TrainType;
        existingTrain.Capacity = model.Capacity;

        _trainsRepository.Save(existingTrain);

        return Ok(existingTrain);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var train = _trainsRepository.GetById(id);
        if (train == null)
            return NotFound();

        _trainsRepository.Delete(train);
        return NoContent();
    }
}
