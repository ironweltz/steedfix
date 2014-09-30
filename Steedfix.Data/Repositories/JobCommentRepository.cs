using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class JobCommentRepository : IJobCommentRepository
    {
        private readonly SteedfixContext _context;

        public JobCommentRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<JobComment> All
        {
            get
            {
                return _context.Comments.OfType<JobComment>();
            }
        }

        public IQueryable<JobComment> AllIncluding(params System.Linq.Expressions.Expression<Func<JobComment, object>>[] includeProperties)
        {
            IQueryable<JobComment> query = _context.Likes.OfType<JobComment>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public JobComment Find(Guid id)
        {
            return _context.Comments.OfType<JobComment>().SingleOrDefault(j=>j.Id == id);
        }

        public void Insert(JobComment entity)
        {
            _context.Comments.Add(entity);
        }

        public void Update(JobComment entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = _context.Comments.Find(id);
            _context.Comments.Remove(entity);
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
