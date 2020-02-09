using EasyConsoleCore;
using one_time_pad_tool.core.helpers;
using one_time_pad_tool.pages;
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


            // STEPS
            // Encrypt: file paths -> encrypt -> base64 pad? -> delete original file?
            // Decrypt: file paths -> (Detect if pad is base64) -> decrypt -> (verify file contents) delete original file? -> delete pad?

            // TODO detect if using base64 pad with:

            //public static bool IsBase64String(string base64)
            //{
            //    Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            //    return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
            //}

            // TODO ASK DELETION STEPS AFTER ENCRYPTION!!!!!
            // TODO ask for pad path before out path
            // TODO allow for base64 string key
            // TODO provide user some information about file paths
            // TODO externalize strings
            // TODO create enums for arguments
            // TODO close program on main menu exit
            // TODO create installer

            base.Display();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPress any any key to begin decryption...");
            Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Decrypting...");
            Console.ForegroundColor = ConsoleColor.White;

            if (FileHelper.CheckFileBase64(args[3]))
            {
                OneTimePad.DecryptFileBase64Pad(args[1], args[2], args[3]);
            } 
            else
            {
                OneTimePad.DecryptFile(args[1], args[2], args[3]);
            }

            //if (Int32.Parse(args[4]) == 0)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("\nSecurely Deleting Encrypted File...");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    OneTimePad.SecureDelete(args[1]);
            //}

            //if (Int32.Parse(args[6]) == 0)
            //{
            //    Console.ForegroundColor = ConsoleColor.Cyan;
            //    Console.WriteLine("\nSecurely Deleting Pad File...");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    OneTimePad.SecureDelete(args[2]);
            //}

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return continue...");
            Console.ReadLine();

            Program.AddPage(new FileOptionsPage(Program, args));
            Program.NavigateTo<FileOptionsPage>();
            //Program.NavigateHome();
        }
    }
}
