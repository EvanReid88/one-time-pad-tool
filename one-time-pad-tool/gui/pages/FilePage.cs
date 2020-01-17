using EasyConsoleCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace one_time_pad_tool.pages
{
    class FilePage : Page
    {
        public FilePage(Program program)
            : base("Encrypt", program) {
        }

        public override void Display()
        {
            base.Display();

            string[] args = new string[5];

            string enter_file_path = "Enter path of file to encrypt: ";
            string enter_pad_path = "Enter path of directory to save one-time-pad: ";
            string enter_outfile_path = "Enter path of directory to save encrypted file: ";

            // for decryption
            //        enter_file_path = "Enter path of file to decrypt: ";
            //        enter_pad_path = "Enter one-time-pad file path";
            //        enter_outfile_path = "Enter path of directory to save decrypted file";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nType 'back' to return");
            Console.ForegroundColor = ConsoleColor.White;

            // TODO validate path input, navigate back if user types "back"
            Console.WriteLine("\n" + enter_file_path);

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //args[0] = Path.GetFullPath(Console.ReadLine());
            //Console.ForegroundColor = ConsoleColor.White;

            args[0] = ValidateFilePath(enter_file_path);

            // TODO validate path input
            Console.WriteLine("\n" + enter_outfile_path);

            args[1] = ValidateDirectoryPath(enter_outfile_path);
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //args[1] = Path.GetFullPath(Console.ReadLine());
            //Console.ForegroundColor = ConsoleColor.White;

            // TODO validate path input
            Console.WriteLine("\n" + enter_pad_path);

            args[2] = ValidateDirectoryPath(enter_pad_path);

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //args[2] = Path.GetFullPath(Console.ReadLine());
            //Console.ForegroundColor = ConsoleColor.White;

            Program.AddPage(new FileOptionsPage(Program, args));
            Program.NavigateTo<FileOptionsPage>();
        }

        public string ValidateFilePath(string desc)
        {
            bool valid = false;
            string path = "";

            while (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                path = Path.GetFullPath(Console.ReadLine());

                if (File.Exists(path))
                {
                    valid = true;
                } 
                else
                {
                    ClearCurrentConsoleLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(desc);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ensure file path is valid\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

            return path;
        }

        public string ValidateDirectoryPath(string desc)
        {
            bool valid = false;
            string path = "";

            while (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                path = Path.GetFullPath(Console.ReadLine());

                if (Directory.Exists(path))
                {
                    valid = true;
                }
                else
                {
                    // TODO make into helper method
                    ClearCurrentConsoleLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(desc);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ensure directory path is valid\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

            return path;
        }

        public static void ClearCurrentConsoleLine()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }
        }
    }
}
