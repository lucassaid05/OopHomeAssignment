using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GameShipConfiguration
    {
        public int ID { get; set; }
        public string PlayerFK { get; set; }
        public int GameFK { get; set; }
        public string Coordinate { get; set; }
    }
}
