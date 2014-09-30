using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly SteedfixContext _context;

        public PartRepository()
        {
            _context = new SteedfixContext();
        }
        public IQueryable<Part> All
        {
            get { return _context.Items.OfType<Part>(); }
        }

        public IQueryable<Part> AllIncluding(params System.Linq.Expressions.Expression<Func<Part, object>>[] includeProperties)
        {
            IQueryable<Part> query = _context.Items.OfType<Part>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Part Find(Guid id)
        {
            return _context.Items.OfType<Part>().SingleOrDefault(p=>p.Id == id);
        }

        public void Insert(Part entity)
        {
            _context.Items.Add(entity);
        }

        public void Update(Part entity)
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
