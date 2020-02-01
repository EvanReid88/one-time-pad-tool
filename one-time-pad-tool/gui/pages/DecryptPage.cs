using EasyConsoleCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace one_time_pad_tool.gui.pages
{
    class DecryptPage : Page
    {
        private string[] args;

        public DecryptPage(Program program, string[] args) : base("Decrypt", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            // TODO allow for base64 string key
            // TODO externalize strings
            // TODOn create enums for arguments
            // TODO close program on main menu exit
           
            base.Display();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDecrypting...");
            Console.ForegroundColor = ConsoleColor.White;

            OneTimePad.DecryptFile(args[1], args[2], args[3]);

            if (Int32.Parse(args[4]) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSecurely Deleting Encrypted File...");
                Console.ForegroundColor = ConsoleColor.White;
                OneTimePad.SecureDelete(args[1]);
            }

            if (Int32.Parse(args[7]) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nSecurely Deleting Pad File...");
                Console.ForegroundColor = ConsoleColor.White;
                OneTimePad.SecureDelete(args[2]);
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
