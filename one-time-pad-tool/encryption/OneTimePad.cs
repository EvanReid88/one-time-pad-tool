using Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace one_time_pad_tool
{
    // TODO create interface
    // https://blog.bitscry.com/2018/07/05/pgp-encryption-and-decryption-in-c/
    class OneTimePad
    {
        static OneTimePad() { }

        private static FileStream GeneratePad(int size, string pad_path)
        {
            var random = new SecureRandom();
            random.SetSeed(BitConverter.GetBytes(Guid.NewGuid().GetHashCode() + int.MaxValue));
            FileStream fs = new FileStream(pad_path, FileMode.Create);

            for (int i = 0; i < size; i++)
            {
                byte[] b = new byte[1];
                random.NextBytes(b);
                fs.Write(b, 0, b.Length);
            }

            fs.Position = 0;
            return fs;
        }

        // TODO add test try blocks
        public static void ConvertPadToBase64(string pad_path)
        {
            byte[] pad = File.ReadAllBytes(pad_path);
            string pad_base64 = Convert.ToBase64String(pad);
            File.WriteAllText(Path.GetDirectoryName(pad_path) +"\\" + Path.GetFileNameWithoutExtension(pad_path) + "_base64.txt", pad_base64);
            SecureDelete(pad_path);
        }

        // TODO return byte array or byte stream, don't save new file
        public static void ConvertBase64PadToBytes(string pad_path)
        {
            string pad_base64 = File.ReadAllText(pad_path);
            byte[] pad = Convert.FromBase64String(pad_base64);
            File.WriteAllBytes(Path.GetDirectoryName(pad_path) + "\\" + Path.GetFileNameWithoutExtension(pad_path) + ".txt", pad); // TODO get original file extension
            //SecureDelete(pad_path);
        }

        //public void ConvertPadToBytes

        public static void SecureDelete(string file_path)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            // TODO add linux, osx functionality
            if (isWindows)
            {
                try
                {
                    ProcessStartInfo StartInfo = new ProcessStartInfo("sdelete", "-p 2 -r -s -nobanner " + file_path)
                    {
                        RedirectStandardOutput = false,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    Process p = Process.Start(StartInfo);
                    p.WaitForExit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to delete original file using SDelete: " + e);
                }

            }
        }

        public static bool EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {
                // TODO pass bool to generatepad. generate pad will save key on disk and then this fn will open it up for reading

                string pad_name = Path.GetFileNameWithoutExtension(file_path) + "_pad" + Path.GetExtension(file_path);
                // TODO get file extension from path to determine names
                using FileStream original_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream pad_file = GeneratePad((int)original_file.Length, pad_path + pad_name);
                using FileStream encrypted_file = new FileStream(out_path + "encrypted_test.pdf", FileMode.Create);

                int file_byte, pad_byte;
                while ((file_byte = original_file.ReadByte()) >= 0)
                {
                    pad_byte = pad_file.ReadByte();

                    BitArray encrypted_byte_bits = new BitArray(new byte[1] { (byte)file_byte });
                    BitArray pad_byte_bits = new BitArray(new byte[1] { (byte)pad_byte });
                    encrypted_byte_bits.Xor(pad_byte_bits);

                    byte[] encrypted_byte = new byte[1];
                    encrypted_byte_bits.CopyTo(encrypted_byte, 0);
                    encrypted_file.Write(encrypted_byte);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to encrypt file: " + e);
                return false;
            }
        }

        public static void DecryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {

                if (base64Pad)
                {
                    ConvertBase64PadToBytes(pad_path);
                }

                using FileStream encrypted_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream pad_file = new FileStream(pad_path, FileMode.Open, FileAccess.Read);
                using FileStream decrypted_file = new FileStream(out_path + "testpdf_decrypted.pdf", FileMode.Create);

                int file_byte, pad_byte;
                while ((file_byte = encrypted_file.ReadByte()) >= 0)
                {
                    pad_byte = pad_file.ReadByte();

                    BitArray encrypted_byte_bits = new BitArray(new byte[1] { (byte)file_byte });
                    BitArray pad_byte_bits = new BitArray(new byte[1] { (byte)pad_byte });
                    pad_byte_bits.Xor(encrypted_byte_bits);

                    byte[] decrypted_byte = new byte[1];
                    pad_byte_bits.CopyTo(decrypted_byte, 0);
                    decrypted_file.Write(decrypted_byte);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to dencrypt file: " + e);
            }
        }
    }
}
