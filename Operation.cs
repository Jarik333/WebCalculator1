using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDataApp.Models
{
    public class Operation
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public string Expression { set; get; }
        public string Result { set; get; }
        public DateTime Date { set; get; }
        public string IP { set; get; }
        public string Browser { set; get; }
        public string Error { set; get; }
      
    }
}
