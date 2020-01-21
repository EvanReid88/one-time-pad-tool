using EasyConsoleCore;
using one_time_pad_tool.gui;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace one_time_pad_tool.pages
{
    class FilePage : Page
    {
        private string[] args;
        public FilePage(Program program, string[] args)
            : base("File Paths", program) {

            this.args = args;
        }

        public override void Display()
        {
            base.Display();

            string enter_file_path;
            string enter_pad_path;
            string enter_outfile_path;

            if (Int32.Parse(args[0]) == 0)
            {
                enter_file_path = "Enter path of file to encrypt: ";
                enter_pad_path = "Enter path of directory to save one-time-pad: ";
                enter_outfile_path = "Enter path of directory to save encrypted file: ";
            } else
            {
                enter_file_path = "Enter path of file to decrypt: ";
                enter_pad_path = "Enter one-time-pad file path: ";
                enter_outfile_path = "Enter path of directory to save decrypted file";
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nType 'back' to return");
            Console.ForegroundColor = ConsoleColor.White;

            // TODO validate path input, navigate back if user types "back"
            Console.WriteLine("\n" + enter_file_path);

            args[1] = ValidatePath(enter_file_path);

            Console.WriteLine("\n" + enter_outfile_path);

            args[2] = ValidatePath(enter_outfile_path, true);

            Console.WriteLine("\n" + enter_pad_path);

            args[3] = ValidatePath(enter_pad_path, true);

            Program.AddPage(new FileOptionsPage(Program, args));
            Program.NavigateTo<FileOptionsPage>();
        }

        public string ValidatePath(string desc, bool isDir = false)
        {

            bool valid = false;
            string path = "";

            while (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                string input = Console.ReadLine();
                if (input.ToLower() == "back")
                {
                    Program.NavigateBack();
                    return "";
                }
                path = Path.GetFullPath(input);

                bool pathExists = isDir ? Directory.Exists(path) && (path.EndsWith("/") || path.EndsWith("\\") || path.EndsWith("\\\\"))
                                        : File.Exists(path);
                if (pathExists)
                {
                    valid = true;
                }
                else
                {
                    ConsoleHelper.ClearConsoleInvalidInput(desc);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(desc);
                    Console.ForegroundColor = ConsoleColor.Red;
                    string pathtype = isDir ? "directory" : "file";
                    Console.Write("Ensure " + pathtype + " path is valid\n");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

            return path;
        }

    }
}
