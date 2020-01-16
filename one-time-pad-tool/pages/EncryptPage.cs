using EasyConsoleCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace one_time_pad_tool.pages
{
    class EncryptPage : Page
    {
        string tip = "(Type 'back' to go back to last page)";

        public EncryptPage(Program program)
            : base("Encrypt", program) {
        }

        public override void Display()
        {
            base.Display();

            string[] args = new string[3];

            string enter_file_path = "Enter path of file to encrypt: " + tip;
            string enter_pad_path = "Enter path of directory to save one-time-pad: " + tip;
            string enter_outfile_path = "Enter path of directory to save encrypted file: " + tip;

            //        enter_file_path = "Enter path of file to decrypt: ";
            //        enter_pad_path = "Enter one-time-pad file path";
            //        enter_outfile_path = "Enter path of directory to save decrypted file";

            // TODO validate path input, navigate back if user types "back"
            Console.WriteLine("\n" + enter_file_path);
            args[0] = Console.ReadLine();

            // TODO validate path input
            Console.WriteLine("\n" + enter_outfile_path);
            args[1] = Console.ReadLine();

            // TODO validate path input
            Console.WriteLine("\n" + enter_pad_path);
            args[2] = Console.ReadLine();

            Program.AddPage(new PadOptionsPage(Program, args));
            Program.NavigateTo<PadOptionsPage>();
        }
    }
}
