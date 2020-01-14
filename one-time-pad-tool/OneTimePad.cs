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

        public static FileStream GeneratePad(int size, string pad_path)
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

        //public static byte[] GeneratePad(int size, string pad_path)
        //{
        //    var random = new SecureRandom();
        //    random.SetSeed(BitConverter.GetBytes(Guid.NewGuid().GetHashCode() + int.MaxValue));

        //    byte[] pad = new byte[size];
        //    random.NextBytes(pad);

        //    return pad;
        //}

        // TODO public void ConvertPadBase64

        public void EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {
                // TODO get file extension from path to determine names
                using FileStream original_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream pad_file = GeneratePad((int)original_file.Length, pad_path + "test_pad.pdf");
                using FileStream encrypted_file = new FileStream(out_path + "encrypted_test.pdf", FileMode.Create, FileAccess.ReadWrite);

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
                using FileStream encrypted_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream pad_file = new FileStream(pad_path, FileMode.Open, FileAccess.Read);
                using FileStream decrypted_file = new FileStream(out_path + "testpdf_decrypted.pdf", FileMode.Create, FileAccess.ReadWrite);

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

        //public void EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        //{
        //    try
        //    {

        //        // TODO get file extension from path to determine names
        //        byte[] original_bytes = File.ReadAllBytes(file_path);
        //        byte[] pad_bytes = GeneratePad((int)original_bytes.Length, pad_path + "test_pad.pdf");
        //        byte[] encrypted_bytes = new byte[(int)original_bytes.Length];

        //        BitArray encrypted_bits = new BitArray(original_bytes);
        //        BitArray pad_bits = new BitArray(pad_bytes);

        //        encrypted_bits.Xor(pad_bits);

        //        encrypted_bits.CopyTo(encrypted_bytes, 0);

        //        if (base64Pad)
        //        {
        //            string pad = Convert.ToBase64String(pad_bytes);
        //            File.WriteAllText(pad_path + "test_test_pad.txt", pad);
        //        }
        //        else
        //        {
        //            File.WriteAllBytes(pad_path + "test_test_pad.pdf", pad_bytes);
        //        }

        //        File.WriteAllBytes(out_path + "testpdf_encrypted.pdf", encrypted_bytes);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Failed to encrypt file: " + e);
        //    }
        //}



        //public void DecryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        //{
        //    try
        //    {
        //        byte[] original_bytes = File.ReadAllBytes(file_path);
        //        byte[] pad = base64Pad ? Convert.FromBase64String(File.ReadAllText(pad_path)) : File.ReadAllBytes(pad_path);
        //        byte[] decrypted_bytes = new byte[(int)original_bytes.Length];

        //        BitArray encrypted_bits = new BitArray(original_bytes);
        //        BitArray pad_bits = new BitArray(pad);

        //        pad_bits.Xor(encrypted_bits);

        //        pad_bits.CopyTo(decrypted_bytes, 0);

        //        File.WriteAllBytes(out_path + "testpdf_decrypted.pdf", decrypted_bytes);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Failed to dencrypt file: " + e);
        //    }
        //}
    }
}
