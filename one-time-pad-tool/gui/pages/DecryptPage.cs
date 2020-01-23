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
            base.Display();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDecrypting...");
            Console.ForegroundColor = ConsoleColor.White;

            OneTimePad.DecryptFile(args[1], args[2], args[3]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return home");
            Console.ReadLine();

            Program.NavigateHome();
        }
    }
}
