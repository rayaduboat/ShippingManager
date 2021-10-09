using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FinanceManager.Model.Models
{
    public interface IDbConnections
    {
        List<T> Execute<T>(string querystring) where T : class;
        List<T> Execute<T>(string querystring, string pName, string pValue) where T : class;
        T ExecuteSingle<T>(string querystring) where T : class;
        void Add(string sqlstring, SqlParameter[] sqlparameters);
    }

    public class DbConnections : IDbConnections
    {
        private readonly IConfiguration config;
        
        private string connection_string;

        public DbConnections(IConfiguration config)
        {
            connection_string = (config.GetConnectionString("DefaultConnection"));
        }
        public List<T> Execute<T>(string querystring) where T : class
        {
            using (var conn = new SqlConnection(connection_string))
            {
                return conn.Query<T>(querystring).ToList();
            }
        }
        public T ExecuteSingle<T>(string querystring) where T : class
        {
            using (var conn = new SqlConnection(connection_string))
            {
                return conn.Query<T>(querystring).FirstOrDefault();
            }
        }
        public List<T> Execute<T>(string querystring, string pName, string pValue) where T : class
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@" + pName, pValue);
            dynamicParameters.Add("@id", pValue);
            using (var conn = new SqlConnection(connection_string))
            {
                return conn.Query<T>(querystring).ToList();
            }
        }

        /// <summary>
        /// Call this function to add data to database
        /// </summary>
        /// <param name="sqlstring">eg INSERT INTO Members(fname,lname) values(@value1,@values2)</param>
        /// <param name="sqlparameters">SqlParameter sqlParameter = new SqlParameter("@Id", "Amoah");</param>
        public void Add( string sqlstring,SqlParameter[] sqlparameters)  
        {
            var conn = new SqlConnection(connection_string);
            var command = new SqlCommand(sqlstring);
            command.CommandType = CommandType.Text;
            command.Parameters.AddRange(sqlparameters);
            command.ExecuteNonQuery();
        }
    }
}




