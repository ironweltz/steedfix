using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class JobRatingRepository : IJobRatingRepository
    {
        private readonly SteedfixContext _context;

        public JobRatingRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<JobRating> All
        {
            get
            {
                return _context.Ratings.OfType<JobRating>();
            }
        }

        public IQueryable<JobRating> AllIncluding(params System.Linq.Expressions.Expression<Func<JobRating, object>>[] includeProperties)
        {
            IQueryable<JobRating> query = _context.Ratings.OfType<JobRating>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public JobRating Find(Guid id)
        {
            return _context.Ratings.OfType<JobRating>().SingleOrDefault(b=>b.Id == id);
        }

        public void Insert(JobRating entity)
        {
            _context.Ratings.Add(entity);
        }

        public void Update(JobRating entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var jobRating = _context.Ratings.Find(id);
            _context.Ratings.Remove(jobRating);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: add some logging here....
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
