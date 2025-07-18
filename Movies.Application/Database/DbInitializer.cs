﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Database
{
    public class DbInitializer
    {
        public readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAync();
            await connection.ExecuteAsync("""
               create table if not exists movies (
                   id UUID primary key,
                   title TEXT not null,
                   slug TEXT not null,
                   YearOfRelease integer not null);
               """);
            await connection.ExecuteAsync("""
               create unique index concurrently if not exists movies_slug_idx on movies using btree(slug);
               """);
            await connection.ExecuteAsync("""
                create table if not exists genres(
                movieId UUID references movies (id),
                name TEXT not null);
                """);
        }
    }
}
