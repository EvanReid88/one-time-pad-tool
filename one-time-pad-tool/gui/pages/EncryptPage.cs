using EasyConsoleCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace one_time_pad_tool.pages
{
    class EncryptPage : Page
    {

        private string[] args;

        public EncryptPage(Program program, string[] args) : base("Encrypt", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            base.Display();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress any any key to begin encryption...");
            Console.ReadLine();

            // TODO make global print with colors method
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Encrypting..."); // TODO spinner
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                OneTimePad.EncryptFile(args[1], args[2], args[3]);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nPress any key to return home...");
                Console.ReadLine();

                Program.NavigateHome();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return continue...");
            Console.ReadLine();

            Program.AddPage(new PadOptionsPage(Program, args));
            Program.NavigateTo<PadOptionsPage>();
        }
    }
}