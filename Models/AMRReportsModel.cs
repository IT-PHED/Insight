using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PHEDServe.Models
{
    public class TicketStatus
    {
        public string CompletedTickets { get; set; }
        public string ActiveTickets { get; set; }
        public string ClosedTickets { get; set; }
    }


    public class AMRReportsModel
    {

        public string TotalRead { get; set; }



        public string TotalCurrentlyRead { get; set; }

        public string TotalNotCommunicating { get; set; }

        public string TotalMalfunctioning { get; set; }

        public string PercentageRead { get; set; }

        public List<DailyMeterReading> TotalCaptured { get; set; }
    }


}
