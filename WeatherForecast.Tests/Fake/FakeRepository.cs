using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecast.Tests.Fake
{ 
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly List<TEntity> Context;
        public FakeRepository(params TEntity[] context)
        {
            Context = new List<TEntity>(context);
        }
        public IQueryable<TEntity> Query()
        {
            return Context.AsQueryable();
        }
        public IEnumerable<TEntity> All()
        {
            return Context;
        }
        public TEntity Get(int id)
        {
            return null;
        }
        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return Context.FirstOrDefault(predicate);
        }
        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }
        public void Attach(TEntity entity)
        {
        }
        public void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }
    }
}