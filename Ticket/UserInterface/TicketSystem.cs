using System;
using System.Collections.Generic;
using System.IO;
using Ticket.Models;

namespace Ticket.UserInterface
{
    public class TicketSystem
    {
        private TicketSystem() {
            while (choice != 3)
            {
                try
                {
                    choice = promptForChoice();
                    switch (choice)
                    {
                        case 1:
                            //printTickets(file);
                            Console.WriteLine("Print The tickets");
                            break;
                        case 2:
                            addTicket(file);
                            break;
                        case 3:
                            Console.WriteLine("Goodbye");
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Choice");
                }
            }
        }

        private int choice = 0;
        private string file = "tickets.csv";

        
        public static List<Models.Ticket> Tickets = new List<Models.Ticket>();

        private readonly string[] _main =
        {
            "Read data from file",
            "Create new ticket",
            "Exit"
        };

        public void printMenu(string[] items)
        {
            int count = 1;
            Console.WriteLine();
            foreach (var menu in items)
            {
                Console.WriteLine(count++ + ". " + menu);
            }

            Console.WriteLine();
        }

        private int promptForChoice()
        {
            return int.Parse(Console.ReadLine());
        }

       

        private void addTicket(string file)
        {
            string summary = promptForString("Summary");
            string status = promptForString("Status");
            Level level = prioritySelector();
            string submitter = promptForString("Submitter");
            string assigned = promptForString("Assigned");
            string watching = promptForString("Watching");


            bool isValid = true;
            do
            {
                char ticketType = Char.Parse(promptForString("(B)ug, (E)nhancment, (T)ask").ToUpper());
                switch (ticketType)
                {
                    case 'B':
                        Level severity = prioritySelector();
                        Models.Ticket bug = new Bug(summary, status, level, submitter, assigned, watching, severity);
                        Tickets.Add(bug);
                        break;
                    case 'E':
                        string software = promptForString("Software");
                        int cost = Int32.Parse(promptForString("Cost"));
                        string reason = promptForString("Reason");
                        int estimate = Int32.Parse(promptForString("Estimate"));
                        Models.Ticket enhancement = new Enhancement(summary, status, level, submitter, assigned,
                            watching, software, cost, reason, estimate);
                        Tickets.Add(enhancement);
                        break;
                    case 'T':
                        string projectName = promptForString("dueDate");
                        Console.WriteLine(DateTime.Today);
                        DateTime dueDate = DateTime.Parse(promptForString("dueDate"));
                        Models.Ticket task = new Task(summary, status, level, submitter, assigned, watching,
                            projectName, dueDate);
                        Tickets.Add(task);
                        break;
                    default:
                        Console.WriteLine("Invalid Entry");
                        isValid = false;
                        break;
                }
            } while (isValid);


            StreamWriter streamWriter = new StreamWriter(file, append: true);
            //streamWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6}", ticketId, summary, status, priority, submitter,assigned, watching);
            streamWriter.Close();
            foreach (var item in Tickets)
            {
                Console.WriteLine(item);
            }
        }

        private Level prioritySelector()
        {
            Console.WriteLine("Enter Priority 0,1,2:");
            while (true)
            {
                int input = promptForChoice();
                switch (input)
                {
                    case 0:
                        return Level.Low;
                    case 1:
                        return Level.Medium;
                    case 2:
                        return Level.High;
                    default:
                        Console.WriteLine("Invalid Option");
                        Console.WriteLine("Enter Priority 0,1,2:");
                        break;
                }
            }
        }

        private string promptForString(String name)
        {
            Console.WriteLine("Enter {0}:", name);
            return Console.ReadLine();
        }
    }
}