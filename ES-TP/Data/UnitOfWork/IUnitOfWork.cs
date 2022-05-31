using System.Threading.Tasks;

namespace ES_TP.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region "repositories"
        //IApplicationRepository<Log> LogRepository { get; }
        #endregion

        int Commit();
        void Rollback();
        Task<int> CommitAsync();
        Task RollbackAsync();
        void Dispose();
        void DisposeAsync();
    }
}
