using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Exceptions;
using System.Data.Entity;

namespace DataLayer.Repositories
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(WeatherForecastContext context) : base(context)
        {
        }
        public IEnumerable<Request> GetAllRequests()
        {
            return Db.Requests.ToList();
        }
        public WeatherForecastContext Db
        {
            get { return Context as WeatherForecastContext; }
        }
    }
}
