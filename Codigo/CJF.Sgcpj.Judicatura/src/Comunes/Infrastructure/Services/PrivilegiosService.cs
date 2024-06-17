using System.Data;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class PrivilegiosService : IPrivilegiosService
{
    private readonly IConfiguration _configuration;

    public PrivilegiosService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<Privilegio>> ObtenerConfiguracionPrivilegios()
    {
        var privilegios = new List<Privilegio>();
        try
        {
            var connectionString = _configuration["SISE3:BackEnd:SISEDBConnStr"];
            var storedProcedureName = "[SISE3].[pcPrivilegiosXApi]";

            // Consulta y mapeo
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Privilegio privilegio = new Privilegio
                                {
                                    IdPrivilegio = Convert.ToInt32(reader["IdPrivilegio"]),
                                    Api = reader["sURL"] == DBNull.Value ? string.Empty : reader["sURL"].ToString(),
                                    Verbo = reader["sVerbo"] == DBNull.Value ? string.Empty : reader["sVerbo"].ToString(),
                                };

                                privilegios.Add(privilegio);
                            }
                        }
                    }
                }
                catch 
                {
                    throw;
                }
                finally { connection.Close(); }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return privilegios;
    }
}
