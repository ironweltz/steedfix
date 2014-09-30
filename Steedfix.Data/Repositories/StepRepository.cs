using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class StepRepository : IStepRepository
    {
        private readonly SteedfixContext _context;

        public StepRepository()
        {
            _context = new SteedfixContext();
        }
        public IQueryable<Step> All
        {
            get { return _context.Steps; }
        }

        public IQueryable<Step> AllIncluding(params System.Linq.Expressions.Expression<Func<Step, object>>[] includeProperties)
        {
            IQueryable<Step> query = _context.Steps;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Step Find(Guid id)
        {
            return _context.Steps.Find(id);
        }

        public void Insert(Step entity)
        {
            _context.Steps.Add(entity);
        }

        public void Update(Step entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = _context.Steps.Find(id);
            _context.Steps.Remove(entity);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //TODO: Add logging here
                throw;
            }
            ;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
