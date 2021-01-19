using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;

namespace one_time_pad_tool.core.helpers
{
    class FileHelper 
    {
        static FileHelper() { }

        public static void SecureDelete(string file_path, bool use_sdelete = false)
        {
            File.Delete(file_path);
        }

        public static void ConvertFileToBase64(string pad_path)
        {
            try
            {
                byte[] pad = File.ReadAllBytes(pad_path);
                string pad_base64 = Convert.ToBase64String(pad);
                File.WriteAllText(Path.GetDirectoryName(pad_path) + "\\" + Path.GetFileNameWithoutExtension(pad_path) + "_base64.txt", pad_base64);
                SecureDelete(pad_path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed convert file to base64 txt file: " + e);
            }
        }

        public static bool CheckFileBase64(string file_path)
        {
            string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$";
            try
            {
                string pad = File.ReadAllText(file_path);
                return Regex.IsMatch(pad, pattern);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
