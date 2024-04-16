using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using System.Diagnostics;
using TrainSystem.Extensions;
using TrainSystem.Models;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using TrainSystem.ViewModels.DataViews.Trips;

namespace TrainSystem.Controllers
{
	[Route("Users/Users")]
	[ApiController]
	public class UsersController: ControllerBase
    {
		private readonly UsersRepository _usersRepository;

		public UsersController(UsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			UsersIndexVM model = new UsersIndexVM
			{
				Items = _usersRepository.GetAll()
			};

			return Ok(model);
		}

		[HttpPost]
		public IActionResult CreateUser([FromBody] UsersCreateVM model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			User newUser = new User
			{
				Username = model.Username,
				Password = model.Password,
				Firstname = model.Firstname,
				Lastname = model.Lastname,
				Email = model.Email
			};

			_usersRepository.Save(newUser);

			return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, newUser);
		}

		[HttpGet("{id}")]
		public IActionResult GetUser(int id)
		{
			User user = _usersRepository.GetById(id);

			if (user == null)
				return NotFound();

			return Ok(user);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUser(int id, [FromBody] UsersEditVM model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			User userToUpdate = _usersRepository.GetById(id);

			if (userToUpdate == null)
				return NotFound();

			userToUpdate.Username = model.Username;
			userToUpdate.Password = model.Password;
			userToUpdate.Firstname = model.Firstname;
			userToUpdate.Lastname = model.Lastname;
			userToUpdate.Email = model.Email;

			_usersRepository.Save(userToUpdate);

			return Ok(userToUpdate);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			User userToDelete = _usersRepository.GetById(id);

			if (userToDelete == null)
				return NotFound();

			_usersRepository.Delete(userToDelete);

			return NoContent();
		}
	}
}
