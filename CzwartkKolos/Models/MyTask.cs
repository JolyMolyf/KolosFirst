namespace CzwartkKolos.Models
{
    public class MyTask
    {
        public int idTask { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string deadline { get; set; }

        public string idTeam { get; set; }

        public string idTaskType { get; set;  }

        public string idAssignedTo { get; set; }

        public string idCreator { get; set; }
    }
}