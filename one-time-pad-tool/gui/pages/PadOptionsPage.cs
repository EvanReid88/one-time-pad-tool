using EasyConsoleCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using one_time_pad_tool.gui;
using System.IO;
using one_time_pad_tool.core.helpers;

namespace one_time_pad_tool.pages
{
    class PadOptionsPage: Page
    {
        private string[] args;
        public PadOptionsPage(Program program, string[] args)
            : base("Pad Options", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            base.Display();

            string breadcrumb = null;
            foreach (var title in Program.History.Select((page) => page.Title).Reverse())
                breadcrumb += title + " > ";
            breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);

            string[] options = { "Yes", "No", "Back" };

            string header_text = Int32.Parse(args[0]) == 0 ? "Save pad as base64 string?" : "Decrypt with base64 string pad?";
            string header_padoptions = breadcrumb + "\n---\n\n" + header_text + "\n";
            int pad_option = ConsoleHelper.MultipleChoice(true, options, header_padoptions);

            if (Int32.Parse(args[0]) == 0)
            {
                if (pad_option == 0)
                {
                    string pad_path = args[2] + Path.GetFileNameWithoutExtension(args[1]) + "_pad" + Path.GetExtension(args[1]);
                    FileHelper.ConvertFileToBase64(pad_path);
                }
            }

            Program.AddPage(new FileOptionsPage(Program, args));
            Program.NavigateTo<FileOptionsPage>();

            //if (pad_option != 2)
            //{
            //    args[5] = pad_option.ToString();
            //}
            //else
            //{
            //    Program.NavigateBack();
            //}

            //if (Int32.Parse(args[0]) == 0) {
            //    Program.AddPage(new EncryptPage(Program, args));
            //    Program.NavigateTo<EncryptPage>();
            //}
            //else
            //{
            //    Program.AddPage(new DecryptPage(Program, args));
            //    Program.NavigateTo<DecryptPage>();
            //}
        }
    }
}
