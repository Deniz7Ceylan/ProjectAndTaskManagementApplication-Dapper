namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public override string ToString()
        {
            return $"{Name} ({StartDate?.ToShortDateString()} - {FinishDate?.ToShortDateString()})";
        }
    }
}
