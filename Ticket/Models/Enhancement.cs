namespace Ticket.Models
{
    public class Enhancement : Ticket, ITicket
    {
        private string Software { get; set; }
        private int Cost { get; set; }
        private string Reason { get; set; }
        private int Estimate { get; set; }
        
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

        public Enhancement(string summary, Level priority, string submitter, string watching, string software, int cost, string reason, int estimate) : base(summary, priority, submitter, watching)
        {
            Software = software;
            Cost = cost;
            Reason = reason;
            Estimate = estimate;
        }

        public Enhancement(string summary, string status, Level priority, string submitter, string assigned,
            string watching, string software, int cost, string reason, int estimate) : base(summary, status, priority,
            submitter, assigned, watching)
        {
            Software = software;
            Cost = cost;
            Reason = reason;
            Estimate = estimate;
        }
        public override string ToString()
        {
            return $"{TicketId,-15}{Summary,-15}{Status,-15}{Priority,-15}{Submitter,-15}{Assigned,-15}{Watching,-15}{Software, -15}{Cost,-15}{Reason,-15}{Estimate,-15}";
        }

        public string CSV()
        {
            return $"{TicketId},{Summary},{Status},{Priority},{Submitter},{Assigned},{Watching},{Software},{Cost},{Reason},{Estimate}";

        }
    }
}