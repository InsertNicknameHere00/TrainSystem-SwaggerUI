using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using TrainSystem.Extensions;
using TrainSystem.Models;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.Home;
using TrainSystem.ViewModels.Users;

[Route("Home/Index")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly UsersRepository _userRepository;

    public HomeController(UsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
    
        var users = _userRepository.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = _userRepository.GetById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginVM model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        User loggedUser = _userRepository.FirstOrDefault(u =>
            u.Username == model.Username && u.Password == model.Password);

        if (loggedUser == null)
            return BadRequest("Authentication failed!");

        this.HttpContext.Session.SetObject("loggedUser", loggedUser);

        return Ok(loggedUser);
    }




    [HttpPost("logout")]
    public IActionResult Logout()
    {
        this.HttpContext.Session.Remove("loggedUser");
        return Ok(new { message = "Logout successful" });
    }
}
