using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Tests.Fake
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();
        IEnumerable<TEntity> All();
        TEntity Get(int id);
        TEntity Get(Func<TEntity, bool> predicate);
        void Add(TEntity entity);
        void Attach(TEntity entity);
        void Delete(TEntity entity);
    }
}
