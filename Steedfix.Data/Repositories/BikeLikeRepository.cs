using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class BikeLikeRepository : IBikeLikeRepository
    {
        private readonly SteedfixContext _context;

        public BikeLikeRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<BikeLike> All
        {
            get
            {
                return _context.Likes.OfType<BikeLike>();
            }
        }

        public IQueryable<BikeLike> AllIncluding(params System.Linq.Expressions.Expression<Func<BikeLike, object>>[] includeProperties)
        {
            IQueryable<BikeLike> query = _context.Likes.OfType<BikeLike>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public BikeLike Find(Guid id)
        {
            return _context.Likes.OfType<BikeLike>().SingleOrDefault(b=>b.Id == id);
        }

        public void Insert(BikeLike entity)
        {
            _context.Likes.Add(entity);
        }

        public void Update(BikeLike entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var bikeLike = _context.Likes.Find(id);
            _context.Likes.Remove(bikeLike);
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
