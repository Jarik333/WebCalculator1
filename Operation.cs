using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace EFDataApp.Models
{
    public class Operation
    {
       public int ID { set; get; }
        public string Expression { set; get; }
        public string Result { set; get; }
        public string Date { set; get; }
        public string IP { set; get; }
      
    }
}
