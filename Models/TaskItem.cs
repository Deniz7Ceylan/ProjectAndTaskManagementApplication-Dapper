namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models
{
    public class TaskItem : EntityBase
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public DateTime? DueDate { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public override string ToString()
        {
            return $"({GetTaskStateString((TaskState)State)}) {Summary} | Son Tarih:  {DueDate?.ToShortDateString()}";
        }
        public static string GetTaskStateString(TaskState state)
        {
            switch (state)
            {
                case TaskState.None:
                    return "Belirsiz";
                case TaskState.Todo:
                    return "Yapılacak";
                case TaskState.InProgress:
                    return "Yapılıyor";
                case TaskState.Done:
                    return "Tamamlandı";
                default:
                    return "";
            }
        }      
    }
}
