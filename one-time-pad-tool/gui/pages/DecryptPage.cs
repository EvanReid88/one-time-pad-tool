using EasyConsoleCore;
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


            // STEPS
            // Encrypt: file paths -> encrypt -> base64 pad? -> delete original file?
            // Decrypt: file paths -> (Detect if pad is base64) -> decrypt -> (verify file contents) delete original file? -> delete pad?

            // TODO ask for pad path before out path
            // TODO provide user some information about file paths
            // TODO externalize strings
            // TODO create enums for arguments
            // TODO close program on main menu exit
            // TODO create installer
            // TODO add error handling to deletion

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
