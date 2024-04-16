using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainSystem.Entities;

namespace TrainSystem.Repositories
{
    public class TicketsRepository : BaseRepository<Ticket>
    {
        public List<Ticket> GetAll(Expression<Func<Ticket, bool>> filter = null)
        {
            IQueryable<Ticket> query = Items.Include(x => x.User).Include(x => x.Trip);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public Ticket FirstOrDefault(Expression<Func<Ticket, bool>> filter)
        {
            return Items.Include(x => x.User).Include(x => x.Trip).FirstOrDefault(filter);
        }

        public Ticket GetById(int id)
        {
            return Items.Include(x => x.User).Include(x => x.Trip).FirstOrDefault(u => u.Id == id);
        }
    }
}
