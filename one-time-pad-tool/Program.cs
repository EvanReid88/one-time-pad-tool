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
            otp.EncryptFile(dir + "testpdf.pdf", dir, dir);
            otp.DecryptFile(dir + "encrypted_test.pdf", dir, dir + "test_pad.pdf");
            Console.WriteLine("Hello World!");
        }
    }
}
