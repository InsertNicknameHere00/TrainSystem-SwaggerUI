using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrainSystem.Entities;

namespace TrainSystem.Repositories
{
    public class NewsRepository : BaseRepository<News>
    {
        public List<News> GetAll(Expression<Func<News, bool>> filter = null)
        {
            IQueryable<News> query = Items.Include(x => x.Station);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public News FirstOrDefault(Expression<Func<News, bool>> filter)
        {
            return Items.Include(x => x.Station).FirstOrDefault(filter);
        }

        public News GetById(int id)
        {
            return Items.Include(x => x.Station).FirstOrDefault(u => u.Id == id);
        }
    }
}
