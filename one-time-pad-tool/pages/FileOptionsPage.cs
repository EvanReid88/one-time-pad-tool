using EasyConsoleCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using one_time_pad_tool.gui;

namespace one_time_pad_tool.pages
{
    class FileOptionsPage : Page
    {
        private string[] args;

        public FileOptionsPage(Program program, string[] args)
            : base("File Options", program)
        {
            this.args = args;
        }

        public override void Display()
        {
            base.Display();
            // TODO make into method
            string breadcrumb = null;
            foreach (var title in Program.History.Select((page) => page.Title).Reverse())
                breadcrumb += title + " > ";
            breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);


            string[] options = { "Yes", "No", "Back <--" };

            string header_deletefile = breadcrumb + "\n---\n\nDelete original file?\n";
            int deletefile_option = ConsoleHelper.MultipleChoice(true, options, header_deletefile);

            Program.AddPage(new PadOptionsPage(Program, args));
            Program.NavigateTo<PadOptionsPage>();
        }
    }
}
