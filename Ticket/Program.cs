using System;
using System.IO;

namespace Ticket
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string file = "tickets.csv";
            Boolean more = true;
            string choice;

            do
            {
                // ask user a question
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Create file from data.");
                Console.WriteLine("Enter any other key to exit.");
                // input response
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    if (File.Exists(file))
                    {
                        StreamReader streamReader = new StreamReader(file);
                        while (!streamReader.EndOfStream)
                        {
                            string line = streamReader.ReadLine();
                            string[] arr = line.Split(',');
                            foreach (var item in arr)
                            {
                                Console.Write(item);
                            }
                            Console.WriteLine();
                        }
                        streamReader.Close();
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                }
                else if (choice == "2")
                {
                    StreamWriter streamWriter = new StreamWriter(file, append: true);
                    while (more)
                    {
                        Console.WriteLine("Enter TicketID: ");
                        string ticketId = Console.ReadLine();
                        Console.WriteLine("Enter Summary: ");
                        string summary = Console.ReadLine();
                        Console.WriteLine("Enter Status: ");
                        string status = Console.ReadLine();
                        Console.WriteLine("Enter Priority: ");
                        string priority = Console.ReadLine();
                        Console.WriteLine("Enter Submitter: ");
                        string submitter = Console.ReadLine();
                        Console.WriteLine("Enter Assigned: ");
                        string assigned = Console.ReadLine();
                        Console.WriteLine("Enter Watching: ");
                        string watching = Console.ReadLine();
                        streamWriter.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}", ticketId, summary, status,priority,submitter,assigned,watching);

                        Console.WriteLine("Would you Like to enter another ticket?");
                        Console.Write("Enter N to exit: ");
                        string accept = Console.ReadLine();
                        if (accept.ToUpper().StartsWith("N"))
                        {
                            more = false;
                            streamWriter.Close();
                        }
                    }
                }
            } while (choice == "1" || choice == "2");
        }
    }
}