using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OperationDetail
    {
        public string Message{ get; set; }
        public bool IsEror { get; set; }
        public Exception Excep { get; set; }
    }
}
