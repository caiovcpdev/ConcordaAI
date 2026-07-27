using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Domain.Entities
{
    public class CheckInDto
    {
        public Guid EscalaTrabalhadorId { get; set; }
        public DateTime? DataHoraCheckIn { get; set; }
        public DateTime? DataHoraCheckOut { get; set; }
    }
}
