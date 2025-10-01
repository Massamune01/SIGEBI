using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SIGEBI.Infraestructure.Data.Configuration
{
    public class HelperDb
    {
        private readonly string _connectionString;

        public HelperDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> ExecuteReaderAsync<T>(
        string storedProcedure,
        Func<SqlDataReader, T> map)
        {
            var result = new List<T>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(storedProcedure, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(map(reader));
            }

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(
            string storedProcedure,
            IEnumerable<SqlParameter> parameters)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(storedProcedure, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddRange(parameters.ToArray());
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
        }
    }
}