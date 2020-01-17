using EasyConsoleCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace one_time_pad_tool.pages
{
    class EncryptPage : Page
    {

        private string[] args;

        public EncryptPage(Program program, string[] args)
            : base("Encrypting", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            base.Display();

            // TODO make global print with colors method
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nEncrypting..."); // TODO spinner
            Console.ForegroundColor = ConsoleColor.White;

            OneTimePad.EncryptFile(args[0], args[1], args[2]);

            if (Int32.Parse(args[4]) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSecurely Deleting Original File...");
                Console.ForegroundColor = ConsoleColor.White;
                OneTimePad.SecureDelete(args[0]);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return home");
            Console.ReadLine();

            Program.NavigateHome();
        }
    }
}