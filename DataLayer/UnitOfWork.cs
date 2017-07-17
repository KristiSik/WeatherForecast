using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WeatherForecastContext _context;

        public IUserRepository Users { get; private set; }
        public ICityRepository DefaultCities { get; private set; }
        public IRequestRepository Requests { get; set; }

        public UnitOfWork()
        {
            _context = new WeatherForecastContext();
            Users = new UserRepository(_context);
            DefaultCities = new CityRepository(_context);
            Requests = new RequestRepository(_context);
        }
        public UnitOfWork(WeatherForecastContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            DefaultCities = new CityRepository(_context);
            Requests = new RequestRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
