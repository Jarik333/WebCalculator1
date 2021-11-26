
using System.Collections.Generic;
using EFDataApp.Models;

namespace PagerApp.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Operation> Operations { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}