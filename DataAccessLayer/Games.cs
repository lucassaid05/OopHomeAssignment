using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Games
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CreatorFK { get; set; }
        public string OpponentFK { get; set; }
        public bool Complete {  get; set; }
    }
}
