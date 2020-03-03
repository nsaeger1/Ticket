namespace Ticket.Models
{
    public interface ITicket
    {
        // ReSharper disable once InconsistentNaming
        string CSV();
        void AssignTicket(string assigned);
        void EnterIntoTesting(string assigned);
    }
}