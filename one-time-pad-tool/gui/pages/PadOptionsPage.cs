using EasyConsoleCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using one_time_pad_tool.gui;

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

            string header_padoptions = breadcrumb + "\n---\n\nSave pad as base64 string? (Deletes raw byte pad)\n";
            int pad_option = ConsoleHelper.MultipleChoice(true, options, header_padoptions);

            if (pad_option != 2)
            {
                args[4] = pad_option.ToString();
            }
            else
            {
                Program.NavigateBack();
            }

            Program.AddPage(new EncryptPage(Program, args));
            Program.NavigateTo<EncryptPage>();
        }
    }
}
