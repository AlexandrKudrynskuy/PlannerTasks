using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CurrentUser
    {
        public static CurrentUser CurUser { get; set; }
        public static User User { get; set; }

        private CurrentUser()
        { 
        }
        public  static CurrentUser GetCurentUser()
        { 
            if(CurUser == null) 
            {
                CurUser = new CurrentUser();
            }
            return CurUser;
        }
        public static void SetCurrentUser(User user)
        {
        User = user;
        }


    }
}
