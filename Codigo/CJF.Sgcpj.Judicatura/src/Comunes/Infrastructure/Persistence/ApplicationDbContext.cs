using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Reflection;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    [Table("PersonaAsunto_type")]
    public class PersonaAsuntoType
    {
        [Key]
        public int AsuntoId { get; set; }
        public long AsuntoNeunId { get; set; }
        public long UsuarioCaptura { get; set; }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    private List<T> MapToList<T>(DbDataReader dr)
    {
        var objList = new List<T>();
        var props = typeof(T).GetRuntimeProperties();

        var colMapping = dr.GetColumnSchema()
          .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
          .ToDictionary(key => key.ColumnName.ToLower());

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                T obj = Activator.CreateInstance<T>();
                foreach (var prop in props)
                {
                    if (colMapping.ContainsKey(prop.Name.ToLower()))
                    {
                        var val =
                     dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);

                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }

                }
                objList.Add(obj);
            }
        }
        return objList;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<T>> ExecuteStoredProc<T>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    return MapToList<T>(reader);
                }
            }
           catch (Exception e)
            {
                if (e is SqlException sqlEx && sqlEx.Number >= 50000 && sqlEx.Number <= 51999)
                {
                    throw new RuleException(sqlEx.Message);
                }
                else if (e.InnerException is SqlException sqlExI && sqlExI.Number >= 50000 && sqlExI.Number <= 51999)
                {
                    throw new RuleException(sqlExI.Message);
                }
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }


    public async Task<(List<T>, List<MT>)> ExecuteStoredProc<T, MT>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            List<T> MapedListT;
            List<MT> MapedListMT;

            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();

            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    MapedListT = MapToList<T>(reader);
                    reader.NextResult();
                    MapedListMT = MapToList<MT>(reader);

                    return (MapedListT, MapedListMT);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    public async Task<(List<TA>, List<TB>, List<TC>)> ExecuteStoredProcThree<TA, TB, TC>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            List<TA> MapedListTA;
            List<TB> MapedListTB;
            List<TC> MapedListTC;

            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    MapedListTA = MapToList<TA>(reader);
                    reader.NextResult();
                    MapedListTB = MapToList<TB>(reader);
                    reader.NextResult();
                    MapedListTC = MapToList<TC>(reader);
                    return (MapedListTA, MapedListTB, MapedListTC);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    public async Task<(List<TA>, List<TB>)> ExecuteStoredProcTwo<TA, TB>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            List<TA> MapedListTA;
            List<TB> MapedListTB;

            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    MapedListTA = MapToList<TA>(reader);
                    reader.NextResult();
                    MapedListTB = MapToList<TB>(reader);
                    reader.NextResult();
                    return (MapedListTA, MapedListTB);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    public async Task<(List<TA>, List<TB>, List<TC>, List<TD>)> ExecuteStoredProcFour<TA, TB, TC, TD>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            List<TA> MapedListTA;
            List<TB> MapedListTB;
            List<TC> MapedListTC;
            List<TD> MapedListTD;

            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    MapedListTA = MapToList<TA>(reader);
                    reader.NextResult();
                    MapedListTB = MapToList<TB>(reader);
                    reader.NextResult();
                    MapedListTC = MapToList<TC>(reader);
                    reader.NextResult();
                    MapedListTD = MapToList<TD>(reader);
                    return (MapedListTA, MapedListTB, MapedListTC, MapedListTD);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
    public async Task<(List<TA>, List<TB>, List<TC>, List<TD>, List<TE>, List<TF>, List<TG>)> ExecuteStoredProcSeven<TA, TB, TC, TD, TE, TF, TG>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            List<TA> MapedListTA;
            List<TB> MapedListTB;
            List<TC> MapedListTC;
            List<TD> MapedListTD;
            List<TE> MapedListTE;
            List<TF> MapedListTF;
            List<TG> MapedListTG;

            SetStoreAndParams(storedProcName, paremeters, cmd);

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    MapedListTA = MapToList<TA>(reader);
                    reader.NextResult();
                    MapedListTB = MapToList<TB>(reader);
                    reader.NextResult();
                    MapedListTC = MapToList<TC>(reader);
                    reader.NextResult();
                    MapedListTD = MapToList<TD>(reader);
                    reader.NextResult();
                    MapedListTE = MapToList<TE>(reader);
                    reader.NextResult();
                    MapedListTF = MapToList<TF>(reader);
                    reader.NextResult();
                    MapedListTG = MapToList<TG>(reader);

                    return (MapedListTA, MapedListTB, MapedListTC, MapedListTD, MapedListTE, MapedListTF, MapedListTG);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    private static void SetStoreAndParams(string storedProcName, SqlParameter[] paremeters, DbCommand cmd)
    {
        cmd.CommandText = storedProcName;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        if (string.IsNullOrEmpty(cmd.CommandText))
            throw new InvalidOperationException(
              "Call LoadStoredProc before using this method");

        if (paremeters is null || !paremeters.Any())
        {
            return;
        }

        foreach (var parameter in paremeters)
        {
            cmd.Parameters.Add(parameter);
        }
    }

    public async Task<T> ExecuteStoredProcObj<T>(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            foreach (var parameter in paremeters)
            {
                cmd.Parameters.Add(parameter);
            }

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                var readData = await cmd.ExecuteScalarAsync();
                return (T)Convert.ChangeType(readData, typeof(T));
            }
            catch (Exception e)
            {
                if (e is SqlException sqlEx && sqlEx.Number >= 50000 && sqlEx.Number <= 51999)
                {
                    throw new RuleException(sqlEx.Message);
                }
                else if (e.InnerException is SqlException sqlExI && sqlExI.Number >= 50000 && sqlExI.Number <= 51999)
                {
                    throw new RuleException(sqlExI.Message);
                }

                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }

    public async Task<int> ExecuteStoredProcNonQuery(string storedProcName, SqlParameter[] paremeters)
    {
        using (var cmd = Database.GetDbConnection().CreateCommand())
        {
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");

            foreach (var parameter in paremeters)
            {
                cmd.Parameters.Add(parameter);
            }

            if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                cmd.Connection.Open();
            try
            {
                var rows = cmd.ExecuteNonQuery();

                return rows;
            }
            catch (Exception e)
            {
                if (e is SqlException sqlEx && sqlEx.Number >= 50000 && sqlEx.Number <= 51999)
                {
                    throw new RuleException(sqlEx.Message);
                }
                else if (e.InnerException is SqlException sqlExI && sqlExI.Number >= 50000 && sqlExI.Number <= 51999)
                {
                    throw new RuleException(sqlExI.Message);
                }

                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}

