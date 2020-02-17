using Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.IO;

namespace one_time_pad_tool
{
    // TODO create interface
    // TODO make async
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

        public static void EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false)
        {
            try
            {
                string pad_name = Path.GetFileNameWithoutExtension(file_path) + "_pad" + Path.GetExtension(file_path);
                string encrypted_filename = Path.GetFileNameWithoutExtension(file_path) + "_encrypted" + Path.GetExtension(file_path);
                using FileStream original_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream pad_file = GeneratePad((int)original_file.Length, pad_path + pad_name);
                using FileStream encrypted_file = new FileStream(out_path + encrypted_filename, FileMode.Create);

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
                throw (e);
            }
        }

        public static void DecryptFileBase64Pad(string file_path, string out_path, string pad_path)
        {
            try
            {
                string decrypted_filename = Path.GetFileNameWithoutExtension(file_path) + "_decrypted" + Path.GetExtension(file_path);
                using MemoryStream pad_bytes = new MemoryStream(Convert.FromBase64String(File.ReadAllText(pad_path)));
                using FileStream encrypted_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream decrypted_file = new FileStream(out_path + decrypted_filename, FileMode.Create);

                int file_byte, pad_byte;
                while ((file_byte = encrypted_file.ReadByte()) >= 0)
                {
                    pad_byte = pad_bytes.ReadByte();

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
                Console.WriteLine("Failed to decrypt file: " + e);
                throw (e);
            }
        }

        public static void DecryptFile(string file_path, string out_path, string pad_path)
        {
            try
            {
                string decrypted_filename = Path.GetFileNameWithoutExtension(file_path) + "_decrypted" + Path.GetExtension(file_path);
                using FileStream pad_file = new FileStream(pad_path, FileMode.Open, FileAccess.Read);
                using FileStream encrypted_file = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                using FileStream decrypted_file = new FileStream(out_path + decrypted_filename, FileMode.Create);

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
                throw (e);
            }
        }
    }
}
