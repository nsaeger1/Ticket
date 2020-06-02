using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ticket.Models;
namespace Ticket
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Program();
        }

        private Program()
        {
            printTickets(file);
            Boolean running = true;
            while (running)
            {
                try
                {
                    printMenu(_main);
                    choice = promptForChoice();
                    switch (choice)
                    {
                        case 1:
                            printMenu(_print);
                            choice = promptForChoice();
                            List<Models.Ticket> results = new List<Models.Ticket>();
                            switch (choice)
                            {
                                
                                   
                                case 1:
                                    Status selectedStatus = statusSelector();
                                    
                                    foreach (var ticket in Tickets)
                                    {
                                        if (ticket.Status == selectedStatus)
                                        {
                                            results.Add(ticket);
                                        }
                                    }
                                    Console.WriteLine($"{results.Count} tickets of type {selectedStatus}");
                                    foreach (var ticket in results)
                                    {
                                        Console.WriteLine(ticket);
                                    }
                                    break;
                                case 2:
                                    Level selectedPriority = prioritySelector("priority");
                                    foreach (var ticket in Tickets)
                                    {
                                        if (ticket.Priority == selectedPriority)
                                        {
                                            results.Add(ticket);
                                        }
                                    }
                                    Console.WriteLine($"{results.Count} tickets of type {selectedPriority}");
                                    foreach (var ticket in results)
                                    {
                                        Console.WriteLine(ticket);
                                    }
                                    break;
                                case 3:
                                    string selectedSubmitter = Console.ReadLine();
                                    foreach (var ticket in Tickets)
                                    {
                                        if (ticket.Submitter == selectedSubmitter)
                                        {
                                            results.Add(ticket);
                                        }
                                    }
                                    Console.WriteLine($"{results.Count} tickets of type {selectedSubmitter}");
                                    foreach (var ticket in results)
                                    {
                                        Console.WriteLine(ticket);
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine($"{Tickets.Count} tickets");
                                    foreach (var ticket in Tickets)
                                    {
                                        Console.WriteLine(ticket);
                                    }
                                    break;
                            }
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
        private readonly string[] _print =
        {
            "Display tickets by status",
            "Display tickets by priority",
            "Display tickets by submitter",
            "Display all tickets",
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
                        Status status = (Status) Enum.Parse(typeof(Status), arr[3]);
                        Level priority = (Level) Enum.Parse(typeof(Level), arr[4]);

                        if (arr[0].Equals("Bug"))
                        {
                            Level severity = (Level) Enum.Parse(typeof(Level), arr[8]);
                            Models.Ticket bug = new Bug(arr[2], status, priority, arr[5], arr[6], arr[7], severity);
                            Tickets.Add(bug);
                        }
                        else if (arr[0].Equals("Task"))
                        {
                            DateTime dueDate = DateTime.Parse(arr[9]);

                            Models.Ticket task = new Task(arr[2], status, priority, arr[5], arr[6], arr[7], arr[8],
                                dueDate);
                            Tickets.Add(task);
                        }
                        else if (arr[0].Equals("Enhancment"))
                        {
                            int cost = Int32.Parse(arr[9]);
                            int estimate = Int32.Parse(arr[11]);

                            Models.Ticket enhancement = new Enhancement(arr[2], status, priority, arr[5], arr[6],
                                arr[7], arr[8], cost, arr[10], estimate);
                            Tickets.Add(enhancement);
                        }
                        
                    }
                  
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
            string summary = promptForString("Summary");
            Status status = statusSelector();
            Level priority = prioritySelector("priority");
            string submitter = promptForString("Submitter");
            string assigned = promptForString("Assigned");
            string watching = promptForString("Watching");
            StreamWriter streamWriter = new StreamWriter(file, append: true);
            Models.Ticket ticket;
            bool isValid = true;
            do
            {
                char ticketType = Char.Parse(promptForString("(B)ug, (E)nhancment, (T)ask").ToUpper());
                switch (ticketType)
                {
                    case 'B':
                        Level severity = prioritySelector("severity");
                        ticket = new Bug(summary, status,  priority, submitter, assigned, watching, severity);
                        Console.WriteLine(ticket.CSV());
                        streamWriter.WriteLine(ticket.CSV());
                        Tickets.Add(ticket); 
                        break;
                    case 'E':
                        string software = promptForString("Software");
                        int cost = Int32.Parse(promptForString("Cost"));
                        string reason = promptForString("Reason");
                        int estimate = Int32.Parse(promptForString("Estimate"));
                        ticket = new Enhancement( summary, status, priority,submitter, assigned, watching, software, cost, reason, estimate);
                        Console.WriteLine(ticket.CSV());
                        streamWriter.WriteLine(ticket.CSV());

                        Tickets.Add(ticket);
                        break;
                    case 'T':
                        string projectName = promptForString("project name");
                        Console.WriteLine(DateTime.Today);
                        DateTime dueDate = DateTime.Parse(promptForString("dueDate"));
                        ticket = new Task(summary, status, priority, submitter, assigned, watching, projectName, dueDate);
                        Console.WriteLine(ticket.CSV());
                        streamWriter.WriteLine(ticket.CSV());

                        Tickets.Add(ticket);
                       
                        break;
                    default:
                        Console.WriteLine("Invalid Entry");
                        isValid = false;
                        break;
                }
            } while (!isValid);
            
            
            streamWriter.Close();
            
        }

        private Level prioritySelector(string label)
        {
            
            foreach (var level in Enum.GetValues(typeof(Level)))
            {
                int pos = (int) level;
                Console.WriteLine( $"{pos}) {level}");
            }
            Console.Write($"Enter {label}: ");
            while (true)
            {
                try
                {
                    return (Level) promptForChoice();
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid choice");
                }
            }

        }
        private Status statusSelector()
        {
            foreach (var status in Enum.GetValues(typeof(Status)))
            {
                int pos = (int) status;
                Console.WriteLine( $"{pos}) {status}");
            }
            Console.Write("Enter Status: ");

            while (true)
            {
                try
                {
                    
                    return (Status)promptForChoice();
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid choice");
                }
            }
            
            
            /*while (true)
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
            }*/
        }
      

        private string promptForString(String name)
        {
            Console.WriteLine("Enter {0}:", name);
            return Console.ReadLine();
        }
    }
}