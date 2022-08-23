using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Managers
{
    public class TaskItemManager
    {
        DataAccessManager _dataAccess = new DataAccessManager();
        string _tableName = "Tasks";

        public List<TaskItem> List()
        {
            return _dataAccess.Select<TaskItem>(_tableName);
        }
        public List<TaskItem>ListByProjectId(int projectId)
        {
            string sql = $"Select * From [{_tableName}] Where ProjectId = @ProjectId";
            return _dataAccess.Select<TaskItem>(sql, new { ProjectId = projectId });
        }
        public bool Add(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Summary.Trim()))
                throw new Exception("Görev özeti boş bırakılamaz.");

            string[] columnNames = new string[]
            {
                nameof(task.Summary),
                nameof(task.Description),
                nameof(task.State),
                nameof(task.DueDate),
                nameof(task.EmployeeId),
                nameof(task.ProjectId),
                nameof(task.UpdatedOn),
                nameof(task.CreatedOn)
            };
            return _dataAccess.Insert<TaskItem>(columnNames, _tableName, task);
        }
        public bool Edit(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Summary?.Trim()))
                throw new Exception("Görev özeti boş bırakılamaz.");

            string[] columnNames = new string[]
            {
                nameof(task.Summary),
                nameof(task.Description),
                nameof(task.State),
                nameof(task.DueDate),
                nameof(task.EmployeeId),
                nameof(task.ProjectId),
                nameof(task.UpdatedOn),
                nameof(task.CreatedOn)
            };
            return _dataAccess.Update<TaskItem>(columnNames, _tableName, task);
        }
        public bool Delete(int id)
        {
            return _dataAccess.Delete(_tableName, id);
        }
    }
}
