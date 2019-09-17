using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Models
{
    public class Application
    {
        public static object StartupPath { get; internal set; }
        public Guid Id { get; set; }
        public int ApplicationId { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public decimal Amount { get; set; }
        public DateTime PostingDate { get; set; }
        public Boolean IsCleared  { get; set; }
        public DateTime ClearedDate { get; set; }
    }
}
