using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly SteedfixContext _context;

        public JobRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<Job> All
        {
            get
            {
                IQueryable<Job> query = _context.Jobs
                    .Include(p=>p.Image)
                    .Include(p=>p.Likes)
                    .Include(p=>p.Ratings)
                    .Include(p=>p.CreatedByUser);

                return query;
            }
        }

        public IQueryable<Job> AllIncluding(params System.Linq.Expressions.Expression<Func<Domain.Job, object>>[] includeProperties)
        {
            IQueryable<Job> query = _context.Jobs;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Job Find(Guid id)
        {
            return _context.Jobs.Find(id);
        }

        public void Insert(Job entity)
        {
            _context.Jobs.Add(entity);
        }

        public void Update(Job entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var job = _context.Jobs.Find(id);
            _context.Jobs.Remove(job);
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
