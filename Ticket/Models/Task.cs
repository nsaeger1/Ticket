using System;

namespace Ticket.Models
{
    public class Task : Ticket, ITicket
    {
        private string ProjectName { get; set; }
        private DateTime DueDate { get; set; }
        
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

        public Task(string ticketType,string summary, Level priority, string submitter, string watching, string projectName, DateTime dueDate) : base(ticketType, summary, priority, submitter, watching)
        {
            TicketType = "Task";
            ProjectName = projectName;
            DueDate = dueDate;
            
        }

        public Task(string summary, Status status, Level priority, string submitter, string assigned, string watching, string projectName, DateTime dueDate) : base(summary,  priority, submitter, assigned, watching)
        {
            TicketType = "Task";
            ProjectName = projectName;
            DueDate = dueDate;
            Status = status;
        }
        public override string ToString()
        {
            return $"{TicketId,-15}{Summary,-15}{Status,-15}{Priority,-15}{Submitter,-15}{Assigned,-15}{Watching,-15}{ProjectName, -15}{DueDate, -15}";
        }
        public override string CSV()
        {
            return $"{TicketType},{TicketId},{Summary},{Status},{Priority},{Submitter},{Assigned},{Watching},{ProjectName},{DueDate}";

        }
    }
}