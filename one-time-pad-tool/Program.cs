using System;
using static one_time_pad_tool.OneTimePad;

namespace one_time_pad_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            OneTimePad otp = new OneTimePad();
            string dir = "C:\\Evan\\Projects\\C#\\one-time-pad-tool\\one-time-pad-tool\\";
            otp.EncryptFile(dir + "testpdf.pdf", dir, dir, true);
            otp.DecryptFile(dir + "testpdf_encrypted.pdf", dir, dir + "test_test_pad.txt", true);
            Console.WriteLine("Hello World!");
        }
    }
}
