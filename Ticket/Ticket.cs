using System;

namespace Ticket
{
    public class Ticket
    {
        private int TicketId { get; set; }
        string Summary { get; set; }
        string Status{ get; set; }
        Priority Priority{ get; set; }
        string Submitter{ get; set; }
        string Assigned{ get; set; }
        string Watching{ get; set; }

        public Ticket(int ticketId, string summary, string status, Priority priority, string submitter, string assigned, string watching)
        {
            TicketId = ticketId;
            Summary = summary;
            Status = status;
            Priority = priority;
            Submitter = submitter;
            Assigned = assigned;
            Watching = watching;
        }

        public override string ToString()
        {
            return $"{TicketId,-15}{Summary,-15}{Status,-15}{Priority,-15}{Submitter,-15}{Assigned,-15}{Watching,-15}";
                
        }
    }
}