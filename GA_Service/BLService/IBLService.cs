using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLService
{
    public interface IBLService<T> where T: TicketDto
    {
        public string connstr { get; set; }   
        Task<IEnumerable<T>> GetAll();
        Task<int> Save(T dto);
    }
}
