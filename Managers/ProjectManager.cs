using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Managers
{
    public class ProjectManager
    {
        DataAccessManager _dataAccess = new DataAccessManager();
        string _tableName = "Projects";

        public List<Project> List()
        {
            return _dataAccess.Select<Project>(_tableName);
        }
        public bool Add(Project project)
        {
            if (string.IsNullOrEmpty(project.Name?.Trim()))
                throw new Exception("Proje adı boş bırakılamaz.");
            if (project.StartDate > project.FinishDate)
                throw new Exception("Başlangıç tarihi bitiş tarihinden büyük olamaz!");

            string[] columnNames = new string[]
            {
                nameof(Project.Name),
                nameof(Project.Description),
                nameof(Project.StartDate),
                nameof(Project.FinishDate),
                nameof(Project.UpdatedOn),
                nameof(Project.CreatedOn)
            };
            return _dataAccess.Insert<Project>(columnNames, _tableName, project);
        }
        public bool Edit(Project project)
        {
            if (string.IsNullOrEmpty(project.Name?.Trim()))
                throw new Exception("Çalışan adı boş bırakılamaz.");
            if (project.StartDate > project.FinishDate)
                throw new Exception("Başlangıç tarihi bitiş tarihinden büyük olamaz!");

            string[] columnNames = new string[]
            {
                nameof(Project.Name),
                nameof(Project.Description),
                nameof(Project.StartDate),
                nameof(Project.FinishDate),
                nameof(Project.UpdatedOn),
                nameof(Project.CreatedOn)                
            };
            return _dataAccess.Update<Project>(columnNames, _tableName, project);
        }
        public bool Delete(int id)
        {
            return _dataAccess.Delete(_tableName, id);
        }
    }
}
