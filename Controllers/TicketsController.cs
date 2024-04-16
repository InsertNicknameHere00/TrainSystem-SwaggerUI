using Microsoft.AspNetCore.Mvc;
using TrainSystem.Entities;
using TrainSystem.Repositories;
using TrainSystem.ViewModels.DataViews.Tickets;

namespace TrainSystem.Controllers
{
    [Route("Tickets/Tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsRepository _ticketRepository;

        public TicketsController(TicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            var tickets = _ticketRepository.GetAll();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public IActionResult GetTicketById(int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult CreateTicket([FromBody] TicketsCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTicket = new Ticket
            {
                RouteId = model.RouteId,
                Price = model.Price,
                UserId = model.UserId
            };

            _ticketRepository.Save(newTicket);

            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, newTicket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, [FromBody] TicketsEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticketToUpdate = _ticketRepository.GetById(id);
            if (ticketToUpdate == null)
            {
                return NotFound();
            }

            ticketToUpdate.RouteId = model.RouteId;
            ticketToUpdate.Price = model.Price;
            ticketToUpdate.UserId = model.UserId;

            _ticketRepository.Save(ticketToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var ticketToDelete = _ticketRepository.GetById(id);
            if (ticketToDelete == null)
            {
                return NotFound();
            }

            _ticketRepository.Delete(ticketToDelete);

            return NoContent();
        }
    }
}
