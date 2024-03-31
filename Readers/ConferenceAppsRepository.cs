using Dapper;
using Domain;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
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

        public async Task<Applications> EmptyApp()
        {
            Applications newapp = new Applications() { };

            return newapp;
        }

        public async Task<bool> CheckUserById(Guid author)
        {
            var query = "SELECT EXISTS (SELECT * FROM applications WHERE author = @author)";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<Boolean>(query, new { author });
                Console.WriteLine(requestedApp);
                if (requestedApp == true)
                {
                    return true;
                }

                return false;
            }

        }

            public async Task<Applications> AddApps(NewAppDTO app)
        {
            var query = "INSERT INTO applications (id, author, activity, name, description, outline, datetime, sended) VALUES (@id, @author, @activity, @name, @description, @outline, @datetime, @sended)";
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
            parameters.Add("sended", false, DbType.Boolean);

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
            var query2 = "SELECT id, author, activity, name, description, outline FROM applications WHERE id = @id";

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

        public async Task<string> CheckSended(Guid id)
        {
            var query = "SELECT sended FROM applications WHERE id = @id";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<AppForSendorDeleteorEdit>(query, new { id });
                if (requestedApp.Sended == true)
                {
                    return "YES";
                }

                return "NO";
            }
        }

        private bool IsValidAppForReview(AppForSendorDeleteorEdit? app)
        {
            if (app.Activity == null ||
                app.Name == null ||
                app.Outline == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> AddAppsToReview(Guid id)
        {
            var query = "SELECT id, author, activity, name, description, outline, datetime FROM applications WHERE id = @id";
            var query2 = "INSERT INTO sendedapps (id, author, activity, name, description, outline, datetime) VALUES (@id, @author, @activity, @name, @description, @outline, @datetime)";
            var query3 = "UPDATE applications SET sended = @sended WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<AppForSendorDeleteorEdit>(query, new { id });

                if (IsValidAppForReview(requestedApp))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("author", requestedApp.Author, DbType.Guid);
                    parameters.Add("id", requestedApp.Id, DbType.Guid);
                    parameters.Add("activity", requestedApp.Activity, DbType.String);
                    parameters.Add("name", requestedApp.Name, DbType.String);
                    parameters.Add("description", requestedApp.Description, DbType.String);
                    parameters.Add("outline", requestedApp.Outline, DbType.String);
                    parameters.Add("datetime", requestedApp.DateTime, DbType.DateTime2);

                    var parameters2 = new DynamicParameters();
                    parameters2.Add("sended", true, DbType.Boolean);
                    parameters2.Add("id", id, DbType.Guid);

                    await connection.ExecuteAsync(query2, parameters);
                    await connection.ExecuteAsync(query3, parameters2);

                    return "Success";
                }

                return "Fail";
            }
             
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