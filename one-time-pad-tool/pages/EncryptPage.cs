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
            Console.WriteLine("\nEncrypting...\n"); // TODO spinner

            OneTimePad.EncryptFile(args[0], args[1], args[2]);

            if (Int32.Parse(args[4]) == 0)
            {
                OneTimePad.SecureDelete(args[0]);
            }

            Console.Clear();
            base.Display();
            Console.WriteLine("\nDone!\n");

            Thread.Sleep(1000); // TODO wait till user presses any button to return to menu
            Program.NavigateTo<MainPage>();
        }
    }
}