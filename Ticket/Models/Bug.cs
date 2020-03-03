namespace Ticket.Models
{
    public class Bug : Ticket, ITicket
    {
        private Level Severity { get; set; }

        public Bug(string summary, Level priority, string submitter, string watching) : base(summary, priority, submitter, watching){}

        public void AssignTicket(string assigned)
        {
            Status = Status.Assigned;
            Assigned = assigned;
        }

        public void EnterIntoTesting(string assigned)
        {
            Status = Status.Testing;
            Assigned = assigned;
        }

        public Bug(string summary, string status, Level priority, string submitter, string assigned, string watching, Level severity) : base(summary, status, priority, submitter, assigned, watching)
        {
            Severity = severity;
        }

        public override string ToString()
        {
            return $"{TicketId,-15}{Summary,-15}{Status,-15}{Priority,-15}{Submitter,-15}{Assigned,-15}{Watching,-15}{Severity, -15}";
        }

        public string CSV()
        {
            return $"{TicketId},{Summary},{Status},{Priority},{Submitter},{Assigned},{Watching},{Severity}";

        }

        
    }
}