using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ticket.Models;

namespace Ticket.FileSystem
{
    public class ReadFile
    {
        private List<Models.Ticket> Tickets = new List<Models.Ticket>();
        public static List<Models.Ticket> readFile (string file) {
        
            if (File.Exists(file))
            {
                StreamReader streamReader = new StreamReader(file);
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] arr = line.Split(',');
                    if (!arr.Contains("TicketID"))
                    {
                        
                        //Models.Ticket ticket = new Models.Ticket(int.Parse(arr[0]), arr[1], arr[2], pp, arr[4], arr[5], arr[6]);
                        //Tickets.Add(ticket);
                    }
                    else
                    {
                        Level pp = (Level) Enum.Parse(typeof(Level), arr[3]);
                        
                    }
                }
                
                streamReader.Close();
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

            return Tickets;
        }
    }
}