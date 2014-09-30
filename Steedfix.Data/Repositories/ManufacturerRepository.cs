using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly SteedfixContext _context;

        public ManufacturerRepository()
        {
            _context = new SteedfixContext();
        }
        public IQueryable<Manufacturer> All
        {
            get { return _context.Manufacturers; }
        }

        public IQueryable<Manufacturer> AllIncluding(params System.Linq.Expressions.Expression<Func<Manufacturer, object>>[] includeProperties)
        {
            IQueryable<Manufacturer> query = _context.Manufacturers;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Manufacturer Find(Guid id)
        {
            return _context.Manufacturers.Find(id);
        }

        public void Insert(Manufacturer entity)
        {
            _context.Manufacturers.Add(entity);
        }

        public void Update(Manufacturer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = _context.Manufacturers.Find(id);
            _context.Manufacturers.Remove(entity);
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
