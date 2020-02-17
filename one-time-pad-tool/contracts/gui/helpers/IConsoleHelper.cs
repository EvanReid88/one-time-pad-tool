using System;
using System.Collections.Generic;
using System.Text;

namespace one_time_pad_tool.contracts.gui.helpers
{
    interface IConsoleHelper
    {
        int MultipleChoice(bool canCancel, string[] menuItems, string menu_title);
    }
}
