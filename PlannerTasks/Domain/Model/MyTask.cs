using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class MyTask
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Description { get; set; }
        public TypeTask TypeTask { get; set; }
        public int TypeTaskId { get; set;}
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public Priority Priority{ get; set; }
        public int PriorityId { get; set; }
        public User Admin { get; set; }
        public int AdminId { get; set; }
        public User Worker { get; set; }
        public int WorkerId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateFinich { get; set; }
        public int TimeSpan { get; set; } 
    }
}
