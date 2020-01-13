using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace one_time_pad_tool
{
    // TODO create interface
    // TODO use byte streams!
    class OneTimePad
    {

        static OneTimePad() { }

        public static byte[] GeneratePad(int size)
        {
            var random = new SecureRandom(); 
            random.SetSeed(BitConverter.GetBytes(Guid.NewGuid().GetHashCode() + int.MaxValue));
            var bytes = new byte[size];
            random.NextBytes(bytes);

            return bytes;
        }

        public static byte[] Encrypt(byte[] data, byte[] pad)
        {
            var result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                var sum = (int)data[i] + (int)pad[i];
                if (sum > 255)
                    sum -= 255;
                result[i] = (byte)sum;
            }
            return result;
        }

        public static byte[] Decrypt(byte[] encrypted, byte[] pad)
        {
            var result = new byte[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++)
            {
                var dif = (int)encrypted[i] - (int)pad[i];
                if (dif < 0)
                    dif += 255;
                result[i] = (byte)dif;
            }
            return result;
        }

        public void EncryptFile(string file_path, string out_path, string pad_path)
        {
            try
            {
                byte[] original_bytes = System.IO.File.ReadAllBytes(file_path);
                byte[] pad_bytes = GeneratePad(original_bytes.Length);
                byte[] encrypted_bytes = Encrypt(original_bytes, pad_bytes);

                string encrypted_bytes_base64 = Convert.ToBase64String(encrypted_bytes);
                string pad_bytes_base64 = Convert.ToBase64String(pad_bytes);

                File.WriteAllText(out_path + "test_encrypted.txt", encrypted_bytes_base64);
                File.WriteAllText(pad_path + "test_pad.txt", pad_bytes_base64);
            } 
            catch (Exception e)
            {
                Console.WriteLine("Failed to encrypt file: " + e);
            }
            
        }
    }
}
