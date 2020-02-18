using EasyConsoleCore;
using one_time_pad_tool.gui;
using System;

namespace one_time_pad_tool.pages
{
    class MainPage : Page
    {
        public MainPage(Program program)
            : base("One Time Pad Tool", program) { 
        }
        public override void Display()
        {
            base.Display();
            Console.ForegroundColor = ConsoleColor.White;

            string[] args = new string[7];

            string[] options = new string[] { "OTP Encrypt", "OTP Decrypt", "Exit" };
            args[0] = ConsoleHelper.MultipleChoice(true, options, this.Title + "\n---\n").ToString();

            if (args[0] == "2")
            {
                Environment.Exit(0);
            }

            Program.AddPage(new FilePage(Program, args));
            Program.NavigateTo<FilePage>();
        }
    }
}
