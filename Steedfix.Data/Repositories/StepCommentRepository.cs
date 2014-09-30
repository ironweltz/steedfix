using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class StepCommentRepository : IStepCommentRepository
    {
        private readonly SteedfixContext _context;

        public StepCommentRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<StepComment> All
        {
            get
            {
                return _context.Comments.OfType<StepComment>();
            }
        }

        public IQueryable<StepComment> AllIncluding(params System.Linq.Expressions.Expression<Func<StepComment, object>>[] includeProperties)
        {
            IQueryable<StepComment> query = _context.Likes.OfType<StepComment>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public StepComment Find(Guid id)
        {
            return _context.Comments.OfType<StepComment>().SingleOrDefault(s=>s.Id == id);
        }

        public void Insert(StepComment entity)
        {
            _context.Comments.Add(entity);
        }

        public void Update(StepComment entity)
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
