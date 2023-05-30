using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileResubmitter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * FileResubmitter - Transfers files in bulk from source to destinatiom
             * Command Line Arguments:
             * 1. mode (mandatory) - chooses the mode for transfer.
             *    Available Modes:
             *    Extract - Extracts the files from the source archive and transfers to the destination
             *    Direct - Transfers the file Directly
             * 2. vanish (optional) - transfer files one by one and if only the file disappears from destination path
             * 3. file (mandatory) - mention file to extract file names
             * 4. verbose (optional) - shows command output to window, otherwise terminates once the operation is complete
             * 5. src (mandatory) - source path
             * 6. dest (mandatory) - dest path
            */
            // Convert args string array to List array
            List <string> argslist = new List <string> (args);
            List<string> mandatory_args = new List<string> {"--mode", "--file", "--src", "--dest" };
            if (!(mandatory_args.Count() == Intersect(argslist, mandatory_args).Count()))
            {
                Console.WriteLine("One or more argument are missing, please make sure to fill all the required arguments.");
                ShowDefaultArgumentMessage();
            }
            Console.Read();
        }

        static void ShowDefaultArgumentMessage()
        {
            Console.Write("FileResubmitter:\r\n Transfers files in bulk from source to destination\r\nCommand Line Arguments:\r\n1. mode (mandatory) - chooses the mode for transfer.\r\nAvailable Modes:\r\nExtract - Extracts the files from the source archive and transfers to the destination\r\nDirect - Transfers the file Directly\r\n2. vanish (optional) - transfer files one by one and if only the file disappears from destination path\r\n             * 3. file (mandatory) - mention file to extract file names\r\n4. verbose (optional) - shows command output to window, otherwise terminates once the operation is complete\r\n5. src (mandatory) - source path\r\n6. dest (mandatory) - dest path\n");
        }
        static IEnumerable<T> Intersect<T>(IList<T> lhs, IList<T> rhs)
        {
            // Adapted from https://social.msdn.microsoft.com/Forums/vstudio/en-US/e1a18e03-865e-4676-b51e-48b7ea76e206/intersection-between-two-lists-of-strings?forum=csharpgeneral
            if (lhs == null) throw new ArgumentNullException("lhs");
            if (rhs == null) throw new ArgumentNullException("rhs");

            // build the dictionary from the shorter list
            if (lhs.Count > rhs.Count)
            {
                IList<T> tmp = rhs;
                rhs = lhs;
                lhs = tmp;
            }
            Dictionary<T, bool> lookup = new Dictionary<T, bool>();
            foreach (T item in lhs)
            {
                if (!lookup.ContainsKey(item)) lookup.Add(item, true);
            }
            foreach (T item in rhs)
            {
                if (lookup.ContainsKey(item))
                {
                    lookup.Remove(item); // prevent duplicates
                    yield return item;

                }
            }
        }
    }
}
