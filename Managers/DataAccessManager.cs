using Dapper;
using Microsoft.Data.SqlClient;
using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Managers
{
    public class DataAccessManager
    {
        // Daccess Select

        private readonly string _connectionString = "Server=localhost;Database=ProjectAndTaskManagementApplication;Trusted_Connection=True";
        private SqlConnection _connection;
        public DataAccessManager()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public List<T>Select<T>(string tableName) where T : EntityBase
        {
            string sql = $"Select * From {tableName}";
            return _connection.Query<T>(sql).ToList();
        }
        public List<T> Select<T>(string sql, object param = null) where T : EntityBase
        {
            return _connection.Query<T>(sql,param).ToList();
        }

        // Daccess Insert

        public bool Insert<T>(string[]columnNames,string tableName, T obj) where T: EntityBase
        {
            string columns = string.Join(',', columnNames);
            List<string> parameters = new List<string>(columnNames.Length);
            columnNames.ToList().ForEach(x => parameters.Add($"@{x}"));
            string parameterNames = string.Join(',', parameters);
            string sql = $"Insert Into [{tableName}] ({columns}) Values ({parameterNames})";
            obj.CreatedOn=DateTime.Now;
            return _connection.Execute(sql, obj) > 0;
        }

        // Daccess Update

        public bool Update<T>(string[]columnNames, string tableName, T obj) where T : EntityBase
        {
            List<string> parameters = new List<string>(columnNames.Length);
            columnNames.ToList().ForEach(x => parameters.Add($"{x}=@{x}"));
            string parameterNames = string.Join(',', parameters);
            string sql = $"Update [{tableName}] Set {parameterNames} Where Id = @Id";
            obj.UpdatedOn=DateTime.Now;
            return _connection.Execute(sql, obj) > 0;
        }

        // Daccess Delete

        public bool Delete (string tableName, int id)
        {
            string sql = $"Delete From [{tableName}] Where Id = @Id";
            return _connection.Execute(sql, new {Id=id}) > 0;
        }
    }
}
