using Dapper;
using Domain;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;


namespace Readers.Repository
{
    public class AddNewApplicationRepository : IAddNewApplicationRepository
    {
        private readonly IConfiguration _configuration;

        public AddNewApplicationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}
