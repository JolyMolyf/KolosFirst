using System.Collections.Generic;
using System.Threading.Tasks;

namespace CzwartkKolos.Models
{
    public class ProjectResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<MyTask> tasks { get; set; }
    }
}