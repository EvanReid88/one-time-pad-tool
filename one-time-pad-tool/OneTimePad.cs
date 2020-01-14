using Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace one_time_pad_tool
{
    // TODO create interface
    // https://blog.bitscry.com/2018/07/05/pgp-encryption-and-decryption-in-c/
    class OneTimePad
    {

        static OneTimePad() { }

        public static byte[] GeneratePad(int size, string pad_path)
        {
            var random = new SecureRandom();
            random.SetSeed(BitConverter.GetBytes(Guid.NewGuid().GetHashCode() + int.MaxValue));

            byte[] pad = new byte[size];
            random.NextBytes(pad);

            return pad;
        }

        public void EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {

                // TODO get file extension from path to determine names
                byte[] original_bytes = File.ReadAllBytes(file_path);
                byte[] pad_bytes = GeneratePad((int)original_bytes.Length, pad_path + "test_pad.pdf");
                byte[] encrypted_bytes = new byte[(int)original_bytes.Length];

                BitArray encrypted_bits = new BitArray(original_bytes);
                BitArray pad_bits = new BitArray(pad_bytes);

                encrypted_bits.Xor(pad_bits);

                encrypted_bits.CopyTo(encrypted_bytes, 0);

                if (base64Pad)
                {
                    string pad = Convert.ToBase64String(pad_bytes);
                    File.WriteAllText(pad_path + "test_test_pad.txt", pad);
                }
                else
                {
                    File.WriteAllBytes(pad_path + "test_test_pad.pdf", pad_bytes);
                }

                File.WriteAllBytes(out_path + "testpdf_encrypted.pdf", encrypted_bytes);

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to encrypt file: " + e);
            }
        }

        public void DecryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {
                byte[] original_bytes = File.ReadAllBytes(file_path);
                byte[] pad = base64Pad ? Convert.FromBase64String(File.ReadAllText(pad_path)) : File.ReadAllBytes(pad_path);
                byte[] decrypted_bytes = new byte[(int)original_bytes.Length];

                BitArray encrypted_bits = new BitArray(original_bytes);
                BitArray pad_bits = new BitArray(pad);

                pad_bits.Xor(encrypted_bits);

                pad_bits.CopyTo(decrypted_bytes, 0);

                File.WriteAllBytes(out_path + "testpdf_decrypted.pdf", decrypted_bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to dencrypt file: " + e);
            }
        }
    }
}
