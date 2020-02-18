using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ticket
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Program();
        }
        private string file = "tickets.csv";

        private readonly string[] _main =
        {
            "Read data from file",
            "Create new ticket",
            "Exit"
        };
        public static List<Ticket> Tickets = new List<Ticket>();

        private Program()
        {
            int choice = 0;
            while (choice != 3)
            {
                printMenu(_main);
                
                try
                {
                    choice = promptForChoice();
                    switch (choice)
                    {
                        case 1:
                            printTickets(file);
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
        private int promptForChoice()
        {
            return int.Parse(Console.ReadLine());
        }
        private void printMenu(string[] items)
        {
            int count = 1;
            Console.WriteLine();
            foreach (var menu in items)
            {
                Console.WriteLine(count++ + ". " + menu);
            }

            Console.WriteLine();
        }

        private void printTickets(string file)
        {
            if (File.Exists(file))
            {
                StreamReader streamReader = new StreamReader(file);
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] arr = line.Split(',');
                    if (!arr.Contains("TicketID"))
                    {
                        Priority pp = (Priority)Enum.Parse(typeof(Priority), arr[3]);
                        Ticket ticket = new Ticket(int.Parse(arr[0]), arr[1], arr[2],pp,arr[4],arr[5],arr[6]);
                        Tickets.Add(ticket); 
                    }
                    else
                    {
                        foreach (var item in arr)
                        {
                            Console.Write($"{item,-15}");
                        }
                    }
                }
                Console.WriteLine();
                foreach (var ticket in Tickets)
                {
                    Console.WriteLine(ticket);
                }
                streamReader.Close();
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            
            
        }

        private void addTicket(string file)
        {
            Console.WriteLine(Tickets.Count);
            int ticketId = Tickets.Count;
            string summary = promptForString("Summary");
            string status = promptForString("Status");
            Priority priority = prioritySelector();
            string submitter = promptForString("Submitter");
            string assigned = promptForString("Assigned");
            string watching = promptForString("Watching");

            Ticket ticket = new Ticket(ticketId, summary, status,priority,submitter,assigned,watching);
            Tickets.Add(ticket);
            StreamWriter streamWriter = new StreamWriter(file, append: true);
            streamWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6}", ticketId, summary, status,priority,submitter,assigned,watching);
            streamWriter.Close();
            foreach (var item in Tickets)
            {
                Console.WriteLine(item);

            }

        }

        private Priority prioritySelector()
        {
            Console.WriteLine("Enter Priority 0,1,2:");
            while (true)
            {
                int input = promptForChoice();
                switch (input)
                {
                    case 0:
                        return Priority.Low;
                    case 1:
                        return Priority.Medium;
                    case 2:
                        return Priority.High;
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