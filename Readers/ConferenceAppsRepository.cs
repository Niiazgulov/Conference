using Dapper;
using Domain;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readers
{
    public class ConferenceAppsRepository : IConferenceAppsRepository
    {
        private readonly IConfiguration _configuration;

        public ConferenceAppsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Applications> AddApps(NewAppDTO app)
        {
            var query = "INSERT INTO applications (id, author, activity, name, description, outline, datetime) VALUES (@id, @author, @activity, @name, @description, @outline, @datetime)";
            var parameters = new DynamicParameters();
            Guid newId = Guid.NewGuid();
            DateTime localDate = DateTime.Now;
            parameters.Add("author", app.Author, DbType.Guid);
            parameters.Add("id", newId, DbType.Guid);
            parameters.Add("activity", app.Activity, DbType.String);
            parameters.Add("name", app.Name, DbType.String);
            parameters.Add("description", app.Description, DbType.String);
            parameters.Add("outline", app.Outline, DbType.String);
            parameters.Add("datetime", localDate, DbType.DateTime2);

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                await connection.ExecuteAsync(query, parameters);
            }

            Applications newapp = new Applications() { Author = app.Author, Id = newId, Activity = app.Activity, Name = app.Name, Description = app.Description, Outline = app.Outline };
            
            return newapp;
        }

        public async Task<Applications> EditApps(Guid id, EditedAppDTO app)
        {
            var query = "UPDATE applications SET activity = COALESCE(@activity, activity), name = COALESCE(@name, name), description = COALESCE(@description, description), outline = COALESCE(@outline, outline), datetime = @datetime WHERE id = @id";
            DateTime localDate = DateTime.Now;
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("activity", app.Activity, DbType.String);
            parameters.Add("name", app.Name, DbType.String);
            parameters.Add("description", app.Description, DbType.String);
            parameters.Add("outline", app.Outline, DbType.String);
            parameters.Add("datetime", localDate, DbType.DateTime2);

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                await connection.ExecuteAsync(query, parameters);
            }

            var query2 = "SELECT id, author, activity, name, description, outline FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var editedApp = await connection.QuerySingleOrDefaultAsync<Applications>(query2, new { id });
                return editedApp;
            }
        }

        public async Task DeleteApps(Guid id)
        {
            var query = "DELETE FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                await connection.ExecuteAsync(query, new { id });
            }

        }
        public void AddAppsToReview(Guid id)
        {
            var connectionString = _configuration.GetConnectionString("NpgConnection");
        }
    }
}


/*
            var query = "SELECT * FROM Applications WHERE Id = @Id";

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                //public async Task<IEnumerable<Applications>> AddApps(Applications app)
                                //var apps = await connection.QueryAsync<Applications>(query);
                //return apps.ToList();

                var newapp = await connection.QuerySingleOrDefaultAsync<Applications>(query, new { app.Author });
                return newapp;
                //return connection.Query<Applications>("SELECT * FROM Books");
            }


            /*
            NpgsqlConnection sqlConnection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection"));
            sqlConnection.Open();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = CommandType.Text;
            sqlConnection.Close();
            
            return app;
            */