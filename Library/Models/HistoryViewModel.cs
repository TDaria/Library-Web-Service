using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class HistoryViewModel
    {
        public int BookId { get; set; }

        public List<string> Logs { get; set; }

        public HistoryViewModel()
        {
            this.Logs = new List<string>();
        }
    }
}