﻿namespace Ticket.Models
{
    public abstract class Ticket
    {
        private static int _currentId;
        protected int TicketId { get; }
        protected string Summary { get; }
        protected Status Status{ get; set; }
        protected Level Priority{ get; }
        protected string Submitter{ get; }
        protected string Assigned{ get; set; }
        protected string Watching{ get; }

        protected Ticket()
        {
            TicketId = 0;
            Summary = "Default Summary";
            Status = Status.Unassigned;
            Priority = Level.Low;
            Submitter = "Default Submitter";
            Assigned = "Default Assigned";
            Watching = "Default Watching";
        }
        
        protected Ticket(string summary, Level priority, string submitter, string watching)
        {
            TicketId = GetNextID();
            Summary = summary;
            Status = Status.Unassigned;
            Priority = priority;
            Submitter = submitter;
            Assigned = "";
            Watching = watching;
        }
        protected Ticket(string summary, string status, Level priority, string submitter, string assigned, string watching)
        {
            TicketId = GetNextID();
            Summary = summary;
            Status = Status.Unassigned;
            Priority = priority;
            Submitter = submitter;
            Assigned = assigned;
            Watching = watching;
        }

        static Ticket()
        {
            _currentId = 0;
        }

        private int GetNextID()
        {
            return ++_currentId;
        }
    }
}