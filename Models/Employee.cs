using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models
{
    public class Employee : EntityBase
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{NameSurname} ({Email} - {Phone})";
        }
    }
}
