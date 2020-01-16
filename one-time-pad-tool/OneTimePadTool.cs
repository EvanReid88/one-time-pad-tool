using System;
using static one_time_pad_tool.OneTimePad;
using one_time_pad_tool.gui;

using System.Collections.Generic;
using EasyConsoleCore;
using one_time_pad_tool.pages;

namespace one_time_pad_tool
{
    class OneTimePadTool : Program
    {
        public OneTimePadTool() 
            : base("One Time Pad File Encryption", breadcrumbHeader: true)
        {
            AddPage(new MainPage(this));
            AddPage(new FilePage(this));
            SetPage<MainPage>();
        }
    }
}
