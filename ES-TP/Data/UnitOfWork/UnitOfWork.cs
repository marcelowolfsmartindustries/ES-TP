using System;
using System.Threading.Tasks;

namespace ES_TP.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _databaseContext;

        //private IApplicationRepository<Log> _logRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _databaseContext = dbContext;
        }


        #region "repositories"
        //public IApplicationRepository<Log> LogRepository
        //{
        //    get { return _logRepository = _logRepository ?? new ApplicationRepository<Log>(_databaseContext); }
        //}
        #endregion

        public int Commit() => _databaseContext.SaveChanges();
        public void Rollback() => _databaseContext.Dispose();
        public async Task<int> CommitAsync() => await _databaseContext.SaveChangesAsync();
        public async Task RollbackAsync() => await _databaseContext.DisposeAsync();

        public void Dispose()
        {
            _databaseContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void DisposeAsync()
        {
            _databaseContext?.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
