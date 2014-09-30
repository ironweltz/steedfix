using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly SteedfixContext _context;

        public ToolRepository()
        {
            _context = new SteedfixContext();
        }
        public IQueryable<Tool> All
        {
            get { return _context.Items.OfType<Tool>(); }
        }

        public IQueryable<Tool> AllIncluding(params System.Linq.Expressions.Expression<Func<Tool, object>>[] includeProperties)
        {
            IQueryable<Tool> query = _context.Items.OfType<Tool>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Tool Find(Guid id)
        {
            return _context.Items.OfType<Tool>().SingleOrDefault(t => t.Id == id);
        }

        public void Insert(Tool entity)
        {
            _context.Items.Add(entity);
        }

        public void Update(Tool entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = Find(id);
            _context.Items.Remove(entity);
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
