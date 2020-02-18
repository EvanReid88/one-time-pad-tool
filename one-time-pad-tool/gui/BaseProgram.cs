using EasyConsoleCore;
using one_time_pad_tool.pages;

namespace one_time_pad_tool
{
    class BaseProgram : Program
    {
        public BaseProgram() 
            : base("One Time Pad File Encryption", breadcrumbHeader: true)
        {
            AddPage(new MainPage(this));
            SetPage<MainPage>();
        }
    }
}
