using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class JobLikeRepository : IJobLikeRepository
    {
        private readonly SteedfixContext _context;

        public JobLikeRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<JobLike> All
        {
            get
            {
                return _context.Likes.OfType<JobLike>();
            }
        }

        public IQueryable<JobLike> AllIncluding(params System.Linq.Expressions.Expression<Func<JobLike, object>>[] includeProperties)
        {
            IQueryable<JobLike> query = _context.Likes.OfType<JobLike>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public JobLike Find(Guid id)
        {
            return _context.Likes.OfType<JobLike>().SingleOrDefault(b=>b.Id == id);
        }

        public void Insert(JobLike entity)
        {
            _context.Likes.Add(entity);
        }

        public void Update(JobLike entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var jobLike = _context.Likes.Find(id);
            _context.Likes.Remove(jobLike);
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
