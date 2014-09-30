using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly SteedfixContext _context;

        public BikeRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<Bike> All
        {
            get
            {
                return _context.Bikes;
            }
        }

        public IQueryable<Bike> AllIncluding(params System.Linq.Expressions.Expression<Func<Bike, object>>[] includeProperties)
        {
            IQueryable<Bike> query = _context.Bikes;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Bike Find(Guid id)
        {
            return _context.Bikes.Find(id);
        }

        public void Insert(Bike entity)
        {
            _context.Bikes.Add(entity);
        }

        public void Update(Bike entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var Bike = _context.Bikes.Find(id);
            _context.Bikes.Remove(Bike);
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
