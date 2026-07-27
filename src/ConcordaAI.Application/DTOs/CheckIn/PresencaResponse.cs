using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcordaAI.Application.DTOs.CheckIn
{
    public class PresencaResponse
    {
        public DateTime? DataHoraCheckIn { get; set; }
        public DateTime? DataHoraCheckOut { get; set; }
        public bool Presente => DataHoraCheckIn.HasValue;
    }
}
