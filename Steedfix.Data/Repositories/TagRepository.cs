using Steedfix.Data.Interfaces;
using Steedfix.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace Steedfix.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SteedfixContext _context;

        public TagRepository()
        {
            _context = new SteedfixContext();
        }
        public IQueryable<Tag> All
        {
            get { return _context.Tags; }
        }

        public IQueryable<Tag> AllIncluding(params System.Linq.Expressions.Expression<Func<Tag, object>>[] includeProperties)
        {
            IQueryable<Tag> query = _context.Tags;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Tag Find(Guid id)
        {
            return _context.Tags.Find(id);
        }

        public void Insert(Tag entity)
        {
            _context.Tags.Add(entity);
        }

        public void Update(Tag entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = _context.Tags.Find(id);
            _context.Tags.Remove(entity);
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
