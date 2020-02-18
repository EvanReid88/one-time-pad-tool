using EasyConsoleCore;
using System;
using System.Linq;
using one_time_pad_tool.gui;
using one_time_pad_tool.core.helpers;

namespace one_time_pad_tool.pages
{
    class FileOptionsPage : Page
    {
        private string[] args;

        public FileOptionsPage(Program program, string[] args)
            : base("File Options", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            base.Display();

            string breadcrumb = null;
            foreach (var title in Program.History.Select((page) => page.Title).Reverse())
                breadcrumb += title + " > ";
            breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);

            string[] options = { "Yes", "No" };
            string header_text = Int32.Parse(args[0]) == 0 ? "original" : "encrypted";
            string header_deletefile = breadcrumb + "\n---\n\nDelete " + header_text + " file? (Cannot be undone)\n";
            int deletefile_option = ConsoleHelper.MultipleChoice(true, options, header_deletefile);

            Console.Clear();
            base.Display();

            if (deletefile_option == 0)
            {
                FileHelper.SecureDelete(args[1]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFile Deleted.");
            }


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress any key to return home...");
            Console.ReadLine();

            Program.NavigateHome();
        }
    }
}
