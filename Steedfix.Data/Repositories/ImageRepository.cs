using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly SteedfixContext _context;

        public ImageRepository()
        {
            _context = new SteedfixContext();
        }

        public IQueryable<Image> All
        {
            get
            {
                return _context.Images;
            }
        }

        public IQueryable<Image> AllIncluding(params System.Linq.Expressions.Expression<Func<Image, object>>[] includeProperties)
        {
            IQueryable<Image> query = _context.Images;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Image Find(Guid id)
        {
            return _context.Images.Find(id);
        }

        public void Insert(Image entity)
        {
            _context.Images.Add(entity);
        }

        public void Update(Image entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var image = _context.Images.Find(id);
            _context.Images.Remove(image);
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
