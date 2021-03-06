﻿using EasyConsoleCore;
using one_time_pad_tool.core.helpers;
using one_time_pad_tool.pages;
using System;

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
            Console.WriteLine("\nPress any any key to begin decryption...");
            Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Decrypting...");
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                if (FileHelper.CheckFileBase64(args[3]))
                {
                    OneTimePad.DecryptFileBase64Pad(args[1], args[2], args[3]);
                }
                else
                {
                    OneTimePad.DecryptFile(args[1], args[2], args[3]);
                }
            }
            catch
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
