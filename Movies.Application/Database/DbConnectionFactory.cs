﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public  interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAync();
    }

    public class NpgSqlConnectionFactory : IDbConnectionFactory
    {
        public readonly string _connectionString;

        public NpgSqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAync()
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
