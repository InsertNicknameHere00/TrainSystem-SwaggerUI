using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainSystem.Entities;

namespace TrainSystem.Repositories
{
    public class TripsRepository : BaseRepository<Trip>
    {
        public List<Trip> GetAll(Expression<Func<Trip, bool>> filter = null)
        {
            IQueryable<Trip> query = Items.Include(x => x.StartStation).Include(x => x.EndStation).Include(x => x.Train);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public Trip FirstOrDefault(Expression<Func<Trip, bool>> filter)
        {
            return Items.Include(x => x.StartStation).Include(x => x.EndStation).Include(x => x.Train).FirstOrDefault(filter);
        }

        public Trip GetById(int id)
        {
            return Items.Include(x => x.StartStation).Include(x => x.EndStation).Include(x => x.Train).FirstOrDefault(u => u.Id == id);
        }
    }
}
