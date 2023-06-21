using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLService
{
    public class TicketDto
    {
        public string IdTienda { get; set; }
        public string IdRegistradora { get; set; }
        public int Ticket { get; set; }
        public DateTime HoraFecha { get; set; }


    }
}
