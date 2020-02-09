using EasyConsoleCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
            // TODO adjust for decryption option args[0]

            base.Display();

            string breadcrumb = null;
            foreach (var title in Program.History.Select((page) => page.Title).Reverse())
                breadcrumb += title + " > ";
            breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);

            // TODO ask to delete pad

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

            //if (deletefile_option != 2)
            //{
            //    args[4] = deletefile_option.ToString();
            //}
            //else
            //{
            //    Program.NavigateBack();
            //}


            //// TODO fix exception. Perhaps move to pad options 
            //if (Int32.Parse(args[0]) == 1)
            //{
            //    string header_deletepad = breadcrumb + "\n---\n\nDelete pad? (Cannot be undone)";
            //    int deletepad_option = ConsoleHelper.MultipleChoice(true, options, header_deletepad);

            //    if (deletepad_option != 2)
            //    {
            //        args[6] = deletepad_option.ToString();
            //    }
            //    else
            //    {
            //        Program.NavigateBack();
            //    }
            //}

            //Program.AddPage(new PadOptionsPage(Program, args));
            //Program.NavigateTo<PadOptionsPage>();
        }
    }
}
