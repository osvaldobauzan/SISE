using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public  Task<List<T>> ExecuteStoredProc<T>(string storedProcName, SqlParameter[] paremeters = null);
    Task<(List<T>, List<MT>)> ExecuteStoredProc<T, MT>(string storedProcName, SqlParameter[] paremeters);
    Task<T> ExecuteStoredProcObj<T>(string v, SqlParameter[] sqlParameters);
    Task<(List<TA>, List<TB>, List<TC>)> ExecuteStoredProcThree<TA, TB, TC>(string storedProcName, SqlParameter[] paremeters);
    Task<(List<TA>, List<TB>)> ExecuteStoredProcTwo<TA, TB>(string storedProcName, SqlParameter[] paremeters);
    Task<(List<TA>, List<TB>, List<TC>, List<TD>)> ExecuteStoredProcFour<TA, TB, TC, TD>(string storedProcName, SqlParameter[] paremeters);
    Task<(List<TA>, List<TB>, List<TC>, List<TD>, List<TE>, List<TF>, List<TG>)> ExecuteStoredProcSeven<TA, TB, TC, TD, TE, TF, TG>(string storedProcName, SqlParameter[] paremeters);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> ExecuteStoredProcNonQuery(string storedProcName, SqlParameter[] paremeters);
}
