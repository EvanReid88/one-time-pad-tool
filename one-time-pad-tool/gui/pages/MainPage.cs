using EasyConsoleCore;
using one_time_pad_tool.gui;
using System;
using System.Collections.Generic;
using System.Text;

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
            string[] options = new string[] { "OTP Encrypt", "OTP Decrypt", "Exit" };
            int option = ConsoleHelper.MultipleChoice(true, options, this.Title + "\n---\n");

            if (option == 0)
            {
                Program.NavigateTo<FilePage>();
            } 
            //else
            //{

            //}
        }
    }
}
