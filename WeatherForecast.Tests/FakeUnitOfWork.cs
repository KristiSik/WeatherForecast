using WeatherForecast.Tests.FakeRepository;

namespace WeatherForecast.Tests
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly FakeContext _context;

        public IUserRepository Users { get; private set; }

        public FakeUnitOfWork()
        {
            _context = new FakeContext();
            Users = new UserRepository(_context);
        }
        public FakeUnitOfWork(FakeContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
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
