using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using SSResurrezioneBR.Models.Exceptions;
using SSResurrezioneBR.Models.Exceptions.Infrastructure;
using SSResurrezioneBR.Models.Options;

namespace SSResurrezioneBR.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : ISqliteDatabaseAccessor
    {
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions;
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public SqliteDatabaseAccessor(ILogger<SqliteDatabaseAccessor> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions)
        {
            this.logger = logger;
            this.connectionStringsOptions = connectionStringsOptions;
        }

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());

            using SqliteConnection conn = await GetOpenedConnection();
            using SqliteCommand cmd = GetCommand(formattableQuery, conn);

            using SqliteDataReader dataReader = await cmd.ExecuteReaderAsync();
            DataSet dataSet = new DataSet();

            // creazione di tanti DataTable per quante sono le tabelle
            // di risultati trovate dal SqliteDataReader
            do
            {
                DataTable dataTable = new DataTable();
                dataSet.Tables.Add(dataTable);
                dataTable.Load(dataReader);
            } while (!dataReader.IsClosed);

            return dataSet;
        }

        private static SqliteCommand GetCommand(FormattableString formattableQuery, SqliteConnection conn)
        {
            // creazione SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i] ?? DBNull.Value);
                sqliteParameters.Add(parameter);
                queryArguments[i] = $"@{i}";
            }
            string query = formattableQuery.ToString();

            var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddRange(sqliteParameters);
            return cmd;
        }

        private async Task<SqliteConnection> GetOpenedConnection()
        {
            var conn = new SqliteConnection(connectionStringsOptions.CurrentValue.Default);
            await conn.OpenAsync();
            return conn;
        }

        public async Task<T> QueryScalarAsync<T>(FormattableString formattableQuery)
        {
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());
            using SqliteConnection conn = await GetOpenedConnection();
            using SqliteCommand cmd = GetCommand(formattableQuery, conn);
            object result = await cmd.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T))!;
        }
        public async Task<int> CommandAsync(FormattableString formattableCommand)
        {
            try
            {
                logger.LogInformation(formattableCommand.Format, formattableCommand.GetArguments());
                using SqliteConnection conn = await GetOpenedConnection();
                using SqliteCommand cmd = GetCommand(formattableCommand, conn);
                int affectedRows = await cmd.ExecuteNonQueryAsync();
                return affectedRows;
            }
            catch (SqliteException exc) when (exc.SqliteErrorCode == 19)
            {
                throw new ConstraintViolationException(exc);
            }
        }
    }
}