using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using TrainSystem.Extensions;
using TrainSystem.Models;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.DataViews.Newsletter;

[Route("News/News")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly NewsRepository _newsRepository;

    public NewsController(NewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var newsList = _newsRepository.GetAll();

        return Ok(newsList);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var newsItem = _newsRepository.GetById(id);

        if (newsItem == null)
            return NotFound();

        return Ok(newsItem);
    }

    [HttpPost]
    public IActionResult Create([FromBody] NewsCreateVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newNews = new News
        {
            Title = model.Title,
            CreationDate = model.CreationDate,
            Text = model.Text,
            Author = model.Author,
            StationId = model.StationId
        };

        _newsRepository.Save(newNews);

        return CreatedAtAction(nameof(GetById), new { id = newNews.Id }, newNews);
    }

    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] NewsEditVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingNews = _newsRepository.GetById(id);

        if (existingNews == null)
            return NotFound();

        existingNews.Title = model.Title;
        existingNews.CreationDate = model.CreationDate;
        existingNews.Text = model.Text;
        existingNews.Author = model.Author;
        existingNews.StationId = model.StationId;

        _newsRepository.Save(existingNews);

        return Ok(existingNews);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var newsItem = _newsRepository.GetById(id);

        if (newsItem == null)
            return NotFound();

        _newsRepository.Delete(newsItem);

        return NoContent();
    }
}
