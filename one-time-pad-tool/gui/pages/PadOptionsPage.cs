using EasyConsoleCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using one_time_pad_tool.gui;
using one_time_pad_tool.gui.pages;

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

            if (pad_option != 2)
            {
                args[5] = pad_option.ToString();
            }
            else
            {
                Program.NavigateBack();
            }

            if (Int32.Parse(args[0]) == 0) {
                Program.AddPage(new EncryptPage(Program, args));
                Program.NavigateTo<EncryptPage>();
            }
            else
            {
                Program.AddPage(new DecryptPage(Program, args));
                Program.NavigateTo<DecryptPage>();
            }
        }
    }
}
