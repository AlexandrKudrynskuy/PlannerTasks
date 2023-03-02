using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public enum TypeUser
    { 
    SuperAdmin,
    Admin,
    Worker
    }
    public class User
    {
        public int Id { get; set;}
        public TypeUser TypeUser { get; set;}
        public string Login { get; set;}
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set;}
        public string Phone { get; set;}
        public string Photo { get; set;}
        public List<MyTask> TasksAdmin { get; set; }
        public List<MyTask> TasksWorker { get; set; }
   

    }
}
