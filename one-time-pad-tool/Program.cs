using System;
using static one_time_pad_tool.OneTimePad;

namespace one_time_pad_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            OneTimePad otp = new OneTimePad();
            otp.EncryptFile("C:\\Evan\\Projects\\C#\\one-time-pad-tool\\one-time-pad-tool\\test.txt", "C:\\Evan\\Projects\\C#\\one-time-pad-tool\\one-time-pad-tool\\", "");
            Console.WriteLine("Hello World!");
        }
    }
}
