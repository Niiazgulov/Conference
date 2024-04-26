using Dapper;
using Domain.Models;
using Domain.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace DataAccess.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IConfiguration _configuration;

        public ApplicationRepository(IConfiguration configuration)
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

        public async Task<Applications?> EditApps(Guid id, EditedAppDTO app)
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
                if (requestedApp != null) 
                {                 
                    if (requestedApp.Sended)
                        return "YES";
                }
                
                return "NO";
            }
        }

        public async Task<bool> CheckUserById(Guid author)
        {
            var query = "SELECT EXISTS (SELECT * FROM applications WHERE author = @author AND sended = @sended)";
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("author", author, DbType.Guid);
                parameters.Add("sended", false, DbType.Boolean);
                var requestedApp = await connection.QuerySingleOrDefaultAsync<bool>(query, parameters);

                if (requestedApp)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task<(bool, string)> AddAppsToReview(Guid id)
        {
            var query = "SELECT id, author, activity, name, description, outline, datetime FROM applications WHERE id = @id";
            var query2 = "UPDATE applications SET sended = @sended WHERE id = @id";

            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("NpgConnection")))
            {
                var requestedApp = await connection.QuerySingleOrDefaultAsync<AppForSendorDeleteorEdit>(query, new { id });

                if (IsValidAppForReview(requestedApp))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("sended", true, DbType.Boolean);
                    parameters.Add("id", id, DbType.Guid);

                    await connection.ExecuteAsync(query2, parameters);
                    return (true, String.Empty);
                }

                return (false, "В черновике заявки есть поле NULL! Заявка не может быть отправлена, добавьте недостающие данные.");
            }
        }

        private bool IsValidAppForReview(AppForSendorDeleteorEdit? app)
        {
            if (app!.Activity == null ||
                app.Name == null ||
                app.Outline == null)
            {
                return false;
            }

            return true;
        }
    }
}
